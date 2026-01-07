using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.Player
{
    public class PlayerRenderer : MonoBehaviour
    {
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");

        [Header("Components")] 
        [SerializeField] private Animator animator;
        
        [Header("Data")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        [SerializeField] private Vector2Variable moveDirectionVariable;

        private void Update()
        {
            var direction = lookDirectionVariable.Value;

            // If we aren't aiming, look where we are going
            if (direction.sqrMagnitude < 0.01f)
            {
                direction = moveDirectionVariable.Value;
            }

            // If we are neither aiming nor moving, keep the last facing direction (do nothing)
            if (direction.sqrMagnitude < 0.01f) return;
            
            UpdateAnimator(direction);
        }

        private void UpdateAnimator(Vector2 direction)
        {
            var snapX = 0f;
            var snapY = 0f;
            
            if (Mathf.Abs(direction.x) > 0.1f)
            {
                snapX = Mathf.Sign(direction.x);
            }

            if (Mathf.Abs(direction.y) > 0.1f)
            {
                snapY = Mathf.Sign(direction.y);
            }

            animator.SetFloat(InputX, snapX);
            animator.SetFloat(InputY, snapY);
        }
    }
}