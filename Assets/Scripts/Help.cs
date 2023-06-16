using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Help : MonoBehaviour
{
 private bool isOpen = false;
 public GameObject help;

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            if (isOpen)
            {
                help.SetActive(false);
                Debug.Log("Fechou");
                isOpen = false;
            }
            else
            {
                help.SetActive(true);
                Debug.Log("Abriu");
                isOpen = true;
            }
        }
    }
}
