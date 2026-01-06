using UnityAtoms;
using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Event of type `BulletControllerPair`. Inherits from `AtomEvent&lt;BulletControllerPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/BulletControllerPair", fileName = "BulletControllerPairEvent")]
    public sealed class BulletControllerPairEvent : AtomEvent<BulletControllerPair>
    {
    }
}
