using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ConsertarTrigger : MonoBehaviour
{
    public ConsertarData consertarData;
    public string playerTag = "Player"; // Tag do objeto com o personagem principal
    public GameObject playerObject; // Objeto com a tag "Player"
    public float distanciaMaxima = 3f; // Distância máxima para considerar o player certo

    public GameObject prefabConsertado; // Novo prefab a ser ativado após o conserto
    public GameObject currentPrefab; // Prefab atual

    private Transform personagem; // Transform do personagem principal
    public HotbarDisplay hotbarDisplay;
    public bool check = false; //
    public string recipeName;

    public TextMeshProUGUI uiText; // Referência para o componente de texto da UI
    private bool isTextVisible = false; // Controla a visibilidade do texto
    private bool isFixed = false; // Controla se o problema já foi consertado

    private void Start()
    {
        hotbarDisplay = FindObjectOfType<HotbarDisplay>();
        playerObject = GameObject.FindGameObjectWithTag(playerTag); // Encontra o objeto com base na tag
        if (playerObject != null)
        {
            personagem = playerObject.transform; // Atribui o transform do personagem
        }
        else
        {
            Debug.LogError("Objeto com a tag 'Player' não encontrado!");
        }

        // Configurações iniciais do componente de texto da UI
        uiText.text = "";
        uiText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (personagem == null || isFixed)
            return;

        float distancia = Vector3.Distance(transform.position, personagem.position); // Calcula a distância entre o objeto com o script e o personagem

        if (distancia <= distanciaMaxima)
        {
            bool hasAllItems = true;
            Dictionary<string, int> missingItems = new Dictionary<string, int>();

            foreach (ItemConsertarData requirement in consertarData.requirements)
            {
                int availableQuantity = hotbarDisplay.GetItemCount(requirement.id);
                if (availableQuantity < requirement.quantity)
                {
                    hasAllItems = false;
                    missingItems.Add(requirement.item.name, requirement.quantity - availableQuantity);
                }
            }

            if (hasAllItems)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    foreach (ItemConsertarData requirement in consertarData.requirements)
                    {
                        int remainingQuantity = requirement.quantity;
                        while (remainingQuantity > 0)
                        {
                            int itemCount = hotbarDisplay.GetItemCount(requirement.id);
                            int quantityToRemove = Mathf.Min(itemCount, remainingQuantity);
                            hotbarDisplay.RemoveItem(requirement.id, quantityToRemove);
                            remainingQuantity -= quantityToRemove;
                        }
                    }

                    Debug.Log("Itens removidos do inventário. Problema consertado.");

                    // Atualizar o texto da UI
                    uiText.text = "Itens removidos do inventário. Problema consertado.";

                    // Exibir o texto da UI
                    uiText.gameObject.SetActive(true);
                    isTextVisible = true;

                    // Marcar o problema como consertado
                    isFixed = true;

                    // Agendar a função para ocultar o texto após 2 segundos
                    StartCoroutine(HideUITextAfterDelay(2f));

                    // Ativar o novo prefab
                    GameObject newPrefab = Instantiate(prefabConsertado, transform.position, transform.rotation);
                    currentPrefab.SetActive(false);
                    currentPrefab = newPrefab;
                    prefabConsertado.SetActive(true);;
                    
                }
                else
                {
                    // Limpar o texto da UI se nenhum evento de conserto estiver ocorrendo
                    if (isTextVisible)
                    {
                        uiText.text = "";
                        uiText.gameObject.SetActive(false);
                        isTextVisible = false;
                    }
                }
            }
            else
            {
                string missingItemsString = "";
                foreach (KeyValuePair<string, int> item in missingItems)
                {
                    if (item.Value == 1)
                    {
                        missingItemsString += item.Key + " (1), ";
                    }
                    else
                    {
                        missingItemsString += item.Key + " (x" + item.Value + "), ";
                    }
                }
                missingItemsString = missingItemsString.TrimEnd(',', ' ');
                Debug.Log("Faltam os seguintes itens no inventário: " + missingItemsString);

                // Atualizar o texto da UI
                uiText.text = "Voce precisa de " + missingItemsString  + " para consertar";

                // Exibir o texto da UI
                uiText.gameObject.SetActive(true);
                isTextVisible = true;
            }
        }
        else
        {
            // Ocultar o texto da UI se o jogador estiver longe demais
            if (isTextVisible)
            {
                uiText.text = "";
                uiText.gameObject.SetActive(false);
                isTextVisible = false;
            }
        }
    }

    private IEnumerator HideUITextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isTextVisible)
        {
            uiText.text = "";
            uiText.gameObject.SetActive(false);
            isTextVisible = false;
        }
    }
}
