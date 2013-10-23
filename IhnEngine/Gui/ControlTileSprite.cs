using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IhnLib {
    public class ControlTileSprite : Control {
        TileType TType;

        public SpriteEffects Mirror = SpriteEffects.None;
        public ControlTileSprite(Control parent, TileType tileType)
            : base(parent, new Vector2(32, 32)) {
            TType = tileType;
        }
        public override void Render(Ihn ihn, SpriteBatch spriteBatch) {
            SystemTilemap.RenderTile(spriteBatch, ihn.GetEntitiesWith<ComponentTilemap>()[0].GetComp<ComponentTilemap>(), (TileType)this.Tag, new List<Direction>(), Root);
            base.Render(ihn, spriteBatch);
        }
    }
}
