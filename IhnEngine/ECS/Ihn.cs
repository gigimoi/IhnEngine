using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace IhnLib {
	public class Ihn : Game{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		List<ISystem> Systems = new List<ISystem>();
		List<Entity> Entities = new List<Entity>();
		public Vector2 CameraPos;
		public float Zoom = 1.0f;

		public static Ihn Instance;

		public Ihn(int width, int height, bool vsync, bool fullscreen = false) {
			graphics = new GraphicsDeviceManager(this);            
			graphics.SynchronizeWithVerticalRetrace = vsync;
			graphics.PreferredBackBufferHeight = height;
			graphics.PreferredBackBufferWidth = width;
			graphics.PreferMultiSampling = true;
			graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
			graphics.PreferredBackBufferFormat = SurfaceFormat.RgbPvrtc4Bpp;
			graphics.IsFullScreen = fullscreen;
			Instance = this;
		}

		public static void CopyDebugAssets() {
			var dir = new DirectoryInfo("assets");
			var files = new DirectoryInfo(@"..\..\assets");
			if(dir.Exists) {
				foreach(FileInfo file in dir.GetFiles()) {
					file.Delete();
				}
				foreach(DirectoryInfo d in dir.GetDirectories()) {
					d.Delete(true);
				}
			}
			foreach(string dirPath in Directory.GetDirectories(@"..\..\assets", "*", SearchOption.AllDirectories)) {
				Directory.CreateDirectory(dirPath.Replace(@"..\..\assets", "assets"));
			}
			foreach(string newPath in Directory.GetFiles(@"..\..\assets", "*", SearchOption.AllDirectories)) {
				File.Copy(newPath, newPath.Replace(@"..\..\assets", "assets"));
			}
		}
		public void ClearSystems() {
			Systems = new List<ISystem>();
		}
		public void ClearEntities() {
			Entities = new List<Entity>();
			_ecdict = new Dictionary<Type, List<Entity>>();
		}
		public void AddSystem(ISystem system) {
			Systems.Add(system);
		}
		public void AddEntity(Entity entity) {
			Entities.Add(entity);
		}
		public void RemoveEntity(Entity entity) {
			Entities.Remove(entity);
			var comps = new Component[entity.Components.Count];
			entity.Components.Values.CopyTo(comps, 0);
			for(int i = 0; i < entity.Components.Count; i++) {
				UnRegisterEntityHasComponent(comps[i].GetType(), entity);
			}
		}
		public void RemoveSystem(Type sysType) {
			for(int i = 0; i < Systems.Count; i++) {
				if(Systems[i].GetType() == sysType) {
					Systems.RemoveAt(i);
					break;
				}
			}
		}

		public void RegisterEntityHasComponent(Type t, Entity entity) {
			if(!_ecdict.ContainsKey(t)) {
				_ecdict.Add(t, new List<Entity> { entity });
			}
			else {
				_ecdict[t].Add(entity);
			}
		}
		public void UnRegisterEntityHasComponent(Type t, Entity entity) {
			_ecdict[t].Remove(entity);
		}
		Dictionary<Type, List<Entity>> _ecdict = new Dictionary<Type, List<Entity>>();
		public SpriteBatch SBatch;
		public List<Entity> GetEntitiesWith<T>() where T : Component {
			if(_ecdict.ContainsKey(typeof(T))) {
				return _ecdict[typeof(T)];
			}
			return new List<Entity>();
		}
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
		public Entity GetEntityAt(int i) {
			return Entities[i];
		}
		public ISystem GetSystemAt(int i) {
			return Systems[i];
		}
		public int EntityCount {
			get {
				return Entities.Count;
			}
		}
		public int SystemCount {
			get {
				return Systems.Count;
			}
		}

		protected override void Initialize() {
			base.Initialize();
		}
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			EventManager.Raise("Ihn Load Rsc");
		}
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
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			if(SBatch == null) {
				SBatch = spriteBatch;
			}
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, 
			                  DepthStencilState.Default, RasterizerState.CullNone, null, 
			                  Matrix.CreateScale(Zoom));
			EventManager.Raise("Pre Ihn Render");
			for(int i = 0; i < Systems.Count; i++) {
				for(int j = 0; j < Entities.Count; j++) {
					if(SystemHelper.CanSystemRunOnEntity(Systems[i], Entities[j])) {
						Systems[i].Render(this, spriteBatch, Entities[j]);
					}
				}
			}
			EventManager.Raise("Post Ihn Render");
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}