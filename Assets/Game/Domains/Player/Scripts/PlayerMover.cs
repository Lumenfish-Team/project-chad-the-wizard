using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("How fast the player moves")]
        [SerializeField] private float moveSpeed;
        
        [Header("Variables")]
        [SerializeField] private Vector2Variable moveDirectionVariable;
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        private Rigidbody2D _rigidbody2D;
        private Vector2 _targetMoveDirection;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            moveDirectionVariable.Changed.Register(SetMoveDirection);
        }

        private void OnDisable()
        {
            moveDirectionVariable.Changed.Unregister(SetMoveDirection);
        }

        private void Update()
        {
            playerPositionVariable.Value = transform.position;
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void SetMoveDirection(Vector2 moveDirection)
        {
            _targetMoveDirection = moveDirection;
        }
        
        private void ApplyMovement()
        {
            _rigidbody2D.linearVelocity = _targetMoveDirection * moveSpeed;
        }
    }
}