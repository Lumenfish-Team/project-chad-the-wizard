using UnityEngine;
using UnityAtoms;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Variable of type `Lumenfish.InputHandling.InputDeviceID`. Inherits from `AtomVariable&lt;Lumenfish.InputHandling.InputDeviceID, InputDeviceIDPair, InputDeviceIDEvent, InputDeviceIDPairEvent, InputDeviceIDInputDeviceIDFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/InputDeviceID", fileName = "InputDeviceIDVariable")]
    public sealed class InputDeviceIDVariable : AtomVariable<InputDeviceID, InputDeviceIDPair, InputDeviceIDEvent, InputDeviceIDPairEvent, InputDeviceIDInputDeviceIDFunction>
    {
        protected override bool ValueEquals(InputDeviceID other)
        {
            return _value == other;
        }
    }
}
