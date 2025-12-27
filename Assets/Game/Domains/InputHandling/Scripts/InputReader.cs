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
            UpdateActiveDevice(context);
            
            moveInputVariable.SetValue(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            UpdateActiveDevice(context);

            switch (activeInputDeviceVariable.Value)
            {
                case InputDeviceID.Gamepad:
                {
                    gamepadLookInputVariable.SetValue(context.ReadValue<Vector2>());
                    break;
                }
                
                case InputDeviceID.MouseKeyboard:
                {
                    mousePositionInputVariable.SetValue(Mouse.current.position.ReadValue());
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateActiveDevice(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            activeInputDeviceVariable.Value = context.control.device switch
            {
                Gamepad => InputDeviceID.Gamepad,
                Mouse or Keyboard => InputDeviceID.MouseKeyboard,
                _ => activeInputDeviceVariable.Value
            };
        }
    }
}