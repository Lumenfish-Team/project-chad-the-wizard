#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using Lumenfish.WeaponSystem;

namespace Lumenfish.WeaponSystem
{
    /// <summary>
    /// Event property drawer of type `BulletControllerPair`. Inherits from `AtomEventEditor&lt;BulletControllerPair, BulletControllerPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(BulletControllerPairEvent))]
    public sealed class BulletControllerPairEventEditor : AtomEventEditor<BulletControllerPair, BulletControllerPairEvent> { }
}
#endif
