using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.Randoman.Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private int _gridWidth = 0;
        [SerializeField] private int _gridHeight = 0;
        [SerializeField] private float _cellSize = 0f;
        [Range(0f, 1f)] [SerializeField] private float _obstacleRatio = 0.2f;
        [Header("Grid Visuals")]
        [SerializeField] Transform _coinsParent;
        [SerializeField] Transform _wallsParent;
        [SerializeField] private GameObject _wallPrefab = null;
        [SerializeField] private GameObject _coinPrefab = null;

        private MapGrid _mapGrid = null;

        private void Awake()
        {
            _mapGrid = new MapGrid(_gridWidth,_gridHeight,_cellSize,-new Vector3(_gridWidth,_gridHeight)*_cellSize/2, _obstacleRatio);

            BuildMap();
        }

        private void BuildMap()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
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
    }

}

