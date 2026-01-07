using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    [CreateAssetMenu(fileName = "NewWeaponModel", menuName = "Lumenfish/Weapon System/New Weapon Model")]
    public class WeaponModel : ScriptableObject
    {
        [Header("Firing Logic")] 
        [Tooltip("Time in seconds between shots.")]
        [SerializeField] private float fireRate;

        [Tooltip("Number of projectiles fired per shot (e.g., 3 for shotgun).")]
        [Min(1)]
        [SerializeField] private int projectilesPerShot;

        [Tooltip("Total angle of the spread in degrees.")]
        [Range(0f, 360f)]
        [SerializeField] private float spreadAngle;

        public float GetFireRate()
        {
            return fireRate;
        }

        public int GetProjectilesPerShot()
        {
            return projectilesPerShot;
        }

        public float GetSpreadAngle()
        {
            return spreadAngle;
        }
    }
}