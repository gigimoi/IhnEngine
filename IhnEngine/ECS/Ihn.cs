using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace IhnLib {
    /// <summary>
    /// Core class, only 1 should ever be active at a time
    /// </summary>
	public class Ihn : Game{
		GraphicsDeviceManager graphics;
        /// <summary>
        /// Default spritebatch
        /// </summary>
		public SpriteBatch SBatch;
		List<ISystem> Systems = new List<ISystem>();
		List<Entity> Entities = new List<Entity>();
        /// <summary>
        /// Vector where the camera is located
        /// </summary>
		public Vector2 CameraPos;
        /// <summary>
        /// Zoom level of the camera
        /// </summary>
		public float Zoom = 1.0f;

        /// <summary>
        /// Singleton
        /// </summary>
		public static Ihn Instance;

        private bool vsync;
        private int height;
        private int width;
        private bool fullscreen;

        /// <summary>
        /// Instantiates a new Ihn. Only call this once
        /// </summary>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        /// <param name="vsync">Vertical Retrace</param>
        /// <param name="fullscreen">Guess</param>
		public Ihn(int width, int height, bool vsync, bool fullscreen = false) {
			graphics = new GraphicsDeviceManager(this);            
			Content.RootDirectory = Directory.GetCurrentDirectory().Substring(0, 3);
			Instance = this;

            SBatch = new SpriteBatch(graphics.GraphicsDevice);

            this.vsync = vsync;
            this.height = height;
            this.width = width;
            this.fullscreen = fullscreen;
		}

        /// <summary>
        /// Initialize this instance.
        /// </summary>
		protected override void Initialize () {
            graphics.SynchronizeWithVerticalRetrace = vsync;
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferMultiSampling = true;
			//graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
			//graphics.PreferredBackBufferFormat = SurfaceFormat.Rg32;
			graphics.IsFullScreen = fullscreen;
            this.Window.AllowUserResizing = false;
		}

        /// <summary>
        /// Copies the debug assets from up 2 folders for easier access
        /// </summary>
		public static void CopyDebugAssets() {
			var dir = new DirectoryInfo("assets");
			if(dir.Exists) {
				foreach(FileInfo file in dir.GetFiles()) {
					file.Delete();
				}
				foreach(DirectoryInfo d in dir.GetDirectories()) {
					d.Delete(true);
				}
			}
			foreach(string dirPath in Directory.GetDirectories(@"../../assets", "*", SearchOption.AllDirectories)) {
				Directory.CreateDirectory(dirPath.Replace(@"../../assets", "assets"));
			}
			foreach(string newPath in Directory.GetFiles(@"../../assets", "*", SearchOption.AllDirectories)) {
				File.Copy(newPath, newPath.Replace(@"../../assets", "assets"));
			}
		}
        /// <summary>
        /// Removes all systems in the Ihn
        /// </summary>
		public void ClearSystems() {
			Systems = new List<ISystem>();
		}
        /// <summary>
        /// Removes all entities in the Ihn
        /// </summary>
		public void ClearEntities() {
			Entities = new List<Entity>();
			_ecdict = new Dictionary<Type, List<Entity>>();
		}
        /// <summary>
        /// Adds a new system to the ihn
        /// </summary>
        /// <param name="system">System to add</param>
		public void AddSystem(ISystem system) {
			Systems.Add(system);
		}
        /// <summary>
        /// Adds a new entity to the ihn
        /// </summary>
        /// <param name="entity">Entity to add</param>
		public void AddEntity(Entity entity) {
			Entities.Add(entity);
		}
        /// <summary>
        /// Removes an entity from the ihn
        /// </summary>
        /// <param name="entity">Entity to remove</param>
		public void RemoveEntity(Entity entity) {
			Entities.Remove(entity);
			var comps = new Component[entity.Components.Count];
			entity.Components.Values.CopyTo(comps, 0);
			for(int i = 0; i < entity.Components.Count; i++) {
				UnRegisterEntityHasComponent(comps[i].GetType(), entity);
			}
		}
        /// <summary>
        /// Removes a system from the ihn
        /// </summary>
        /// <param name="sysType">System type to remove</param>
		public void RemoveSystem(Type sysType) {
			for(int i = 0; i < Systems.Count; i++) {
				if(Systems[i].GetType() == sysType) {
					Systems.RemoveAt(i);
					break;
				}
			}
		}
		/// <summary>
		/// Checks if the ihn contains an entity
		/// </summary>
		/// <param name="entity">Entity to check for</param>
		/// <returns>True if Ihn contains entity</returns>
		public bool ContainsEntity(Entity entity) {
			for (int i = 0; i < Entities.Count; i++) {
				if (Entities[i] == entity) {
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Registers that an entity has a component
		/// </summary>
		/// <param name="t">Component type</param>
		/// <param name="entity">Entity that has the component</param>
		public void RegisterEntityHasComponent(Type t, Entity entity) {
			if(!_ecdict.ContainsKey(t)) {
				_ecdict.Add(t, new List<Entity> { entity });
			}
			else {
				_ecdict[t].Add(entity);
			}
		}
        /// <summary>
        /// Unregisters that an entity has a component
        /// </summary>
        /// <param name="t">Component Type</param>
        /// <param name="entity">Entity to unregister component from</param>
		public void UnRegisterEntityHasComponent(Type t, Entity entity) {
			_ecdict[t].Remove(entity);
		}
		Dictionary<Type, List<Entity>> _ecdict = new Dictionary<Type, List<Entity>>();
        /// <summary>
        /// Gathers a list of all entities with component T
        /// </summary>
        /// <typeparam name="T">Component to search for entities with</typeparam>
        /// <returns>List of all entities with component T</returns>
		public List<Entity> GetEntitiesWith<T>() where T : Component {
			if(_ecdict.ContainsKey(typeof(T))) {
				return _ecdict[typeof(T)];
			}
			return new List<Entity>();
		}
        /// <summary>
        /// Gathers a list of all entities with component T1 and T2
        /// </summary>
        /// <typeparam name="T1">First Component to search for entities with</typeparam>
        /// <typeparam name="T2">Second Component to search for entities with</typeparam>
        /// <returns>List of all entities with component T1 and T2</returns>
		public List<Entity> GetEntitiesWith<T1, T2>() where T1 : Component where T2 : Component {
			if(_ecdict.ContainsKey(typeof(T1)) && _ecdict.ContainsKey(typeof(T2))) {
				List<Entity> L1 = _ecdict[typeof(T1)];
				List<Entity> L2 = _ecdict[typeof(T2)];
				List<Entity> L3 = new List<Entity>();
				for(int i = 0; i < L1.Count; i++) {
					for(int j = 0; j < L2.Count; j++) {
						if(L1[i] == L2[j]) {
							L3.Add(L1[i]);
						}
					}
				}
				return L3;
			}
			return new List<Entity>();
		}
        /// <summary>
        /// Gets the entity found in the entity list at i
        /// </summary>
        /// <param name="i">Index to get from</param>
        /// <returns>Entity at i</returns>
		public Entity GetEntityAt(int i) {
			return Entities[i];
		}
        /// <summary>
        /// Gets the system found in the system list at i
        /// </summary>
        /// <param name="i">Index to get from</param>
        /// <returns>System at i</returns>
		public ISystem GetSystemAt(int i) {
			return Systems[i];
		}
        /// <summary>
        /// Amount of entities in the ihn
        /// </summary>
		public int EntityCount {
			get {
				return Entities.Count;
			}
		}
        /// <summary>
        /// Amount of systems in the ihn
        /// </summary>
		public int SystemCount {
			get {
				return Systems.Count;
			}
		}
		/// <summary>
        /// Triggers "Ihn Load Rsc" and initializes spritebatch
        /// </summary>
		protected override void LoadContent()
		{
			SBatch = new SpriteBatch(GraphicsDevice);
			EventManager.Raise("Ihn Load Rsc");
		}
        /// <summary>
        /// 1: Updates keyhelper, mousehelper
        /// 2: Raises "Pre Ihn Update"
        /// 3: Updates all systems and entities
        /// 4: Raises "Post Ihn Update"
        /// </summary>
        /// <param name="gameTime">Time past since last update</param>
		protected override void Update(GameTime gameTime)
		{
			KeyHelper.Update();
			MouseHelper.Update();
			EventManager.Raise("Pre Ihn Update");
			for(int i = 0; i < Systems.Count; i++) {
				for(int j = 0; j < Entities.Count; j++) {
					if(SystemHelper.CanSystemRunOnEntity(Systems[i], Entities[j])) {
						Systems[i].Update(this, Entities[j]);
					}
				}
			}
			EventManager.Raise("Post Ihn Update");
			base.Update(gameTime);
		}
        /// <summary>
        /// 1: Clears screen with cornflower blue and begins SBatch
        /// 2: Raises "Pre Ihn Render"
        /// 3: Renders all systems/entities
        /// 4: Raises "Post Ihn Render"
        /// 5: Ends SBatch
        /// </summary>
        /// <param name="gameTime"></param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			SBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, 
			                  DepthStencilState.Default, RasterizerState.CullNone, null, 
			                  Matrix.CreateScale(Zoom));
			EventManager.Raise("Pre Ihn Render");
			for(int i = 0; i < Systems.Count; i++) {
				for(int j = 0; j < Entities.Count; j++) {
					if(SystemHelper.CanSystemRunOnEntity(Systems[i], Entities[j])) {
						Systems[i].Render(this, SBatch, Entities[j]);
					}
				}
			}
			EventManager.Raise("Post Ihn Render");
			SBatch.End();
			base.Draw(gameTime);
		}
	}
}