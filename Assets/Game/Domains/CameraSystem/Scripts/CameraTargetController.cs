using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.CameraSystems
{
    public class CameraTargetController : MonoBehaviour
    {
        [Header("References")]
        public Transform playerTransform;
        public Vector2Variable lookDirectionVariable;

        [Header("Settings")] 
        public float lookAheadDistance;
        public float smoothTime;

        private Vector3 _currentVelocity;
        private Vector3 _targetOffset;

        private void LateUpdate()
        {
            var desiredPosition = CalculateTargetPosition();
            
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, 
                ref _currentVelocity, smoothTime);
        }

        private Vector3 CalculateTargetPosition()
        {
            var input = lookDirectionVariable.Value;
            _targetOffset = input.normalized * lookAheadDistance;
            
            var finalTarget = playerTransform.position + _targetOffset;
            finalTarget.z = 0;

            return finalTarget;
        }
    }
}