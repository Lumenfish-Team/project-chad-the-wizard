using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.CameraSystem
{
    public class CameraTargetController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reads the player's current position from the Atoms variable.")]
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        [Tooltip("Input variable for look direction.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        
        [Tooltip("Input variable for movement direction.")]
        [SerializeField] private Vector2Variable moveDirectionVariable;

        [Header("Aim Settings")] 
        [Tooltip("The maximum distance the camera shifts towards the crosshair/aim direction.")]
        [SerializeField] private float aimLeadDistance = 2f;
        
        [Tooltip("Smoothing time for the aim offset. Lower values make it snappier.")]
        [SerializeField] private float aimLeadSmoothTime = 0.1f;

        [Header("Movement Settings")] 
        [Tooltip("The maximum distance the camera shifts in the direction of movement.")]
        [SerializeField] private float movementLeadDistance = 1f;
        
        [Tooltip("Smoothing time for the movement offset. Higher values feel heavier/smoother.")]
        [SerializeField] private float movementLeadSmoothTime = 0.25f;
        
        private Vector3 _currentAimOffset;
        private Vector3 _aimVelocity;
        private Vector3 _currentMoveOffset;
        private Vector3 _moveVelocity;
        
        private void LateUpdate()
        {
            var targetAimOffset = GetRawAimOffset();
            var targetMoveOffset = GetRawMoveOffset();
            
            _currentAimOffset = Vector3.SmoothDamp(_currentAimOffset, targetAimOffset, ref _aimVelocity, 
                aimLeadSmoothTime); 
            
            _currentMoveOffset = Vector3.SmoothDamp(_currentMoveOffset, targetMoveOffset, ref _moveVelocity, 
                movementLeadSmoothTime);
            
            // Cast to Vector3 to ensure z-axis compatibility
            Vector3 playerPos = playerPositionVariable.Value;
            var finalPosition = playerPos + _currentAimOffset + _currentMoveOffset;
            finalPosition.z = transform.position.z; // Keep camera's original Z

            transform.position = finalPosition;
        }

        private Vector3 GetRawAimOffset()
        {
            if (lookDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            return lookDirectionVariable.Value.normalized * aimLeadDistance;
        }

        private Vector3 GetRawMoveOffset()
        {
            if (moveDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            return moveDirectionVariable.Value.normalized * movementLeadDistance;
        }
    }
}