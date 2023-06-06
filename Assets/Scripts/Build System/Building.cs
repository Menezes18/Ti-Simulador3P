using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{

    private BuildingData _assignedData;
    private BoxCollider _boxCollider;
    private GameObject _graphic;
    private Transform _colliders;
    private Renderer _renderer;
    private Material _defaultMaterial;


    private bool _flaggedForDelete;
    public bool FlaggedForDelete => _flaggedForDelete;
    private bool _isOverlapping;

    public BuildingData AssignedData => _assignedData;
    public BuildTools buildTools;

    public bool IsOverlapping => _isOverlapping;


    public bool t = false;


    public void Init(BuildingData data)
    {

    
        _assignedData = data; 
        _boxCollider = GetComponent<BoxCollider>(); 
        _boxCollider.size = _assignedData.BuildingSize; 
        _boxCollider.center = new Vector3(0, (_assignedData.BuildingSize.y + .2f) * 0.5f, 0); 
        _boxCollider.isTrigger = true; 
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; 

        Debug.LogWarning("Building");
        _graphic = Instantiate(data.Prefab, transform); // Instancia o prefab gráfico do prédio como um filho deste objeto.
        
        _renderer = _graphic.GetComponentInChildren<Renderer>(); 
        _defaultMaterial = _renderer.material; // Armazena o material padrão do objeto gráfico.
        _colliders = _graphic.transform.Find("Colliders"); // Procura por um objeto filho chamado "Colliders".
        if (_colliders != null) _colliders.gameObject.SetActive(false); // Desativa o objeto "Colliders", se existir. */

        //DesativarPreview();
    }
    public void PlaceBuilding()
    {   
        BuildTools buildTools = FindObjectOfType<BuildTools>(); // Encontra o objeto BuildTools na cena.
         if (_isOverlapping)
        {
            Debug.Log("Não é possível colocar o bloco porque está sobreposto a outro bloco.");
            return;
        }
        _boxCollider.enabled = false; 
        if (_colliders != null) _colliders.gameObject.SetActive(true); // Ativa o objeto "Colliders", se existir.
        //UpdateMaterial(_defaultMaterial); 
        UpdateMaterial(_defaultMaterial);
        gameObject.layer = 7; 
        gameObject.name = _assignedData.DisplayName + " - " + transform.position; // Define o nome do objeto com base no nome do prédio e em sua posição.
        _graphic.GetComponent<BoxCollider>().isTrigger = true;
       
    }


    public void UpdateMaterial(Material newMaterial)
    {
        if(_renderer.material != newMaterial) _renderer.material = newMaterial;
    }

    public void BoxCollider()
    {
    }

    public void FlagForDelete(Material deleleMat)
    {
        UpdateMaterial(deleleMat);
        _flaggedForDelete = true;


    }

    public void RemoveDeleteFlag()
    {
        UpdateMaterial(_defaultMaterial);
        _flaggedForDelete = false;
    }
    
    private void OnTriggerStay(Collider other){
        _isOverlapping = true;
    }

    private void OnTriggerExit(Collider other){
        _isOverlapping = false;
    }   
}
