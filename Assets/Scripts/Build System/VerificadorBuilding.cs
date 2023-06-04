using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificadorBuilding : MonoBehaviour
{
    public int numeroVerificar = -1;
    public HotbarDisplay _hotbarDisplay;
    public BuildTools _buildTools;
    public Building _buildBuilding;
    enum Itens
    {
        Numero1 = 1,
        Numero2 = 2,
        Numero3 = 0,
        po = 40
    }


    public void Awake()
    {
        _buildTools = FindObjectOfType<BuildTools>();
    }
    public void VerificarEnum(int numero)
    {
        // Verifica se o número está presente no enum
        if (System.Enum.IsDefined(typeof(Itens), numero))
        {
            // Mostra uma mensagem de debug
            Debug.Log("O número está presente no enum!");
            _buildTools.buildingAtivar = true;
            _buildBuilding.t = true;
            
           // _buildBuilding.ativarPreview();
            _hotbarDisplay.SetDataBuilding();

        }
        else
        {
            // Mostra uma mensagem de debug
            Debug.Log("O número NÃO está presente no enum.");
            _hotbarDisplay.ClearSelectedItem();
            
        }
    }

    public void VerificarEnumPreview(int numero)
    {
        // Verifica se o número está presente no enum
        if (System.Enum.IsDefined(typeof(Itens), numero))
        {
            Debug.Log("A");
            _buildBuilding.t = true;
            //_buildBuilding.ativarPreview();
            //_buildBuilding.Init();
           // _buildBuilding.ativarPreview();
           //TODO: Pensar em outra maneira, estou deixando o material invisivel
            _buildTools.invisivel = true;
            _buildTools.buildingAtivar = true;
            _buildTools.AtivarBox();
        }
        else
        {
            // Mostra uma mensagem de debug
            Debug.Log("O número NÃO está presente no enum.");
            //_buildBuilding._colliders.gameObject.SetActive(false);
            _buildTools.DesativarBox();
            _buildTools.invisivel = false;
           // _buildBuilding.DesativarPreview();
            _buildTools.buildingAtivar = false;
            
        }
    }

    void Start()
    {
        
        // Exemplo de uso do método
        //VerificarEnum(numeroVerificar);
    }

    public void Update() {
       // VerificarEnum(numeroVerificar);
    }

}
