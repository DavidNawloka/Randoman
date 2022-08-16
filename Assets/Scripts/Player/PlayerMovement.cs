using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Astutos.Randoman.Map;

namespace Astutos.Randoman.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MapManager _mapManager = null;


        void Start()
        {
            int xCenter, yCenter;
            _mapManager.GetMiddleOfGrid(out xCenter, out yCenter);
            print(_mapManager.FindWalkableGridcell(xCenter,yCenter));
        }

        void Update()
        {

        }
    }

}