using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Control
{
    public class InputManager : MonoBehaviour
    {
        public event EventHandler OnPauseAction;

        private static InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private PlayerInputAction playerInputAction;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            playerInputAction = new PlayerInputAction();
            playerInputAction.Player.Enable();

            playerInputAction.UI.Pause.performed += Pause_performed;
        }

        private void OnEnable()
        {
            playerInputAction.Player.Enable();
            playerInputAction.UI.Enable();
        }

        private void OnDisable()
        {
            playerInputAction.Player.Disable();
            playerInputAction.UI.Disable();
        }

        private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (OnPauseAction != null)
            {
                OnPauseAction?.Invoke(this, EventArgs.Empty);
            }
        }

        public Vector2 GetPlayerMovement()
        {
            return playerInputAction.Player.Movement.ReadValue<Vector2>();
        }

        // public Vector2 GetMouseDelta()
        // {
        //     return playerInputAction.Player.Look.ReadValue<Vector2>();
        // }

        public bool PlayerJumped()
        {
            return playerInputAction.Player.Jump.triggered;
        }

        // public bool PlayerPaused()
        // {
        //     return playerInputAction.UI.Pause.triggered;
        // }
    }
}

