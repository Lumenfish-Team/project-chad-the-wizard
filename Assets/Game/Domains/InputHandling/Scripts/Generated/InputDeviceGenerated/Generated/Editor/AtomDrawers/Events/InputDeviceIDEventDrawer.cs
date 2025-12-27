#if UNITY_2019_1_OR_NEWER
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Event property drawer of type `Lumenfish.InputHandling.InputDeviceID`. Inherits from `AtomDrawer&lt;InputDeviceIDEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(InputDeviceIDEvent))]
    public class InputDeviceIDEventDrawer : AtomDrawer<InputDeviceIDEvent> { }
}
#endif
