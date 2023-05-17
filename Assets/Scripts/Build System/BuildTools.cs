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


    private bool _deleteModeEnabled;

    private Camera _camera;

    [SerializeField] private Building _spawnedBuilding;
    private Building _targetBuilding;
    private Quaternion _lastRotation;

    public BuildingData Data;
    public bool buildingAtivar = false;
    public string objectName;
    
    private void Start()
    {
        _camera = Camera.main;
        ChoosePart(Data);
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

    private void Update()
    {
        ReadRaycastObjectNames();
        if(buildingAtivar)
        {
        if(Keyboard.current.qKey.wasPressedThisFrame) _deleteModeEnabled = !_deleteModeEnabled;
        if(_deleteModeEnabled) DeleteModeLogic();
        else BuildModeLogic();
        }
    }

    private bool IsRayHittingSomething(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }
    
    private void DeleteModeLogic()
    {
        if (IsRayHittingSomething(_deleteModeLayerMask, out RaycastHit hitInfo))
        {
            var delectedBuilding = hitInfo.collider.gameObject.GetComponentInParent<Building>();

            if(delectedBuilding == null) return;
            if (_targetBuilding == null) _targetBuilding = delectedBuilding;
            if(delectedBuilding != _targetBuilding && _targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.RemoveDeleteFlag();
                _targetBuilding = delectedBuilding;
            }

            if(delectedBuilding == _targetBuilding && !_targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.FlagForDelete(_buildingMatNegative);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {

                Destroy(_targetBuilding.gameObject);
                _targetBuilding = null; 

            }

        }
        else
        {
            if(_targetBuilding != null && _targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.RemoveDeleteFlag();
                _targetBuilding = null;
            }
        }
    
    }  
private void ReadRaycastObjectNames()
{
    if (IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
    {
        objectName = hitInfo.collider.gameObject.name;
        Debug.Log("Objeto olhado: " + objectName);
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
    // Resto do código para posicionar o bloco
}
    private void PositionBuildingPreview()
    {
        _spawnedBuilding.UpdateMaterial(_spawnedBuilding.IsOverlapping ? _buildingMatNegative : _buildingMatPositive);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _spawnedBuilding.transform.Rotate(0, _rotateSnapAngle, 0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if (IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo))
        {
            var gridPosition = WordGrid.GridPositionFronWorldPosition3D(hitInfo.point, 1f);
            _spawnedBuilding.transform.position = gridPosition;
           
            if (Mouse.current.leftButton.wasPressedThisFrame && !_spawnedBuilding.IsOverlapping)
            {
                if (objectName == "Plant"){
                    _spawnedBuilding.PlaceBuilding();
                    var dataCopy = _spawnedBuilding.AssignedData;
                   _spawnedBuilding = null;
                    ChoosePart(dataCopy);
                    buildingAtivar = false;

                }
                
            }
        }
    }


}