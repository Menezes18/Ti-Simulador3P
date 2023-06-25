using UnityEngine;
using UnityEngine.InputSystem;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxMaterial1;
    public Material skyboxMaterial2;
    public Light sunLight;
    public Light moonLight;
    public float rotationSpeed = 1f;
    public float rotationThreshold = 90f;

    private bool isSunActive = true;

    private void Start()
    {
        RenderSettings.skybox = skyboxMaterial1; // Define o skybox inicial
        sunLight.enabled = true; // Ativa a luz do sol
        moonLight.enabled = false; // Desativa a luz da lua
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            ChangeSkybox(skyboxMaterial1); // Mudar para o skybox 1 quando a tecla "K" for pressionada
        }
        else if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeSkybox(skyboxMaterial2); // Mudar para o skybox 2 quando a tecla "L" for pressionada
        }

        RotateSkybox();
    }

    private void ChangeSkybox(Material newSkybox)
    {
        RenderSettings.skybox = newSkybox;

        if (newSkybox == skyboxMaterial1)
        {
            isSunActive = true;
            sunLight.enabled = true;
            moonLight.enabled = false;
        }
        else if (newSkybox == skyboxMaterial2)
        {
            isSunActive = false;
            sunLight.enabled = false;
            moonLight.enabled = true;
        }
    }

    private void RotateSkybox()
    {
        if (isSunActive)
        {
            float rotation = Time.time * rotationSpeed;
            RenderSettings.skybox.SetFloat("_Rotation", rotation);

            if (rotation >= rotationThreshold)
            {
                ChangeSkybox(skyboxMaterial2); // Mudar para o skybox 2 (lua) quando o sol completar uma rotação
            }
        }
    }
}
