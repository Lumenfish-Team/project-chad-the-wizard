#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Variable property drawer of type `Lumenfish.WeaponSystem.BulletController`. Inherits from `AtomDrawer&lt;BulletControllerVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BulletControllerVariable))]
    public class BulletControllerVariableDrawer : VariableDrawer<BulletControllerVariable> { }
}
#endif
