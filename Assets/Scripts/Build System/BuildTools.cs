using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildTools : MonoBehaviour
{
  
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
    
    private void Start()
    {
        _camera = Camera.main;

    }

    private void Update()
    {
        if(Keyboard.current.qKey.wasPressedThisFrame) _deleteModeEnabled = !_deleteModeEnabled;
        if(_deleteModeEnabled) DeleteModeLogic();
        else BuildModeLogic();
    }

    private bool IsRayHittingSomething(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
        return Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask);
    }
    private void DeleteModeLogic()
    {
        if (!IsRayHittingSomething(_deleteModeLayerMask, out RaycastHit hitInfo)) return;
        if (Mouse.current.leftButton.wasPressedThisFrame) Destroy(hitInfo.collider.gameObject);
    }

    private void BuildModeLogic()
    {
        if(_spawnedBuilding == null) return;

        if(IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo)) 
        {
         _spawnedBuilding.UpdateMaterial(_buildingMatNegative);  
        }
        else
        {
        _spawnedBuilding.UpdateMaterial(_buildingMatNegative);  
        _spawnedBuilding.transform.position = hitInfo.point;
        }

       if (Mouse.current.leftButton.wasPressedThisFrame) 
       {
       
        Building placedBuilding =  Instantiate(_spawnedBuilding, hitInfo.point, Quaternion.identity);
        placedBuilding.PlaceBuilding();

       }
    }
}
