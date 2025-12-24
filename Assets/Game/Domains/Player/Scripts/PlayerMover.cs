using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Movement speed in units per second")]
        [SerializeField] private float moveSpeed;
        
        [Header("Variables")]
        [SerializeField] private Vector2Variable moveDirectionVariable;
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            playerPositionVariable.Value = transform.position;
        }

        private void FixedUpdate()
        {
            var direction = moveDirectionVariable.Value;
            _rigidbody2D.linearVelocity = direction * moveSpeed;
        }
    }
}