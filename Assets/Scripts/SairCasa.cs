using UnityEngine;
using UnityEngine.InputSystem;

public class SairCasa : MonoBehaviour
{
 public GameObject sairCasaTag;
    public GameObject casaEntrarTag;

    private bool proximoSairCasa;
    private bool proximoCasaEntrar;

    public GameObject destinoSairCasa; // Objeto de destino para teletransporte ao sair de casa
    public GameObject destinoCasaEntrar; // Objeto de destino para teletransporte ao entrar na casa

    private void Update()
    {
        // Verifica a proximidade da tag "SairCasa"
        if (Vector3.Distance(transform.position, sairCasaTag.transform.position) < 2f)
        {
            if (!proximoSairCasa)
            {
                proximoSairCasa = true;
                Debug.Log("Chegou perto da tag SairCasa. Pressione E para sair.");
            }
        }
        else
        {
            proximoSairCasa = false;
        }

        // Verifica a proximidade da tag "CasaEntrar"
        if (Vector3.Distance(transform.position, casaEntrarTag.transform.position) < 2f)
        {
            if (!proximoCasaEntrar)
            {
                proximoCasaEntrar = true;
                Debug.Log("Chegou perto da tag CasaEntrar. Pressione E para entrar na casa.");
            }
        }
        else
        {
            proximoCasaEntrar = false;
        }

        // Verifica se o jogador pressionou a tecla E para realizar a ação
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (proximoSairCasa)
            {
                Debug.Log("Saindo...");
                TeleportarJogador(destinoSairCasa);
            }
            else if (proximoCasaEntrar)
            {
                Debug.Log("Entrando na casa...");
                TeleportarJogador(destinoCasaEntrar);
            }
        }
    }

    private void TeleportarJogador(GameObject destino)
    {
        transform.position = destino.transform.position; // Teleporta o jogador para a posição do destino
    }
}