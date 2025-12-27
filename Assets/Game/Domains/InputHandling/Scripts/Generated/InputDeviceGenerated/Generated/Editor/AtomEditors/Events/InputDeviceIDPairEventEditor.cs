#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Lumenfish.InputHandling;
using UnityAtoms.BaseAtoms;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Event property drawer of type `InputDeviceIDPair`. Inherits from `AtomEventEditor&lt;InputDeviceIDPair, InputDeviceIDPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(InputDeviceIDPairEvent))]
    public sealed class InputDeviceIDPairEventEditor : AtomEventEditor<InputDeviceIDPair, InputDeviceIDPairEvent> { }
}
#endif
