#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Event property drawer of type `BulletControllerPair`. Inherits from `AtomDrawer&lt;BulletControllerPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(BulletControllerPairEvent))]
    public class BulletControllerPairEventDrawer : AtomDrawer<BulletControllerPairEvent> { }
}
#endif
