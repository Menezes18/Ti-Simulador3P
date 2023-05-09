using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    private Renderer _renderer;
    private Material _defaultMaterial;

    private bool _flaggedForDelete;
    public bool FlaggedForDelete => _flaggedForDelete;
    
    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer) _defaultMaterial = _renderer.material;
        else Debug.Log("Renderer not Found");

    }

    public void UpdateMaterial(Material newMaterial)
    {
        if(_renderer.material != newMaterial) _renderer.material = newMaterial;
    }


    public void PlaceBuilding()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer) _defaultMaterial = _renderer.material;
        else Debug.Log("Renderer not Found");
        UpdateMaterial(_defaultMaterial);
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
    
}
