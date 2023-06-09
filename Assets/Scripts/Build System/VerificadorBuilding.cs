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
        enxada = 4,
        madeira = 7,
        sementeAbobora = 10,
        sementeBatata = 12,
        sementeBeterraba = 14,
        sementeCenoura = 16,
        sementeMelao = 18,
        sementePimenta = 20,
        sementeTrigo = 22,
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

            //Debug.Log("O número está presente no enum!");
            _buildTools.buildingAtivar = true;
            _buildBuilding.t = true;
            
           // _buildBuilding.ativarPreview();
            _hotbarDisplay.SetDataBuilding();

        }
        else
        {

            //Debug.Log("O número NÃO está presente no enum.");
            _hotbarDisplay.ClearSelectedItem();
            
        }
    }

    public void VerificarEnumPreview(int numero)
    {
        // Verifica se o número está presente no enum
        
        if (System.Enum.IsDefined(typeof(Itens), numero))
        {
            //Debug.Log("A");
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

           // Debug.Log("O número NÃO está presente no enum.");
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
