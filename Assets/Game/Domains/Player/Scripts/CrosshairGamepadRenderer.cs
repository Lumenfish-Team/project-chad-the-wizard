using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Lumenfish.Player.UI
{
    public class CrosshairGamepadRenderer : MonoBehaviour, ICrosshairRenderer
    {
        [Header("Settings")] 
        [Tooltip("How far the crosshair floats from the player.")]
        [SerializeField] private float renderDistance = 3f;
        
        [Tooltip("Smoothness of the crosshair movement.")]
        [SerializeField] private float crosshairSpeed = 15f;
        
        [Header("Inputs")]
        [SerializeField] private Vector2Variable gamepadInputVariable;
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        [Header("Outputs")]
        [Tooltip("Variable to write the final look direction for the Player implementation.")]
        [SerializeField] private Vector2Variable playerLookDirectionVariable;
        
        private Camera _mainCamera;
        private Vector2 _currentDisplayDirection;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Render(Image crosshairImage, RectTransform rectTransform)
        {
            var inputDirection = gamepadInputVariable.Value;

            // Handle Visibility & Idle
            if (inputDirection.sqrMagnitude < 0.01f)
            {
                crosshairImage.enabled = false;
                return;
            }
            
            crosshairImage.enabled = true;
            inputDirection.Normalize();
            
            playerLookDirectionVariable.SetValue(inputDirection);
            
            _currentDisplayDirection = Vector2.Lerp(_currentDisplayDirection, inputDirection, Time.deltaTime * crosshairSpeed);
            var centerPoint = playerPositionVariable.Value;
            var targetWorldPos = centerPoint + (_currentDisplayDirection * renderDistance);
            
            rectTransform.position = _mainCamera.WorldToScreenPoint(targetWorldPos);
        }
    }
}