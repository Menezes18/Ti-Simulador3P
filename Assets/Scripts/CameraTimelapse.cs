using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTimelapse : MonoBehaviour
{
    public float movementSpeed = 1.0f; // Velocidade de movimento da câmera
    public float rotationSpeed = 1.0f; // Velocidade de rotação da câmera
    public float flightSpeed = 1.0f; // Velocidade de voo da câmera
    public float lookSpeed = 2.0f; // Velocidade de rotação da câmera com o mouse

    private bool isRecording = false;
    private bool isFastForward = false;
    private Vector2 lookInput;
    private Vector2 moveInput;
    private float timeScale;
    public float sensitivity = 2f; // Sensibilidade do mouse

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
        Cursor.visible = false; // Oculta o cursor
    }


    private Vector2 mouseDelta;

    private void Update()
    {
        // Rotação da câmera no eixo Y
        transform.Rotate(Vector3.up, mouseDelta.x * sensitivity);

        // Rotação da câmera no eixo X
        transform.Rotate(Vector3.left, mouseDelta.y * sensitivity);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}