using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Astutos.Randoman.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Action<Vector2> OnPlayerMoved;

        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Enable();

            
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Move.performed += (InputAction.CallbackContext callback) => OnPlayerMoved?.Invoke(callback.ReadValue<Vector2>());
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Move.performed -= (InputAction.CallbackContext callback) => OnPlayerMoved?.Invoke(callback.ReadValue<Vector2>());
        }
    }

}