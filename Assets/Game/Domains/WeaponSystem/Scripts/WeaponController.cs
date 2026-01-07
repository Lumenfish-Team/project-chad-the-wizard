using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Time in seconds between shots.")]
        [SerializeField] private float fireRate;

        [Tooltip("Transform where the projectile will be spawned.")]
        [SerializeField] private Transform fireStartPoint;

        [Header("Data")]
        [Tooltip("Direction the weapon is aiming.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;

        [Tooltip("The specific bullet prefab to fire.")]
        [SerializeField] private BulletControllerVariable activeBulletVariable;
        
        [Tooltip("Input signal for firing.")]
        [SerializeField] private BoolVariable isHoldingFireButtonVariable;

        private Coroutine _fireCoroutine;
        private WaitForSeconds _fireDelay;

        private void Awake()
        {
            _fireDelay = new WaitForSeconds(fireRate);
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
                PerformFire();
                yield return _fireDelay;
            }
        }

        private void PerformFire()
        {
            var bulletController = Instantiate(activeBulletVariable.Value, fireStartPoint.position, fireStartPoint.rotation);
            bulletController.Initialize(lookDirectionVariable.Value);
        }
    }
}