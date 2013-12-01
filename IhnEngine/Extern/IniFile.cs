//
// IniFile.cs
// Created by Christian Aaangel
//
// Inifile parser, structure and design lifted from Delphi. 
// Bool read/write is compatible with delphi
// DateTime read/write is NOT compatible with delphi
//
// Created August 5th, 2013
//
// Updated August 7th, 2013
// * Fixed creation date
// * Added Encoding to File read/write
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IhnLib
{
    public class IniSection : Dictionary<string, string>
    {
        /// <summary>
        /// Name of the section.
        /// </summary>
        public string sectionName;

        /// <summary>
        /// Section name is set to name.
        /// </summary>
        /// <param name="name"></param>
        public IniSection(string name)
        {
            sectionName = name;
        }

        /// <summary>
        /// Call ReadSectionKeys to read the keys into a List of string.
        /// </summary>
        /// <returns></returns>
        public List<string> ReadSectionKeys()
        {
            var result = new List<string>();

            foreach (string key in Keys)
            {
                result.Add(key);
            }

            return result;
        }
    }
    
    public class IniFile
    {
        // private
        
        private string _iniFileFilename;

        private List<IniSection> iniContent;

        private IniSection GetSection(string section)
        {
            foreach (var aSection in iniContent)
            {
                if (section.ToUpper() == aSection.sectionName.ToUpper())
                {
                    return aSection;
                }
            }
            return null;
        }

        private void _Load()
        {
            if (!File.Exists(_iniFileFilename))
            {
                return;
            }

            string[] lines = File.ReadAllLines(_iniFileFilename, Encoding.GetEncoding("iso-8859-1"));
            IniSection section = null;
            
            foreach (string line in lines)
            {
                if (line != "")
                {                
                    // If new section
                    if (line.Substring(0, 1) == "[")
                    {
                        string sectionName = line.Substring(1, line.Length - 2);
                        section = new IniSection(sectionName);
                        iniContent.Add(section);
                    }
                    else
                    {
                        // Treat as value, if not empty
                        string[] strList = line.Split('='); // use the ' to define a char
                        string key = strList[0];
                        string value = strList[1];
                        if (strList.Length > 2)
                        {
                            for (int i = 2; i < strList.Length; i++)
                            {
                                value = value + "=" + strList[i];
                            }
                        }

                       if (section != null) // write only if a section is set
                       {
                           section.Add(key, value);
                       }
                    }
                }
            }
        }

        private void _Save()
        {
            TextWriter file = new StreamWriter(_iniFileFilename, false, Encoding.GetEncoding("iso-8859-1"));

            foreach (var section in iniContent)
            {
                file.WriteLine("[" + section.sectionName + "]");

                var keys = section.ReadSectionKeys();

                foreach (var key in keys)
                {
                    var value = section[key];
                    file.WriteLine(key + "=" + value);
                }
                file.WriteLine("");
            }

            file.Close();
        }

        // constructor
        
        /// <summary>
        /// Create assigns the iniFileName parameter to the FileName property.
        /// </summary>
        /// <param name="iniFilename">Specify the filename of the ini file to use (loaded if it exists)</param>
        public IniFile(string iniFilename)
        {
            _iniFileFilename = iniFilename;
            iniContent = new List<IniSection>();
            _Load();
        }

        // public
        
        /// <summary>
        /// Contains the filename of the ini file from which to read and write information. 
        /// </summary>
        public string fileName
        {
            get { return _iniFileFilename; }
            set { _iniFileFilename = value; }
        }

        /// <summary>
        /// Call DeleteKey to erase an INI file entry. If the key is the only one in the section, the section will be deleted as well.
        /// </summary>
        /// <param name="sectionName">Name of an INI file section</param>
        /// <param name="keyName">Name of the Key to delete from the section</param>
        public void DeleteKey(string sectionName, string keyName)
        {
            for (int i = 0; i < iniContent.Count - 1; i++)
            {
                var sec = GetSection(sectionName);
                if (sec != null)
                {
                    string value;
                    if (sec.TryGetValue(keyName, out value))
                    {
                        if (sec.Count == 1)
                        {
                            // only key in the section, delete the section instead.
                            EraseSection(sectionName);
                        }
                        else
                        {
                            sec.Remove(keyName);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Call EraseSection to remove a section, all its keys, and their data values from an INI file.
        /// </summary>
        /// <param name="sectionName">Section identifies the INI file section to remove</param>
        public void EraseSection(string sectionName)
        {
            for (int i = 0; i < iniContent.Count - 1; i++)
            {
                if (iniContent[i].sectionName.ToUpper() == sectionName.ToUpper())
                {
                    iniContent.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Get the IniSection object that holds section information.
        /// </summary>
        /// <param name="sectionName">Name of an INI file section</param>
        /// <returns></returns>
        public IniSection ReadSection(string sectionName)
        {
            return GetSection(sectionName);
        }

        /// <summary>
        /// Call ReadSections to retrieve the names of all sections in an INI file into a List of string. 
        /// </summary>
        /// <returns></returns>
        public List<string> ReadSections()
        {
            var result = new List<string>();

            foreach (var section in iniContent)
            {
                result.Add(section.sectionName);
            }

            return result;
        }

        /// <summary>
        /// Call ReadSectionKeys to read the keys, within a specified section of an INI file into a List of string.
        /// </summary>
        /// <param name="sectionName">Name of an INI file section</param>
        /// <returns></returns>
        public List<string> ReadSectionKeys(string sectionName)
        {
            var section = GetSection(sectionName);
            if (section == null)
            {
                // return empty list if section wasn't found
                return new List<string>();
            }

            return section.ReadSectionKeys();
        }

        /// <summary>
        /// Call ReadString to read a string value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the string value to return if the section or key does not exists</param>
        /// <returns></returns>
        public string ReadString(string sectionName, string keyName, string defaultValue)
        {
            var section = GetSection(sectionName);
            if (section == null)
            {
                return defaultValue;
            }

            string result;
            if (!section.TryGetValue(keyName, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Call WriteString to write a string value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteString creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the string value to write.</param>
        public void WriteString(string sectionName, string keyName, string value)
        {
            var section = GetSection(sectionName);
            if (section == null)
            {
                section = new IniSection(sectionName);
                iniContent.Add(section);
            }

            if (section.ContainsKey(keyName))
            {
                section[keyName] = value;
            }
            else
            {
                section.Add(keyName, value);
            }
        }

        /// <summary>
        /// Saves the Inifile to the file specified in filename.
        /// </summary>
        public void Save()
        {
            _Save();
        }

        /// <summary>
        /// Loads the Inifile specified in filename.
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            _Load();
            return true;
        }

        /// <summary>
        /// Use SectionExists to determine whether a section exists within the ini file specified in FileName. SectionExists returns a Boolean value that indicates whether the section in question exists.
        /// </summary>
        /// <param name="sectionName">sectionName is the ini file section SectionExists determines the existence of.</param>
        /// <returns></returns>
        public bool SectionExists(string sectionName)
        {
            foreach (var section in iniContent)
            {
                if (sectionName.ToUpper() == section.sectionName.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Use KeyExists to determine whether a key exists in the ini file specified in FileName. ValueExists returns a boolean value that indicates whether the key exists in the specified section.
        /// </summary>
        /// <param name="sectionName">sectionName is the section in the ini file in which to search for the key.</param>
        /// <param name="keyName">keyName is the name of the key to search for.</param>
        /// <returns></returns>
        public bool KeyExists(string sectionName, string keyName)
        {
            foreach (var section in iniContent)
            {
                if (section.sectionName.ToUpper() == sectionName.ToUpper())
                {
                    if (section.ContainsKey(keyName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Call WriteBool to write a boolean value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteBool creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the boolean value to write.</param>
        public void WriteBool(string sectionName, string keyName, bool value)
        {
            // Delphi compatible
            // 0 - False
            // 1 - True
            string valueStr;
            if (value)
            {
                valueStr = "1";
            }
            else
            {
                valueStr = "0";
            }
            WriteString(sectionName, keyName, valueStr);
        }

        /// <summary>
        /// Call ReadBool to read a boolean value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the boolean value to return if the section or key does not exists</param>
        /// <returns></returns>
        public bool ReadBool(string sectionName, string keyName, bool defaultValue)
        {
            if (KeyExists(sectionName, keyName))
            {
                if (ReadString(sectionName, keyName, "0") == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Call WriteInteger to write a integer (int) value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteInteger creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the integer (int) value to write.</param>
        public void WriteInteger(string sectionName, string keyName, int value)
        {
            WriteString(sectionName, keyName, value.ToString());
        }

        /// <summary>
        /// Call ReadInteger to read a integer (int) value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the integer (int) value to return if the section or key does not exists</param>
        /// <returns></returns>
        public int ReadInteger(string sectionName, string keyName, int defaultValue)
        {
            if (!KeyExists(sectionName, keyName))
            {
                return defaultValue;
            }

            int result = Convert.ToInt32(ReadString(sectionName, keyName, "-1"));
            return result;
        }

        /// <summary>
        /// Call WriteInteger64 to write a 64bit integer (long) value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteInteger64 creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the 64bit integer (long) value to write.</param>
        public void WriteInteger64(string sectionName, string keyName, long value)
        {
            WriteString(sectionName, keyName, value.ToString());
        }

        /// <summary>
        /// Call ReadInteger64 to read a 64bit integer (long) value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the 64bit integer (long) value to return if the section or key does not exists</param>
        /// <returns></returns>
        public long ReadInteger64(string sectionName, string keyName, long defaultValue)
        {
            if (!KeyExists(sectionName, keyName))
            {
                return defaultValue;
            }

            long result = Convert.ToInt64(ReadString(sectionName, keyName, "-1"));
            return result;
        }

        /// <summary>
        /// Call WriteShort to write a 16bit integer (short) value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteShort creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the 16bit integer (short) value to write.</param>
        public void WriteShort(string sectionName, string keyName, short value)
        {
            WriteString(sectionName, keyName, value.ToString());
        }

        /// <summary>
        /// Call ReadShort to read a 16bit integer (short) value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the 16bit integer (short) value to return if the section or key does not exists</param>
        /// <returns></returns>
        public short ReadShort(string sectionName, string keyName, short defaultValue)
        {
            if (!KeyExists(sectionName, keyName))
            {
                return defaultValue;
            }

            short result = Convert.ToInt16(ReadString(sectionName, keyName, "-1"));
            return result;
        }

        /// <summary>
        /// Call WriteFloat to write a double value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteShort creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the double value to write.</param>
        public void WriteFloat(string sectionName, string keyName, double value)
        {
            WriteString(sectionName, keyName, value.ToString());
        }

        /// <summary>
        /// Call ReadFloat to read a double value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the double value to return if the section or key does not exists</param>
        /// <returns></returns>
        public double ReadFloat(string sectionName, string keyName, double defaultValue)
        {
            if (!KeyExists(sectionName, keyName))
            {
                return defaultValue;
            }

            double result = double.Parse(ReadString(sectionName, keyName, "0"));
            return result;
        }

        /// <summary>
        /// Call WriteDateTime to write a DateTime value to an INI file.
        /// Attempting to write a data value to a nonexistent section or attempting to write data to a nonexistent key are not errors. 
        /// In these cases, WriteDateTime creates the section and key and sets its initial value to Value.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contain the key to which to write.</param>
        /// <param name="keyName">keyName is the name of the key for which to set a value.</param>
        /// <param name="value">value is the DateTime value to write.</param>
        public void WriteDateTime(string sectionName, string keyName, DateTime value)
        {
            WriteString(sectionName, keyName, value.ToString());
        }

        /// <summary>
        /// Call ReadDateTime to read a double value from an INI file.
        /// </summary>
        /// <param name="sectionName">sectionName identifies the section in the file that contains the desired key.</param>
        /// <param name="keyName">keyName is the name of the key from which to retrieve the value.</param>
        /// <param name="defaultValue">defaultValue is the DateTime value to return if the section or key does not exists</param>
        /// <returns></returns>
        public DateTime ReadDateTime(string sectionName, string keyName, DateTime defaultValue)
        {
            if (!KeyExists(sectionName, keyName))
            {
                return defaultValue;
            }

            DateTime result = DateTime.Parse(ReadString(sectionName, keyName, "0"));
            return result;
        }
    }
}