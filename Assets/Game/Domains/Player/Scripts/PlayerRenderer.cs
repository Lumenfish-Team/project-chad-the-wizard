using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.Player
{
    public class PlayerRenderer : MonoBehaviour
    {
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");

        [Header("Components")] 
        public Animator animator;
        
        [Header("References")]
        public Vector2Variable lookDirectionVariable;

        private void Update()
        {
            var direction = lookDirectionVariable.Value;
            if (direction.sqrMagnitude < 0.01f) return;
            
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
