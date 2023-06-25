using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;

    public bool fisicaRigidbody = false;
    private SphereCollider myCollider;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;
    
    private void Awake()
    {
       
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);

        
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
    }

    private void Start()
    {
        //fisicaRigidbody = true;
        id = GetComponent<UniqueID>().ID;
        //SaveGameManager.data.activeItems.Add(id, itemSaveData);
    }

    public void Fisica()
    {
        if(fisicaRigidbody)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
        
            rb.isKinematic = true;
        
        }
    }
    private void Update()
    {
        Fisica();
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(id)) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //if (SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Remove(id);
        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void OnTriggerEnter(Collider other)
    {

        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        if (inventory != null && inventory.AddToInventory(ItemData, 1)) 
        {
            //SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;
    

    public ItemPickUpSaveData(InventoryItemData _data, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        Position = _position;

    }
}
