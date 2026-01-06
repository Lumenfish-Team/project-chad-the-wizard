using System;
using Lumenfish.InputHandling;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Lumenfish.CameraSystem
{
    public class CameraTargetController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reads the player's current position from the Atoms variable.")]
        [SerializeField] private Vector2Variable playerPositionVariable;
        
        [Tooltip("Input variable for look direction.")]
        [SerializeField] private Vector2Variable lookDirectionVariable;
        
        [Tooltip("Input variable for movement direction.")]
        [SerializeField] private Vector2Variable moveDirectionVariable;

        [Header("Input Mode")]
        [Tooltip("Used to determine if we should use mouse logic (distance based) or gamepad logic (direction based).")]
        [SerializeField] private InputDeviceIDVariable activeInputDeviceVariable;

        [Tooltip("Raw mouse position to calculate distance from player.")]
        [SerializeField] private Vector2Variable mouseScreenPositionVariable;

        [Header("Aim Settings")] 
        [Tooltip("The maximum distance the camera shifts towards the crosshair/aim direction.")]
        [SerializeField] private float aimLeadDistance;
        
        [Tooltip("Smoothing time for the aim offset. Lower values make it snappier.")]
        [SerializeField] private float aimLeadSmoothTime;
        
        [Header("Mouse Specific")]
        [Tooltip("If the mouse cursor is within this radius of the player, the camera will not lead.")]
        [SerializeField] private float mouseDeadzoneRadius;

        [Header("Movement Settings")] 
        [Tooltip("The maximum distance the camera shifts in the direction of movement.")]
        [SerializeField] private float movementLeadDistance;
        
        [Tooltip("Smoothing time for the movement offset. Higher values feel heavier/smoother.")]
        [SerializeField] private float movementLeadSmoothTime;
        
        private Vector3 _currentAimOffset;
        private Vector3 _aimVelocity;
        private Vector3 _currentMoveOffset;
        private Vector3 _moveVelocity;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            var targetAimOffset = GetRawAimOffset();
            var targetMoveOffset = GetRawMoveOffset();
            
            _currentAimOffset = Vector3.SmoothDamp(_currentAimOffset, targetAimOffset, ref _aimVelocity, 
                aimLeadSmoothTime); 
            
            _currentMoveOffset = Vector3.SmoothDamp(_currentMoveOffset, targetMoveOffset, ref _moveVelocity, 
                movementLeadSmoothTime);
            
            // Cast to Vector3 to ensure z-axis compatibility
            Vector3 playerPos = playerPositionVariable.Value;
            var finalPosition = playerPos + _currentAimOffset + _currentMoveOffset;
            finalPosition.z = transform.position.z; // Keep camera's original Z

            transform.position = finalPosition;
        }

        private Vector3 GetRawAimOffset()
        {
            return activeInputDeviceVariable.Value switch
            {
                InputDeviceID.Gamepad => GetGamepadOffset(),
                InputDeviceID.MouseKeyboard => GetMouseOffset(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Vector3 GetMouseOffset()
        {
            var mouseScreenPos = mouseScreenPositionVariable.Value;
            var mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -_mainCamera.transform.position.z));
            var playerPos = playerPositionVariable.Value;

            // Calculate difference vector
            var difference = (Vector2)mouseWorldPos - playerPos;
            var distance = difference.magnitude;

            // If inside deadzone, return zero offset (center camera)
            if (distance < mouseDeadzoneRadius) return Vector3.zero;

            // Otherwise, clamp the offset to the max lead distance.
            return Vector3.ClampMagnitude(difference, aimLeadDistance);
        }

        private Vector3 GetGamepadOffset()
        {
            if (lookDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            return lookDirectionVariable.Value.normalized * aimLeadDistance;
        }

        private Vector3 GetRawMoveOffset()
        {
            if (moveDirectionVariable.Value == Vector2.zero) return Vector3.zero;
            
            return moveDirectionVariable.Value.normalized * movementLeadDistance;
        }
    }
}