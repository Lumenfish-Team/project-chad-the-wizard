#if UNITY_2019_1_OR_NEWER
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Variable property drawer of type `Lumenfish.InputHandling.InputDeviceID`. Inherits from `AtomDrawer&lt;InputDeviceIDVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(InputDeviceIDVariable))]
    public class InputDeviceIDVariableDrawer : VariableDrawer<InputDeviceIDVariable> { }
}
#endif
