using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lumenfish.InputHandling
{
    [Serializable]
    public enum InputDeviceID
    {
        Gamepad,
        MouseKeyboard
    }
    
    public class InputReader : MonoBehaviour, InputActions.IPlayerActions
    {
        [Header("Output Variables")]
        [Tooltip("Raw move input from gamepad or keyboard.")]
        [SerializeField] private Vector2Variable moveInputVariable;
        
        [Tooltip("Raw gamepad look stick input.")]
        [SerializeField] private Vector2Variable gamepadLookInputVariable;
        
        [Tooltip("Raw mouse screen position input.")]
        [SerializeField] private Vector2Variable mousePositionInputVariable;

        [Tooltip("Active input device")]
        [SerializeField] private InputDeviceIDVariable activeInputDeviceVariable;
        
        private InputActions _inputActions;
        
        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }
        private void OnDisable()
        { 
            _inputActions.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            UpdateActiveInputDevice(context);
            
            moveInputVariable.SetValue(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            UpdateActiveInputDevice(context);

            switch (activeInputDeviceVariable.Value)
            {
                case InputDeviceID.Gamepad:
                {
                    gamepadLookInputVariable.SetValue(context.ReadValue<Vector2>());
                    break;
                }
                
                case InputDeviceID.MouseKeyboard:
                {
                    mousePositionInputVariable.SetValue(context.ReadValue<Vector2>());
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateActiveInputDevice(InputAction.CallbackContext context)
        {
            if (context.performed && context.control.device is Gamepad)
            {
                activeInputDeviceVariable.Value = InputDeviceID.Gamepad;
            }

            if (context.performed && context.control.device is Keyboard or Mouse)
            {
                activeInputDeviceVariable.Value = InputDeviceID.MouseKeyboard;
            }
        }
    }
}