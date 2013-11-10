using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IhnLib {
    /// <summary>
    /// Static class to aid in 2D pathing using A*
    /// </summary>
	public static class Pathfinding {
        /// <summary>
        /// Finds path along a 2D grid
        /// </summary>
        /// <param name="solidityMap">2D array of solids</param>
        /// <param name="start">Vector of starting position</param>
        /// <param name="end">Vector of ending position</param>
        /// <returns>List of points to follow</returns>
		public static List<Vector2> FindPath(bool[,] solidityMap, Vector2 start, Vector2 end) {
			var finalPath = new List<Vector2>();
			if(!(start.X < solidityMap.GetLength(0) - 1 &&
			     start.X > 0 &&
			     start.Y < solidityMap.GetLength(1) - 1 &&
			     start.Y > 0 && 
			     end.X < solidityMap.GetLength(0) - 1 &&
			     end.X > 0 &&
			     end.Y < solidityMap.GetLength(1) - 1 &&
			     end.Y > 0)) {
				return finalPath;
			}
			var closed = new List<Node>();
			var open = new List<Node> { new Node(start) };
			bool done = false;
			Node currentNode = open[0];
			int trials = 0;
			while(!done && trials < 1000) {
				trials++;
				var lowestF = int.MaxValue;
				for(int i = 0; i < open.Count; i++) {
					if(Math.Abs(open[i].Pos.X - end.X) + Math.Abs(open[i].Pos.Y - end.Y) < lowestF) {
						lowestF = (int)(Math.Abs(open[i].Pos.X - end.X) + Math.Abs(open[i].Pos.Y - end.Y));
						currentNode = open[i];
					}
				}
				if(currentNode.Pos.X == end.X && currentNode.Pos.Y == end.Y) {
					done = true;
				}
				else {
					closed.Add(currentNode);
					open.Remove(currentNode);
					for(int i = -1; i <= 1; i++) {
						for(int j = -1; j <= 1; j++) {
							if((i != 0 || j != 0) && !DirectionHelper.IsDiagonal(DirectionHelper.VectorToDirection(new Vector2(i, j))) && 
							   currentNode.Pos.X + i > 0 && currentNode.Pos.Y + j > 0 && 
							   currentNode.Pos.X + i < solidityMap.GetLength(0) &&
							   currentNode.Pos.Y + j < solidityMap.GetLength(1) && 
							   !solidityMap[(int)currentNode.Pos.X + i, (int)currentNode.Pos.Y + j]) {
								bool isInClosed = false;
								Node closedNode = null;
								for(int k = 0; k < closed.Count; k++) {
									if(closed[k].Pos.X == currentNode.Pos.X + i && closed[k].Pos.Y == currentNode.Pos.Y + j) {
										isInClosed = true;
										closedNode = closed[k];
										break;
									}
								}
								if(isInClosed && closedNode.G > currentNode.G + 1) {
									closedNode.G = currentNode.G + 1;
									closedNode.Parent = currentNode;
								}
								else if(!isInClosed){
									bool isInOpen = false;
									Node openNode = null;
									for(int k = 0; k < open.Count; k++) {
										if(open[k].Pos.X == currentNode.Pos.X + i && open[k].Pos.Y == currentNode.Pos.Y + j) {
											isInOpen = true;
											openNode = open[k];
											break;
										}
									}
									if(isInOpen && openNode.G  > currentNode.G + 1) {
										openNode.G = currentNode.G + 1;
										openNode.Parent = currentNode;
									}
									else if(!isInOpen) {
										Node n = new Node(new Vector2(currentNode.Pos.X + i, currentNode.Pos.Y + j), currentNode);
										n.G = currentNode.G + 1;
										open.Add(n);
										trials = 0;
									}
								}
							}
						}
					}
				}
			}
			while(currentNode.Parent != null) {
				finalPath.Add(currentNode.Pos);
				currentNode = currentNode.Parent;
			}
			finalPath.Add(currentNode.Pos);
			finalPath.Reverse();
			return finalPath;
		}
	}
    /// <summary>
    /// 2D position with pathing value
    /// </summary>
	public class Node {
        /// <summary>
        /// Constructs a node at position with no parent
        /// </summary>
        /// <param name="pos">Position of the node</param>
		public Node(Vector2 pos) {
			Pos = new Vector2((int)pos.X, (int)pos.Y);
		}
        /// <summary>
        /// Constructs a node at position with parent
        /// </summary>
        /// <param name="pos">Position of the node</param>
        /// <param name="parent">Node that led to this one</param>
		public Node(Vector2 pos, Node parent) {
			Pos = pos;
			Parent = parent;
		}
        /// <summary>
        /// Position of the node
        /// </summary>
		public Vector2 Pos;
        /// <summary>
        /// Node that led to this one. Null if a starting node
        /// </summary>
		public Node Parent;
        /// <summary>
        /// Value of the path that led here
        /// </summary>
		public int G;
	}
}

