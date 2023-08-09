using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string[] textArray; // Vetor de strings que você deseja passar
    public TextMeshProUGUI displayText; // Referência ao componente TextMeshProUGUI na UI
    private int currentIndex = 0; // Índice atual do vetor
    private bool canPassText = true; // Variável para controlar se o texto pode ser passado
    public GameObject textofdp;

    private void Update()
    {
        if (canPassText && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (currentIndex < textArray.Length)
            {
                string currentText = textArray[currentIndex];
                displayText.text = currentText; // Define o texto a ser exibido no componente TextMeshProUGUI

                currentIndex++; // Incrementa o índice para o próximo texto no próximo pressionamento de tecla
                canPassText = false; // Impede que o texto seja passado novamente até que a tecla seja liberada
                Invoke(nameof(EnablePassText), 0.5f); // Habilita o texto para ser passado novamente após um pequeno atraso
            }
            if(currentIndex == textArray.Length )
            {
                textofdp.SetActive(false);
                Debug.Log("acabou");
            }
        }
    }

    private void EnablePassText()
    {
        canPassText = true;
    }
}