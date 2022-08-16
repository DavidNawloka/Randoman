using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.Randoman.Map
{
    public class MapGrid
    {

        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;
        private bool[,] gridArray; // true = walkable, false = unwalkeable

        public MapGrid(int width, int height, float cellSize, Vector3 originPosition, float obstacleRatio)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            BuildGrid(width, height, obstacleRatio);
        }

        private void BuildGrid(int width, int height, float obstacleRatio)
        {
            gridArray = new bool[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = true ? Random.Range(0f, 1f) >= obstacleRatio : false;

                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1) gridArray[x, y] = false;
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition + new Vector3(cellSize,cellSize)/2;
        }

        public bool IsWalkable(int x, int y)
        {
            return gridArray[x, y];
        }
    }

}
