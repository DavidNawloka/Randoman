using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.Randoman.Map
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private MapGridSettings _mapGridSettings;
        [Header("Grid Visuals")]
        [SerializeField] Transform _coinsParent;
        [SerializeField] Transform _wallsParent;
        [SerializeField] private GameObject _wallPrefab = null;
        [SerializeField] private GameObject _coinPrefab = null;

        private MapGrid _mapGrid = null;

        private void Awake()
        {
            _mapGridSettings.OriginPosition = -new Vector3(_mapGridSettings.Width, _mapGridSettings.Height) * _mapGridSettings.CellSize / 2;
            _mapGrid = new MapGrid(_mapGridSettings);

            BuildMap();
        }

        private void BuildMap()
        {
            for (int x = 0; x < _mapGridSettings.Width; x++)
            {
                for (int y = 0; y < _mapGridSettings.Height; y++)
                {
                    if (!_mapGrid.IsWalkable(x, y))
                    {
                        Instantiate(_wallPrefab, _mapGrid.GetWorldPosition(x, y), _wallPrefab.transform.rotation,_wallsParent);
                    }
                    else
                    {
                        Instantiate(_coinPrefab, _mapGrid.GetWorldPosition(x, y), _coinPrefab.transform.rotation, _coinsParent);
                    }
                }
            }
        }

        public void GetMiddleOfGrid(out int x, out int y)
        {
            x = Mathf.FloorToInt(_mapGridSettings.Width / 2);
            y = Mathf.FloorToInt(_mapGridSettings.Height / 2);
        }

        public Vector3 FindWalkableGridcell(int xPreferred, int yPreferred)
        {
            int searchRadius = 1;
            int searchCount = 0;
            int x = xPreferred, y = yPreferred;

            while (!_mapGrid.IsWalkable(x, y))
            {

                x = xPreferred;
                y = yPreferred;
                switch (searchCount)
                {
                    case 0:
                        x += searchRadius;
                        break;
                    case 1:
                        x -= searchRadius;
                        break;
                    case 2:
                        y += searchRadius;
                        break;
                    case 3:
                        y -= searchRadius;
                        break;
                    case 4:
                        searchRadius++;
                        searchCount = 0;
                        break;
                }
                searchCount++;
                if (x == _mapGridSettings.Width - 1 || x < 0
                    || y < 0 || y == _mapGridSettings.Height - 1)
                {
                    Debug.LogError($"Preferred Coordinates {xPreferred} and {yPreferred} are most likely invalid!");
                    break;
                }
            }
            return _mapGrid.GetWorldPosition(x, y);
        }
        public MapGridSettings GetMapGridSettings()
        {
            return _mapGridSettings;
        }

        public Vector3 GetCenteredWorldPosition(Vector3 currentPosition)
        {
            int x, y;
            _mapGrid.GetGridPosition(currentPosition, out x, out y);
            return _mapGrid.GetWorldPosition(x, y);
        }
    }

    

}

