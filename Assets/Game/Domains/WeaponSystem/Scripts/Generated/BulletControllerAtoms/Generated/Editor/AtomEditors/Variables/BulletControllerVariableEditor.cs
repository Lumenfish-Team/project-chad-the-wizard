using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Variable Inspector of type `Lumenfish.WeaponSystem.BulletController`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(BulletControllerVariable))]
    public sealed class BulletControllerVariableEditor : AtomVariableEditor<BulletController, BulletControllerPair> { }
}
