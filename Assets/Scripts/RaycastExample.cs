using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RaycastExample : MonoBehaviour
{
    public Material novoMaterial; // Material que será aplicado aos objetos de madeira
    public float tempoMinimoQuebra = 3f; // Tempo mínimo em segundos para destruir o objeto
    public float intervaloPiscagem = 0.5f; // Intervalo de tempo para alternar entre o material original e o material de piscagem
    public Material materialPiscagem; // Material de piscagem
    public float distanciaMinima = 3f; // Distância mínima entre o jogador e o objeto para poder quebrá-lo
    public GameObject itemPrefab; // Prefab do item a ser dropado

    public Color raycastColor = Color.magenta;

    private Camera mainCamera;
    private RaycastHit hit;
    private Renderer lastRenderer; // Referência ao último objeto alterado
    private Material originalMaterial; // Material original do objeto
    private bool isBreaking; // Flag para controlar se o objeto está sendo quebrado
    private float breakingTimer; // Temporizador para controlar a duração da quebra
    private float blinkTimer; // Temporizador para controlar a piscagem do objeto
    private bool isBlinking; // Flag para controlar a piscagem do objeto
    private List<GameObject> objetosQuebrados; // Lista de objetos que estão sendo quebrados

    private HotbarDisplay hotbarDisplay; // Referência ao script HotbarDisplay

    private void Start()
    {
        mainCamera = Camera.main;
        objetosQuebrados = new List<GameObject>();

        // Encontrar o objeto com o script HotbarDisplay
        hotbarDisplay = FindObjectOfType<HotbarDisplay>();
    }

    private void Update()
    {
        // Verificar se o machado está na mão usando o método do HotbarDisplay
        if (hotbarDisplay != null && hotbarDisplay.IsItemInHand(6))
        {
            // Dispara um raio a partir da posição da câmera na direção em que ela está olhando
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                // Desenha o gizmo da linha roxa
                Debug.DrawLine(mainCamera.transform.position, hit.point, raycastColor);

                // Verifica se o objeto atingido possui a tag "Madeira"
                if (hit.collider.CompareTag("madeira"))
                {
                    // Verifica a distância entre o jogador e o objeto
                    float distancia = Vector3.Distance(transform.position, hit.collider.transform.position);
                    if (distancia <= distanciaMinima)
                    {
                        Renderer renderer = hit.collider.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            // Se for um novo objeto, armazena o material original
                            if (renderer != lastRenderer)
                            {
                                ResetBreakingVariables(); // Reinicia as variáveis de quebra
                                lastRenderer = renderer;
                                originalMaterial = renderer.material;
                            }

                            // Altera o material do objeto para o novo material
                            if (!isBreaking && !isBlinking)
                            {
                                renderer.material = novoMaterial;
                            }

                            // Verifica se o botão esquerdo do mouse está pressionado
                            if (Mouse.current.leftButton.isPressed)
                            {
                                if (!isBreaking)
                                {
                                    breakingTimer += Time.deltaTime;

                                    if (breakingTimer >= tempoMinimoQuebra)
                                    {
                                        isBreaking = true;
                                        renderer.material = materialPiscagem;
                                        StartBlinkAnimation(renderer); // Inicia a animação de piscar
                                    }

                                    Debug.Log("Tempo para quebrar: " + (tempoMinimoQuebra - breakingTimer).ToString("F2") + " segundos");
                                }
                            }
                            else
                            {
                                ResetBreakingVariables(); // Reinicia as variáveis de quebra
                            }

                            // Se está quebrando, destrói o objeto e dropa o item
                            if (isBreaking)
                            {
                                isBlinking = false;
                                objetosQuebrados.Add(hit.collider.gameObject); // Adiciona o objeto à lista de objetos quebrados

                                // Cria um novo objeto na posição onde o bloco foi quebrado
                                GameObject droppedItem = Instantiate(itemPrefab, hit.collider.transform.position, Quaternion.identity);

                                // Define qualquer lógica adicional para o item dropado, se necessário

                                // Destroi o objeto original
                                Destroy(hit.collider.gameObject);
                                lastRenderer = null; // Limpa a referência do último objeto alterado
                            }
                        }
                    }
                    else
                    {
                        // Se o objeto estiver além da distância mínima, retorna o último objeto alterado ao material original
                        if (lastRenderer != null && !isBreaking && !isBlinking)
                        {
                            lastRenderer.material = originalMaterial;
                            lastRenderer = null;
                        }
                    }
                }
                else
                {
                    // Se o objeto atingido não for de madeira, retorna o último objeto alterado ao material original
                    if (lastRenderer != null && !isBreaking && !isBlinking)
                    {
                        lastRenderer.material = originalMaterial;
                        lastRenderer = null;
                    }
                }
            }
            else
            {
                // Se o raio não atingir nenhum objeto, retorna o último objeto alterado ao material original
                if (lastRenderer != null && !isBreaking && !isBlinking)
                {
                    lastRenderer.material = originalMaterial;
                    lastRenderer = null;
                }
            }

            // Lógica de piscagem contínua
            if (isBlinking && lastRenderer != null)
            {
                blinkTimer += Time.deltaTime;
                if (blinkTimer >= intervaloPiscagem)
                {
                    if (lastRenderer.material == originalMaterial)
                    {
                        lastRenderer.material = materialPiscagem;
                    }
                    else
                    {
                        lastRenderer.material = originalMaterial;
                    }
                    blinkTimer = 0f;
                }
            }
        }
    }

    private void StartBlinkAnimation(Renderer renderer)
    {
        isBlinking = true;
        renderer.material = materialPiscagem;
    }

    private void ResetBreakingVariables()
    {
        isBreaking = false;
        breakingTimer = 0f;
        objetosQuebrados.Clear();
    }
}
