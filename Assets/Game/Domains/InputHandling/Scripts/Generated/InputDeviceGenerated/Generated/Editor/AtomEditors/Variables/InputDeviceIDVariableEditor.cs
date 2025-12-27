using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.InputHandling
{
    /// <summary>
    /// Variable Inspector of type `Lumenfish.InputHandling.InputDeviceID`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(InputDeviceIDVariable))]
    public sealed class InputDeviceIDVariableEditor : AtomVariableEditor<InputDeviceID, InputDeviceIDPair> { }
}
