using Unity.Cinemachine;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Lumenfish.Player.UI
{
    public class CrosshairRenderer : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float renderDistance;
        
        [Tooltip("Rotation smoothness factor. Lower values result in smoother movement but increased input lag.")]
        [SerializeField] private float crosshairSpeed;
        
        [Header("Variables")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        private Camera _mainCamera;
        private Image _crosshairImage;
        private RectTransform _rectTransform;
        
        private Vector2 _currentDisplayDirection; 

        private void OnEnable()
        {
            _mainCamera = Camera.main;
            _crosshairImage = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            
            _currentDisplayDirection = lookDirectionVariable.Value.normalized;
            
            // Notify the renderer when Cinemachine is done with camera
            CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
        }

        private void OnDisable()
        {
            CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        }

        private void OnCameraUpdated(CinemachineBrain brain)
        {
            Vector3 targetDirection = lookDirectionVariable.Value;

            // Hide the crosshair if input is canceled
            if (targetDirection.sqrMagnitude < 0.01f)
            {
                 _crosshairImage.enabled = false; 
                 return;
            }
            
            _crosshairImage.enabled = true;
            targetDirection.Normalize();
            
            _currentDisplayDirection = Vector3.Slerp(_currentDisplayDirection, targetDirection, Time.deltaTime * crosshairSpeed);
            
            var centerPoint = playerPositionVariable.Value;
            var targetWorldPosition = centerPoint + (_currentDisplayDirection * renderDistance);
            var targetScreenPosition = _mainCamera.WorldToScreenPoint(targetWorldPosition);
            
            _rectTransform.position = targetScreenPosition;
        }
    }
}