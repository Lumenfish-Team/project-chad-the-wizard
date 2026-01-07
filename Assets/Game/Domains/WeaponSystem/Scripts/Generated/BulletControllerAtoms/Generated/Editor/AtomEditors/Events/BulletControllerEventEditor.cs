#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Event property drawer of type `Lumenfish.WeaponSystem.BulletController`. Inherits from `AtomEventEditor&lt;Lumenfish.WeaponSystem.BulletController, BulletControllerEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BulletControllerEvent))]
    public sealed class BulletControllerEventEditor : AtomEventEditor<BulletController, BulletControllerEvent> { }
}
#endif
