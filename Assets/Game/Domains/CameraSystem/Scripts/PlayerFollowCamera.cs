using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Lumenfish.CameraSystem
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        private CinemachineCamera _cinemachineCamera;
        
        [Inject]
        public void Construct(CameraTargetController cameraTargetController)
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            _cinemachineCamera.Target.TrackingTarget = cameraTargetController.transform;
        }
    }
}