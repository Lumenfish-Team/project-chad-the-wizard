using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletController : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Movement speed of the bullet in units per second.")]
        [SerializeField] private float speed;

        [Tooltip("Time in seconds before the bullet destroys itself.")]
        [SerializeField] private float lifeTime;

        [Tooltip("Layers that trigger a collision.")]
        [SerializeField] private LayerMask hitLayers;

        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector2 direction)
        {
            _moveDirection = direction.normalized;
            
            // Clean up if nothing is hit. 
            Destroy(gameObject, lifeTime);
        }

        private void FixedUpdate()
        {
            var currentPosition = _rigidbody.position;
            var nextPosition = currentPosition + (_moveDirection * speed * Time.fixedDeltaTime);
            
            _rigidbody.MovePosition(nextPosition);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the object is in the target layer
            if (((1 << other.gameObject.layer) & hitLayers) != 0)
            {
                HandleHit(other);
            }
        }

        private void HandleHit(Collider2D other)
        {
            Debug.Log($"Bullet hit: {other.name}");
            Destroy(gameObject);
        }
    }
}