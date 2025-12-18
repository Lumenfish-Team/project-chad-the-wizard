using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Settings")] 
        public float moveSpeed;
        public float rotationSpeed;
        
        [Header("Variables")]
        public Vector2Variable moveDirectionVariable;
        public Vector2Variable lookDirectionVariable;
        
        private Rigidbody2D _rigidbody2D;
        
        private Vector2 _targetMoveDirection;
        private Vector2 _targetLookDirection;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            moveDirectionVariable.Changed.Register(SetMoveDirection);
            lookDirectionVariable.Changed.Register(SetLookDirection);
        }

        private void OnDestroy()
        {
            moveDirectionVariable.Changed.Register(SetMoveDirection);
            lookDirectionVariable.Changed.Register(SetLookDirection);
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyLookDirection();
        }

        private void SetMoveDirection(Vector2 moveDirection)
        {
            _targetMoveDirection = moveDirection;
        }
        
        private void SetLookDirection(Vector2 lookDirection)
        {
            _targetLookDirection = lookDirection;
        }
        
        private void ApplyMovement()
        {
            var currentPosition = _rigidbody2D.position;
            var positionStep = _targetMoveDirection * moveSpeed * Time.fixedDeltaTime;
            
            _rigidbody2D.MovePosition(currentPosition + positionStep);
        }

        private void ApplyLookDirection()
        {
            // Prevent snapping to 0 rotation
            if (_targetLookDirection.sqrMagnitude < 0.01f) return;

            var targetAngle = Mathf.Atan2(_targetLookDirection.y, _targetLookDirection.x) * Mathf.Rad2Deg;
            targetAngle -= 90f;
            
            var angleStep = Mathf.MoveTowardsAngle(_rigidbody2D.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            _rigidbody2D.MoveRotation(angleStep);
        }
    }
}