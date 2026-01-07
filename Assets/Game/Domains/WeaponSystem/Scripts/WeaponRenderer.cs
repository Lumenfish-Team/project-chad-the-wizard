using System;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace Lumenfish.WeaponSystem
{
    public class WeaponRenderer : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The pivot transform to rotate (usually parent of the sprite).")]
        [SerializeField] private Transform weaponPivot;
        
        [Tooltip("The sprite renderer of the weapon.")]
        [SerializeField] private SpriteRenderer weaponSpriteRenderer;

        [Header("Data")]
        [Tooltip("Aim input variable.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        
        [Tooltip("Movement input variable (fallback logic).")]
        [SerializeField] private Vector2Variable moveDirectionVariable;

        [Header("Settings")]
        [Tooltip("Distance of the weapon from the character center.")]
        [SerializeField] private float orbitalDistance;
        
        [Tooltip("Sorting order when the weapon is behind the character (aiming up).")]
        [SerializeField] private int behindLayerIndex;
        
        [Tooltip("Sorting order when the weapon is in front of the character (aiming down).")]
        [SerializeField] private int frontLayerIndex;

        private void Start()
        {
            // Set-up for initial weapon position (Default to down/front)
            UpdateWeaponTransform(Vector2.down);
        }

        private void Update()
        {
            var direction = GetEffectiveDirection();
            if (direction.sqrMagnitude < 0.01f) return;

            UpdateWeaponTransform(direction);
        }

        private Vector2 GetEffectiveDirection()
        {
            var direction = lookDirectionVariable.Value;
            if (direction.sqrMagnitude < 0.01f)
            {
                direction = moveDirectionVariable.Value;
            }

            return direction;
        }

        private void UpdateWeaponTransform(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            weaponPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            weaponPivot.localPosition = direction.normalized * orbitalDistance;
            
            // When aiming left (angle > 90 or < -90), flip Y so the gun isn't upside down.
            var isAimingLeft = Mathf.Abs(angle) > 90f;
            weaponSpriteRenderer.flipY = isAimingLeft;
            
            // If aiming Up, gun goes behind player. If aiming Down, gun goes in front.
            weaponSpriteRenderer.sortingOrder = direction.y > 0 ? behindLayerIndex : frontLayerIndex;
        }
    }
}