using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTools : MonoBehaviour
{
    [SerializeField] private float _rotateSnapAngle = 90f;
    [SerializeField] private float _rayDistance;
    [SerializeField] private LayerMask _buildModeLayerMask;
    [SerializeField] private LayerMask _deleteModeLayerMask;

    [SerializeField] private int _defaultLayerInt = 8;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Material _buildingMatPositive;
    [SerializeField] private Material _buildingMatNegative;
    [SerializeField] public Material _buildingMatInv;


    private bool _deleteModeEnabled;
    [SerializeField] public InventoryItemData itemData;

    private Camera _camera;

    [SerializeField] private Building _spawnedBuilding;
    private Building _targetBuilding;
    private Quaternion _lastRotation;

    public BuildingData Data;
    public bool buildingAtivar = true;
    public bool semente = false;
    public string objectName;

    public HotbarDisplay _hotbarDisplay;
    public GameObject ponto;

    public bool invisivel = false;
    public string hitObjectTag;

    
    private void Start()
    {
        InventoryItemData itemData = FindObjectOfType<InventoryItemData>();
        _camera = Camera.main;
        _spawnedBuilding = new GameObject().AddComponent<Building>();
        ChoosePart(Data);
        
         _spawnedBuilding?.UpdateMaterial(_buildingMatInv);
    }

    public void SetData(BuildingData newData)
    {
        Data = newData;
        ChoosePart(Data);
    }
    private void ChoosePart(BuildingData data){

        if(_deleteModeEnabled){
            if(_targetBuilding != null && _targetBuilding.FlaggedForDelete) _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
            _deleteModeEnabled = false;
        }

        if(_spawnedBuilding != null){
            Destroy(_spawnedBuilding.gameObject);
            _spawnedBuilding = null;
        }

        var go = new GameObject{
            layer = _defaultLayerInt,
            name = "Build Preview"
        };
       
        _spawnedBuilding = go.AddComponent<Building>();
        _spawnedBuilding.Init(data);
       
        _spawnedBuilding.transform.rotation = _lastRotation;
        
    }
     public float raycastDistance = 10f;
    private void Update()
    {
        if (IsRayHittingSomething(out hitObjectTag))
        {
           // Debug.Log("Tag do objeto atingido: " + hitObjectTag);
        }
        else
        {
            //Debug.Log("Nenhum objeto atingido");
        }
        ReadRaycastObjectNames();
        if(buildingAtivar)
        {
        BuildModeLogic();
        //if(Keyboard.current.qKey.wasPressedThisFrame) _deleteModeEnabled = !_deleteModeEnabled;
        //if(_deleteModeEnabled) DeleteModeLogic();
        //else BuildModeLogic();
        }
    }
    private bool IsRayHittingSomething(out string hitObjectTag)
    {
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
        Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            hitObjectTag = hit.collider.gameObject.tag;
            return true;
        }
        
        hitObjectTag = null;
        return false;
    }
    //TODO: Pensar em outra maneira, pois aqui estou desativando o collider chando o metodo
    public void DesativarBox()
    {
        foreach (BoxCollider childCollider in _spawnedBuilding.GetComponentsInChildren<BoxCollider>())
        {
            childCollider.enabled = false;
        }
    }
    public void AtivarBox()
    {
        foreach (BoxCollider childCollider in _spawnedBuilding.GetComponentsInChildren<BoxCollider>())
        {
            childCollider.enabled = true;
        }
    }
    private bool IsRayHittingSomething(LayerMask layerMask, out RaycastHit hitInfo)
    {
       
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
           
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }
    
    private void DeleteModeLogic()
    {

    }

private void ReadRaycastObjectNames()
{
    if (IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
    {
        objectName = hitInfo.collider.gameObject.name;
       Debug.Log("Objeto olhado: " + objectName); 

        if(objectName == "Farming")
         {
             _spawnedBuilding.UpdateMaterial(_buildingMatPositive);
        
         }else
         {
            
             //TODO: Pensar em outra maneira, estou deixando o material invisivel
             if(invisivel)
             {
                
                 _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
             }
             else
             {
             //_targetBuilding.BoxCollider();
            
             _spawnedBuilding.UpdateMaterial(_buildingMatInv);

             }

         }

    }
}


private void BuildModeLogic()
{
    if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
    {
        _targetBuilding.RemoveDeleteFlag();
        _targetBuilding = null;
    }
    
    if (_spawnedBuilding == null) return;

    PositionBuildingPreview();
    if (Data.plantacao && !IsRayHittingSomething(LayerMask.GetMask("farming"), out RaycastHit hitInfo))
    {
        // A camada correta não está sendo atingida, portanto, não permita a plantação.
        _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
        return;
    }
   
}
    private void PositionBuildingPreview()
    {
    if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _spawnedBuilding.transform.Rotate(0, _rotateSnapAngle, 0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if (IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
        {
            var gridPosition = WordGrid.GridPositionFronWorldPosition3D(hitInfo.point, 1f);
            _spawnedBuilding.transform.position = gridPosition;

            // // Verifica se a data.plantacao é true e se o objeto atingido pertence à camada "farming".
            // if (Data.plantacao && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Farming"))
            // {
            //     _spawnedBuilding.UpdateMaterial(_buildingMatPositive);
            // }
            // else
            // {
            //     _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
            //     return; // Impede a colocação do bloco se a condição não for atendida.
            // }
        if(objectName == "Farming")
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && !_spawnedBuilding.IsOverlapping)
            {
                    
                        _spawnedBuilding.UpdateMaterial(_buildingMatPositive);
                        _spawnedBuilding.PlaceBuilding();
                        var dataCopy = _spawnedBuilding.AssignedData;
                       _spawnedBuilding = null;
                        Debug.Log(dataCopy);
                        ChoosePart(dataCopy);
                        buildingAtivar = true;
                        _hotbarDisplay.ClearSelectedItem(); //para limpar o item da hotbar

            }   
            
        } else 
        {

            _spawnedBuilding.UpdateMaterial(_buildingMatNegative);

        }
            }
        }
    }
