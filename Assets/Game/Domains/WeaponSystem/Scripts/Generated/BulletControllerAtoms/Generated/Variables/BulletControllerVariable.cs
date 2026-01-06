using UnityEngine;
using System;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Variable of type `Lumenfish.WeaponSystem.BulletController`. Inherits from `AtomVariable&lt;Lumenfish.WeaponSystem.BulletController, BulletControllerPair, BulletControllerEvent, BulletControllerPairEvent, BulletControllerBulletControllerFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/BulletController", fileName = "BulletControllerVariable")]
    public sealed class BulletControllerVariable : AtomVariable<BulletController, BulletControllerPair, BulletControllerEvent, BulletControllerPairEvent, BulletControllerBulletControllerFunction>
    {
        protected override bool ValueEquals(BulletController other)
        {
            return other == _value;
        }
    }
}
