using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform firstPersonCameraPivot;
    public Transform thirdPersonCameraPivot;
    public Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAngle;
    public float pivotAngle;
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    public float cameraSensitivity = 1.0f;
    public bool isCursorLocked = true;
    public bool inventoryIsOpen = false;
    public bool isFirstPersonView = false;

    public GameObject prefab;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    private void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame && !inventoryIsOpen)
        {
            Debug.Log("Abrir!");
            Cursor.lockState = CursorLockMode.None;
            isCursorLocked = false;
            inventoryIsOpen = true;
        }
        else if (Keyboard.current.jKey.wasPressedThisFrame && inventoryIsOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            isCursorLocked = true;
            Debug.Log("Fechar!");
            inventoryIsOpen = false;
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            isFirstPersonView = !isFirstPersonView;
            SwitchCameraView();
        }
    }

    public void HandleCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    public void RotateCamera()
    {
        if (!isCursorLocked)
        {
            return;
        }

        lookAngle += inputManager.cameraInputX * cameraLookSpeed * cameraSensitivity;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed * cameraSensitivity;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        if (isFirstPersonView)
        {
            firstPersonCameraPivot.localRotation = targetRotation;
        }
        else
        {
            thirdPersonCameraPivot.localRotation = targetRotation;
        }
    }

    private void SwitchCameraView()
    {
        if (isFirstPersonView)
        {
            prefab.SetActive(false);
            firstPersonCameraPivot.gameObject.SetActive(true);
            thirdPersonCameraPivot.gameObject.SetActive(false);
        }
        else
        {
            prefab.SetActive(true);
            firstPersonCameraPivot.gameObject.SetActive(false);
            thirdPersonCameraPivot.gameObject.SetActive(true);
        }
    }
}
