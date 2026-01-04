using Lumenfish.CameraSystem;
using UnityEngine;

namespace Lumenfish.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private PlayerController playerControllerPrefab;
        [SerializeField] private CameraTargetController cameraTargetControllerPrefab;
        [SerializeField] private PlayerFollowCamera  playerFollowCameraPrefab;

        private void Start()
        {
            Instantiate(playerControllerPrefab);
            var cameraTargetController = Instantiate(cameraTargetControllerPrefab);
            
            var playerFollowCamera = Instantiate(playerFollowCameraPrefab);
            playerFollowCamera.Initialize(cameraTargetController);
            
            Destroy(gameObject);
        }
    }
}
