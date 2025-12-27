#if UNITY_2019_1_OR_NEWER
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Event property drawer of type `InputDeviceIDPair`. Inherits from `AtomDrawer&lt;InputDeviceIDPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(InputDeviceIDPairEvent))]
    public class InputDeviceIDPairEventDrawer : AtomDrawer<InputDeviceIDPairEvent> { }
}
#endif
