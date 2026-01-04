using Unity.Cinemachine;
using UnityEngine;

namespace Lumenfish.CameraSystem
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        private CinemachineCamera _cinemachineCamera;

        public void Initialize(CameraTargetController cameraTargetController)
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            _cinemachineCamera.Target.TrackingTarget = cameraTargetController.transform;
        }
    }
}