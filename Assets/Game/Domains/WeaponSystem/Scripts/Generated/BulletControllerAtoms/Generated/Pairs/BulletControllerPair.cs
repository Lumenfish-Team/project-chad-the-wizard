using System;
using UnityAtoms;
using UnityEngine;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// IPair of type `&lt;Lumenfish.WeaponSystem.BulletController&gt;`. Inherits from `IPair&lt;Lumenfish.WeaponSystem.BulletController&gt;`.
    /// </summary>
    [Serializable]
    public struct BulletControllerPair : IPair<BulletController>
    {
        public BulletController Item1 { get => _item1; set => _item1 = value; }
        public BulletController Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private BulletController _item1;
        [SerializeField]
        private BulletController _item2;

        public void Deconstruct(out BulletController item1, out BulletController item2) { item1 = Item1; item2 = Item2; }
    }
}