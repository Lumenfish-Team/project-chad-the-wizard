using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Lumenfish.Player.UI
{
    public class CrosshairMouseRenderer : MonoBehaviour, ICrosshairRenderer
    {
        [Header("Inputs")] 
        [SerializeField] private Vector2Variable mouseScreenPositionVariable;
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        [Header("Outputs")]
        [Tooltip("Variable to write the final look direction for the Player implementation.")]
        [SerializeField] private Vector2Variable playerLookDirectionVariable;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Render(Image crosshairImage, RectTransform rectTransform)
        {
            crosshairImage.enabled = true; 
            
            var mousePosition = mouseScreenPositionVariable.Value;
            rectTransform.position = mousePosition;
            
            var worldMousePos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
            var playerPos = playerPositionVariable.Value;
            
            var lookDirection = ((Vector2)worldMousePos - playerPos).normalized;
            playerLookDirectionVariable.SetValue(lookDirection);
        }
    }
}