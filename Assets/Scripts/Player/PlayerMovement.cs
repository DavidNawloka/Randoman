using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Astutos.Randoman.Map;

namespace Astutos.Randoman.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MapManager _mapManager = null;
        [SerializeField] private LayerMask _unwalkableLayerMask;
        [SerializeField][Tooltip("Speed in m/s")] private float _speed = 1f;

        private PlayerInput _playerInput = null;

        private Vector3 _directionToWalk = Vector3.zero;
        private float _timeToWalk = 0;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.OnPlayerMoved += PlayerMoved;
        }

        private void OnDisable()
        {
            _playerInput.OnPlayerMoved -= PlayerMoved;
        }

        void Start()
        {
            PlacePlayer();
        }

        private void PlacePlayer()
        {
            int xCenter, yCenter;
            _mapManager.GetMiddleOfGrid(out xCenter, out yCenter);
            transform.position = _mapManager.FindWalkableGridcell(xCenter, yCenter);
        }

        void Update()
        {
            if (_timeToWalk <= 0)
            {
                CenterOnCell(); 
                return;
            }

            transform.position += _directionToWalk * Time.deltaTime * _speed;
            _timeToWalk -= Time.deltaTime;
        }

        private void PlayerMoved(Vector2 movementDirection)
        {
            if (movementDirection.magnitude <= 0.1f || Mathf.Abs(movementDirection.x)+Mathf.Abs(movementDirection.y) > 1f) return;

            CenterOnCell();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, 50f,_unwalkableLayerMask);

            if(hit.collider != null)
            {
                _directionToWalk = movementDirection;
                _timeToWalk = (hit.distance -_mapManager.GetMapGridSettings().CellSize/2) / _speed;
            }

        }

        private void CenterOnCell()
        {
            transform.position = _mapManager.GetCenteredWorldPosition(transform.position);
        }
    }

}