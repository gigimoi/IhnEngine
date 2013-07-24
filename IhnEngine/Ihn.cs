using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IhnEngine {
	public class Ihn : Game{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		List<ISystem> Systems = new List<ISystem>();
		List<Entity> Entities = new List<Entity>();

		public static Ihn Main;

		public Ihn(int width, int height, bool vsync) {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "assets";	            
			graphics.IsFullScreen = false;
			graphics.SynchronizeWithVerticalRetrace = vsync;
			graphics.PreferredBackBufferHeight = height;
			graphics.PreferredBackBufferWidth = width;
			Main = this;
		}
		public void RegisterSystem(ISystem system) {
			Systems.Add(system);
		}
		public void RegisterEntity(Entity entity) {
			Entities.Add(entity);
		}

		protected override void Initialize() {
			base.Initialize();
		}
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}
		protected override void Update(GameTime gameTime)
		{
			for(int i = 0; i < Systems.Count; i++) {
				for(int j = 0; j < Entities.Count; j++) {
					if(SystemHelper.CanSystemRunOnEntity(Systems[i], Entities[j])) {
						Systems[i].Update(Entities[j]);
					}
				}
			}
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}	
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			for(int i = 0; i < Systems.Count; i++) {
				for(int j = 0; j < Entities.Count; j++) {
					if(SystemHelper.CanSystemRunOnEntity(Systems[i], Entities[j])) {
						Systems[i].Render(spriteBatch, Entities[j]);
					}
				}
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}