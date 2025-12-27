using Lumenfish.InputHandling;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Lumenfish.Player.UI
{
    /// <summary>
    /// Calculates position and updates the Crosshair UI.
    /// Should also output the final look direction for the Player.
    /// </summary>
    public interface ICrosshairRenderer
    {
        public void Render(Image crosshairImage, RectTransform rectTransform);
    }

    public class CrosshairManager : MonoBehaviour
    {
        [Header("Strategies")]
        [SerializeField] private CrosshairGamepadRenderer gamepadRenderer;
        [SerializeField] private CrosshairMouseRenderer mouseRenderer;
        
        [Header("Variables")]
        [SerializeField] private InputDeviceIDVariable activeInputDeviceVariable;
        
        private Image _crosshairImage;
        private RectTransform _rectTransform;
        private ICrosshairRenderer _activeRenderer;
        
        private void Awake()
        {
            _crosshairImage = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();

            // Initial setup
            activeInputDeviceVariable.Changed.Register(OnInputDeviceChanged);
            OnInputDeviceChanged(activeInputDeviceVariable.Value);
            
            // Notify the renderer when Cinemachine is done with camera
            CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
        }

        private void OnDestroy()
        {
            activeInputDeviceVariable.Changed.Unregister(OnInputDeviceChanged);
            CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        }
        
        private void OnInputDeviceChanged(InputDeviceID device)
        {
            _activeRenderer = device switch
            {
                InputDeviceID.Gamepad => gamepadRenderer,
                InputDeviceID.MouseKeyboard => mouseRenderer,
                _ => mouseRenderer // Default fallback
            };
        }

        private void OnCameraUpdated(CinemachineBrain brain)
        {
            _activeRenderer.Render(_crosshairImage, _rectTransform);
        }
    }
}