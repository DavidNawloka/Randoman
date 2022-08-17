using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.Randoman.Map
{
    public class MapGrid
    {
        private MapGridSettings _mapGridSettings;
        private bool[,] gridArray; // true = walkable, false = unwalkeable

        public MapGrid(MapGridSettings mapGridSettings)
        {
            _mapGridSettings = mapGridSettings;

            BuildGrid();
        }

        private void BuildGrid()
        {
            gridArray = new bool[_mapGridSettings.Width, _mapGridSettings.Height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = true ? Random.Range(0f, 1f) >= _mapGridSettings.ObstacleRatio : false;

                    if (x == 0 || x == _mapGridSettings.Width - 1 || y == 0 || y == _mapGridSettings.Height - 1) gridArray[x, y] = false;
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _mapGridSettings.CellSize + _mapGridSettings.OriginPosition + new Vector3(_mapGridSettings.CellSize, _mapGridSettings.CellSize)/2;
        }

        public void GetGridPosition(Vector3 worldPosition, out int x, out int y)
        {
            Vector3 gridPosition = (worldPosition - _mapGridSettings.OriginPosition - new Vector3(_mapGridSettings.CellSize, _mapGridSettings.CellSize) / 2) / _mapGridSettings.CellSize;
            x = Mathf.RoundToInt(gridPosition.x);
            y = Mathf.RoundToInt(gridPosition.y);
        }

        public bool IsWalkable(int x, int y)
        {
            return gridArray[x, y];
        }
    }

    [System.Serializable]
    public class MapGridSettings
    {
        public int Width;
        public int Height;
        public float CellSize;
        public Vector3 OriginPosition;
        public float ObstacleRatio;

        public MapGridSettings(int width, int height, float cellSize, Vector3 originPosition, float obstacleRatio)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            OriginPosition = originPosition;
            ObstacleRatio = obstacleRatio;
        }
    }

}
