using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.CameraSystems
{
    /// <summary>
    /// Calculates a dynamic camera target position by blending the player's position with 
    /// independently smoothed offsets for aiming and movement. 
    /// </summary>
    public class CameraTargetController : MonoBehaviour
    {
        [Header("References")]
        public Transform playerTransform;
        public Vector2Variable lookDirectionVariable;
        public Vector2Variable moveDirectionVariable;

        [Header("Aim Settings")] 
        [Tooltip("The maximum distance the camera shifts towards the crosshair/aim direction.")]
        public float aimLeadDistance;
        
        [Tooltip("Smoothing time for the aim offset. Lower values make it snappier.")]
        public float aimLeadSmoothTime;

        [Header("Movement Settings")] 
        [Tooltip("The maximum distance the camera shifts in the direction of movement.")]
        public float movementLeadDistance;
        
        [Tooltip("Smoothing time for the movement offset. Higher values feel heavier/smoother.")]
        public float movementLeadSmoothTime;
        
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
            
            var finalPosition = playerTransform.position + _currentAimOffset + _currentMoveOffset;
            finalPosition.z = 0;

            transform.position = finalPosition;
        }

        private Vector3 GetRawAimOffset()
        {
            // Return zero if no input, otherwise calculate offset based on direction and distance
            if (lookDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            
            return lookDirectionVariable.Value.normalized * aimLeadDistance;
        }

        private Vector3 GetRawMoveOffset()
        {
            // Return zero if no input, otherwise calculate offset based on direction and distance
            if (moveDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            
            return moveDirectionVariable.Value.normalized * movementLeadDistance;
        }
    }
}