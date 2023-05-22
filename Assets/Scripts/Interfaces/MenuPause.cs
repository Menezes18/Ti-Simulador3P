using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuPause : MonoBehaviour
{
    public Transform pauseMenu;

    void Start()
    {

    }

    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 1;
            }


        }
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackMenu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        Debug.Log("a");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Fechou o Jogo");
        Application.Quit();
    }
}
