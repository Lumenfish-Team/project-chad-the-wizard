using System;
using UnityAtoms;
using UnityEngine;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// IPair of type `&lt;Lumenfish.InputHandling.InputDeviceID&gt;`. Inherits from `IPair&lt;Lumenfish.InputHandling.InputDeviceID&gt;`.
    /// </summary>
    [Serializable]
    public struct InputDeviceIDPair : IPair<InputDeviceID>
    {
        public InputDeviceID Item1 { get => _item1; set => _item1 = value; }
        public InputDeviceID Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private InputDeviceID _item1;
        
        [SerializeField]
        private InputDeviceID _item2;

        public void Deconstruct(out InputDeviceID item1, out InputDeviceID item2) { item1 = Item1; item2 = Item2; }
    }
}