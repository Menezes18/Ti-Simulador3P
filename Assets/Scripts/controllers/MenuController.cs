using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
     public string nomeDoLevelDeJogo;
     public GameObject panelMenuInicial;
     public GameObject panelOptions;
     public GameObject panelCredits;


    public void NewGame()
    {
        SceneManager.LoadScene("Game 3");
        // SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        panelMenuInicial.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        panelOptions.SetActive(false);
        panelMenuInicial.SetActive(true);
        //

    }

    public void OpenCredits()
    {
        panelMenuInicial.SetActive(false);
        panelCredits.SetActive(true);

    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
        panelMenuInicial.SetActive(true);
    }

    
    public void QuitGame()
    {
        Debug.Log("Fechou o Jogo");
        Application.Quit();
    }

}
