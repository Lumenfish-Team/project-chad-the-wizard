using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lumenfish.InputHandling
{
    public class InputReader : MonoBehaviour, InputActions.IPlayerActions
    {
        [Header("Variables")]
        public Vector2Variable moveDirectionVariable;
        public Vector2Variable lookDirectionVariable;
        
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
            var moveDirectionVector = context.ReadValue<Vector2>();
            moveDirectionVariable.SetValue(moveDirectionVector);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            var lookDirectionVector = context.ReadValue<Vector2>();
            lookDirectionVariable.SetValue(lookDirectionVector);
        }
        
    }
}
