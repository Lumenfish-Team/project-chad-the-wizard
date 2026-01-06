#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Event property drawer of type `Lumenfish.WeaponSystem.BulletController`. Inherits from `AtomDrawer&lt;BulletControllerEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BulletControllerEvent))]
    public class BulletControllerEventDrawer : AtomDrawer<BulletControllerEvent> { }
}
#endif
