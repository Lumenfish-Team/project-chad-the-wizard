using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Event of type `InputDeviceIDPair`. Inherits from `AtomEvent&lt;InputDeviceIDPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/InputDeviceIDPair", fileName = "InputDeviceIDPairEvent")]
    public sealed class InputDeviceIDPairEvent : AtomEvent<InputDeviceIDPair>
    {
    }
}
