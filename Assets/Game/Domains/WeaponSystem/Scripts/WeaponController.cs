using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Data")]
        [Tooltip("Data asset defining the behavior of this weapon.")]
        [SerializeField] private WeaponModel weaponModel;
        
        [Tooltip("The specific bullet prefab to fire.")]
        [SerializeField] private BulletControllerVariable activeBulletVariable;
        
        [Header("References")]
        [Tooltip("Transform where the projectile will be spawned.")]
        [SerializeField] private Transform fireStartPoint;

        [Header("Inputs")]
        [Tooltip("Direction the weapon is aiming.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        
        [Tooltip("Input signal for firing.")]
        [SerializeField] private BoolVariable isHoldingFireButtonVariable;

        private Coroutine _fireCoroutine;
        private float _nextFireTime;

        private void Awake()
        {
            isHoldingFireButtonVariable.Changed.Register(OnHoldingFireButtonChanged);
        }

        private void OnDestroy()
        {
            isHoldingFireButtonVariable.Changed.Unregister(OnHoldingFireButtonChanged);
        }

        private void OnHoldingFireButtonChanged(bool isHolding)
        {
            if (isHolding)
            {
                _fireCoroutine ??= StartCoroutine(FireRoutine());
            }
            else
            {
                if (_fireCoroutine == null) return;
                
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
        }

        private IEnumerator FireRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => Time.time >= _nextFireTime);
                
                PerformFire();
                _nextFireTime = Time.time + weaponModel.GetFireRate();
            }
        }

        private void PerformFire()
        {
            var baseDirection = lookDirectionVariable.Value;
            var projectileCount = weaponModel.GetProjectilesPerShot();
            var spreadAngle = weaponModel.GetSpreadAngle();

            // Calculate the starting angle offset (centered spread).
            var startAngle = -spreadAngle / 2f;
            
            // Avoid division by zero if count is 1.
            var angleStep = projectileCount > 1 ? spreadAngle / (projectileCount - 1) : 0f;

            for (var i = 0; i < projectileCount; i++)
            {
                var currentAngle = startAngle + angleStep * i;
                var rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
                Vector2 spreadDirection = rotation * baseDirection;
                
                var bullet = Instantiate(activeBulletVariable.Value, fireStartPoint.position, Quaternion.identity);
                bullet.Initialize(spreadDirection);
            }
        }
    }
}