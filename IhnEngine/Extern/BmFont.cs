// ---- AngelCode BmFont XML serializer ----------------------
// ---- By DeadlyDan @ deadlydan@gmail.com -------------------
// ---- There's no license restrictions, use as you will. ----
// ---- Credits to http://www.angelcode.com/ -----------------

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace IhnLib {
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
	[XmlRoot("font")]
    public class FontFile {
        /// <summary>
        /// External
        /// </summary>
		[XmlElement("info")]
		public FontInfo Info {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlElement("common")]
		public FontCommon Common {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlArray("pages")]
		[XmlArrayItem("page")]
		public List<FontPage> Pages {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlArray("chars")]
		[XmlArrayItem("char")]
		public List<FontChar> Chars {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlArray("kernings")]
		[XmlArrayItem("kerning")]
		public List<FontKerning> Kernings {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
    public class FontInfo {
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("face")]
		public String Face {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("size")]
		public Int32 Size {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("bold")]
		public Int32 Bold {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("italic")]
		public Int32 Italic {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("charset")]
		public String CharSet {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("unicode")]
		public Int32 Unicode {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("stretchH")]
		public Int32 StretchHeight {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("smooth")]
		public Int32 Smooth {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("aa")]
		public Int32 SuperSampling {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[NonSerialized]
		private Rectangle _Padding;
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("padding")]
		public String Padding {
			get {
				return _Padding.X + "," + _Padding.Y + "," + _Padding.Width + "," + _Padding.Height;
			}
			set {
				String[] padding = value.Split(',');
				_Padding = new Rectangle(Convert.ToInt32(padding[0], CultureInfo.InvariantCulture), Convert.ToInt32(padding[1], CultureInfo.InvariantCulture), Convert.ToInt32(padding[2], CultureInfo.InvariantCulture), Convert.ToInt32(padding[3], CultureInfo.InvariantCulture));
			}
		}

		[NonSerialized]
		private Point _Spacing;
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("spacing")]
		public String Spacing {
			get {
				return _Spacing.X + "," + _Spacing.Y;
			}
			set {
				String[] spacing = value.Split(',');
				_Spacing = new Point(Convert.ToInt32(spacing[0], CultureInfo.InvariantCulture), Convert.ToInt32(spacing[1], CultureInfo.InvariantCulture));
			}
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("outline")]
		public Int32 OutLine {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
    public class FontCommon {
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("lineHeight")]
		public Int32 LineHeight {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("base")]
		public Int32 Base {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("scaleW")]
		public Int32 ScaleW {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("scaleH")]
		public Int32 ScaleH {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("pages")]
		public Int32 Pages {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("packed")]
		public Int32 Packed {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("alphaChnl")]
		public Int32 AlphaChannel {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("redChnl")]
		public Int32 RedChannel {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("greenChnl")]
		public Int32 GreenChannel {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("blueChnl")]
		public Int32 BlueChannel {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
    public class FontPage {
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("id")]
		public Int32 ID {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("file")]
		public String File {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
    public class FontChar {
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("id")]
		public Int32 ID {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("x")]
		public Int32 X {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("y")]
		public Int32 Y {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("width")]
		public Int32 Width {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("height")]
		public Int32 Height {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("xoffset")]
		public Int32 XOffset {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("yoffset")]
		public Int32 YOffset {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("xadvance")]
		public Int32 XAdvance {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("page")]
		public Int32 Page {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("chnl")]
		public Int32 Channel {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
	[Serializable]
    public class FontKerning {
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("first")]
		public Int32 First {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("second")]
		public Int32 Second {
			get;
			set;
		}
        /// <summary>
        /// External
        /// </summary>
		[XmlAttribute("amount")]
		public Int32 Amount {
			get;
			set;
		}
	}
    /// <summary>
    /// External
    /// </summary>
    public class FontLoader {
		static FontFile Load(String filename) {
			XmlSerializer deserializer = new XmlSerializer(typeof(FontFile));
			TextReader textReader = new StreamReader(filename);
			FontFile file = (FontFile)deserializer.Deserialize(textReader);
			textReader.Close();
			return file;
		}
	}
}