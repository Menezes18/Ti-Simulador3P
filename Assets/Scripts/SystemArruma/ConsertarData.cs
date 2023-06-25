using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Consertar/ConsertarData")]
public class ConsertarData : ScriptableObject
{
    public GameObject FinalPrefab;
    public ItemConsertarData[] requirements;
}

[System.Serializable]
public class ItemConsertarData 
{
    public InventoryItemData item;
    public int id;
    public int quantity;
}

