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


    private void BuildModeLogic()
    {

        if(_targetBuilding != null && _targetBuilding.FlaggedForDelete)
        {
            _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
        }
        if(_spawnedBuilding == null) return;

        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            _spawnedBuilding.transform.Rotate(0,_rotateSnapAngle,0); // depois mudar a rotação se for menos ou mais
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if(!IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo)) 
        {
         _spawnedBuilding.UpdateMaterial(_buildingMatNegative);  
        }
        else
        {
        _spawnedBuilding.UpdateMaterial(_buildingMatPositive);  
        var gridPosition = WordGrid.GridPositionFronWorldPosition3D(hitInfo.point, 1f);
        _spawnedBuilding.transform.position = gridPosition;
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
        
            Building placedBuilding = Instantiate(_spawnedBuilding, gridPosition, _lastRotation);
            placedBuilding.PlaceBuilding();

        }
        }

    }
}
