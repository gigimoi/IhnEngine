using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    /// <summary>
    /// Control that renders a tiletype
    /// </summary>
    public class ControlTileSprite : Control {
        TileType TType;
        /// <summary>
        /// Spriteeffects to apply on the sprite
        /// </summary>
        public SpriteEffects Mirror = SpriteEffects.None;
        /// <summary>
        /// Creates a new control tile sprite with tile type and parent
        /// </summary>
        /// <param name="parent">Control this control will be a child of</param>
        /// <param name="tileType">TileType to render</param>
        public ControlTileSprite(Control parent, TileType tileType)
            : base(parent, new Vector2(32, 32)) {
            TType = tileType;
        }
        /// <summary>
        /// Renders the tiletype
        /// </summary>
        /// <param name="ihn">Ihn calling the render</param>
        /// <param name="spriteBatch">Spritebatch to render with</param>
        public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
            SystemTilemap.RenderTile(spriteBatch, ihn.GetEntitiesWith<ComponentTilemap>()[0].GetComp<ComponentTilemap>(), (TileType)this.Tag, new List<Direction>(), Root);
            base.Render(ihn, spriteBatch);
        }
    }
}
