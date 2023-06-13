using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AcessarCraft : MonoBehaviour
{
    public Transform player;
    public Transform objeto;
    public float distanciaMinima = 2f; 

    private bool podeInteragir = false;

    public GameObject craftUI;
    private bool isUIOpen = false;

    void Update()
    {
        float distancia = Vector3.Distance(player.position, objeto.position);

        if (distancia <= distanciaMinima)
        {
            podeInteragir = true;
        }
        else
        {
            podeInteragir = false;
        }

        if (podeInteragir && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isUIOpen)
            {
                CloseUI();
            }
            else
            {
                OpenUI();
            }
        }   
    }

    void OpenUI()
    {
        craftUI.SetActive(true);
        isUIOpen = true;
    }

    void CloseUI()
    {
        craftUI.SetActive(false);
        isUIOpen = false;
    }
}