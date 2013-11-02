//
// DirectionHelper.cs
//
// Author:
//       Gigimoi <gigimoigames@gmail.com>
//
// Copyright (c) 2013 Gigimoi
using System;
using Microsoft.Xna.Framework;

namespace IhnLib {
	public static class DirectionHelper {
		public static bool IsDiagonal(Direction dir) {
			return dir == Direction.NorthEast || dir == Direction.NorthWest || dir == Direction.SouthEast || dir == Direction.SouthWest;
		}
		public static Vector2 ToVector(Direction dir) {
			if(dir == Direction.North) {
				return new Vector2(0, -1);
			}
			if(dir == Direction.NorthEast) {
				return new Vector2(1, -1);
			}
			if(dir == Direction.East) {
				return new Vector2(1, 0);
			}
			if(dir == Direction.SouthEast) {
				return new Vector2(1, 1);
			}
			if(dir == Direction.South) {
				return new Vector2(0, 1);
			}
			if(dir == Direction.SouthWest) {
				return new Vector2(-1, 1);
			}
			if(dir == Direction.West) {
				return new Vector2(-1, 0);
			}
			if(dir == Direction.NorthWest) {
				return new Vector2(-1, -1);
			}
			return new Vector2(0, 0);
		}
		public static Direction Invert(Direction dir) {
			if(dir == Direction.North) {
				return Direction.South;
			}
			if(dir == Direction.NorthEast) {
				return Direction.SouthWest;
			}
			if(dir == Direction.East) {
				return Direction.West;
			}
			if(dir == Direction.SouthEast) {
				return Direction.NorthWest;
			}
			if(dir == Direction.South) {
				return Direction.North;
			}
			if(dir == Direction.SouthWest) {
				return Direction.NorthEast;
			}
			if(dir == Direction.West) {
				return Direction.East;
			}
			if(dir == Direction.NorthWest) {
				return Direction.SouthEast;
			}
			return Direction.Center;
		}
		public static float ToAngle(Direction dir) {
			if(dir == Direction.North) {
				return 0;
			}
			if(dir == Direction.NorthEast) {
				return 45;
			}
			if(dir == Direction.East) {
				return 90;
			}
			if(dir == Direction.SouthEast) {
				return 135;
			}
			if(dir == Direction.South) {
				return 180;
			}
			if(dir == Direction.SouthWest) {
				return 225;
			}
			if(dir == Direction.West) {
				return 270;
			}
			return 315;
		}
		public static Direction VectorToDirection(Vector2 vector) {
			bool s = vector.Y > 0;
			bool e = vector.X > 0;
			bool horiz = vector.X != 0;
			bool vert = vector.Y != 0;
			if(!horiz && !vert) {
				return Direction.Center;
			}
			if(e && !vert) {
				return Direction.East;
			}
			if(!e && !vert && horiz) {
				return Direction.West;
			}
			if(e && !s && vert) {
				return Direction.NorthEast;
			}
			if(e && s) {
				return Direction.SouthEast;
			}
			if(s && !horiz) {
				return Direction.South;
			}
			if(!s && !horiz && vert) {
				return Direction.North;
			}
			if(!s && !e && vert && horiz) {
				return Direction.NorthWest;
			}
			if(s && !e && vert && horiz) {
				return Direction.SouthWest;
			}
			return Direction.Center;
		}
	}
}

