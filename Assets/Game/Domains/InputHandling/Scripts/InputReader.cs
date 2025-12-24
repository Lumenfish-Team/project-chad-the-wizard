using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lumenfish.InputHandling
{
    public class InputReader : MonoBehaviour, InputActions.IPlayerActions
    {
        [Header("Output Variables")]
        [Tooltip("Atom variable to output move input to.")]
        [SerializeField] private Vector2Variable moveDirectionVariable;
        
        [Tooltip("Atom variable to output look/aim input to.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        
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
            moveDirectionVariable.SetValue(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            lookDirectionVariable.SetValue(context.ReadValue<Vector2>());
        }
    }
}