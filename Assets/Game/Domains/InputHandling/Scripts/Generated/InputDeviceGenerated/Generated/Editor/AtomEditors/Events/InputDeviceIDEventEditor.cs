#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Lumenfish.InputHandling;
using UnityAtoms.BaseAtoms;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Event property drawer of type `Lumenfish.InputHandling.InputDeviceID`. Inherits from `AtomEventEditor&lt;Lumenfish.InputHandling.InputDeviceID, InputDeviceIDEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(InputDeviceIDEvent))]
    public sealed class InputDeviceIDEventEditor : AtomEventEditor<InputDeviceID, InputDeviceIDEvent> { }
}
#endif
