using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class HotbarDisplay : StaticInventoryDisplay
{
    



    private int _maxIndexSize = 9;
    private int _currentIndex = 0;
    public Database database;

    private PlayerControls _playerControls; 
    //

    public Transform itemPrefab;

    private GameObject spawnedObject;
    public GameObject spawnObject2;

    public bool hasSpawned = true;

    private int itemId = -2;

    public bool teste22 = false;

    private ItemPickUp _itemPickUp;
    private void Awake()
    {

        _playerControls = new PlayerControls();
        _itemPickUp = FindObjectOfType<ItemPickUp>();
    }

    protected override void Start()
    {
        
        base.Start();

        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;

        slots[_currentIndex].ToggleHighlight();
        
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerControls.Enable();

        _playerControls.Player.Hotbar1.performed += Hotbar1;
        _playerControls.Player.Hotbar2.performed += Hotbar2;
        _playerControls.Player.Hotbar3.performed += Hotbar3;
        _playerControls.Player.Hotbar4.performed += Hotbar4;
        _playerControls.Player.Hotbar5.performed += Hotbar5;
        _playerControls.Player.Hotbar6.performed += Hotbar6;
        _playerControls.Player.Hotbar7.performed += Hotbar7;
        _playerControls.Player.Hotbar8.performed += Hotbar8;
        _playerControls.Player.Hotbar9.performed += Hotbar9;
        _playerControls.Player.Hotbar10.performed += Hotbar10;
        _playerControls.Player.UseItem.performed += UseItem;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _playerControls.Disable();

        _playerControls.Player.Hotbar1.performed -= Hotbar1;
        _playerControls.Player.Hotbar2.performed -= Hotbar2;
        _playerControls.Player.Hotbar3.performed -= Hotbar3;
        _playerControls.Player.Hotbar4.performed -= Hotbar4;
        _playerControls.Player.Hotbar5.performed -= Hotbar5;
        _playerControls.Player.Hotbar6.performed -= Hotbar6;
        _playerControls.Player.Hotbar7.performed -= Hotbar7;
        _playerControls.Player.Hotbar8.performed -= Hotbar8;
        _playerControls.Player.Hotbar9.performed -= Hotbar9;
        _playerControls.Player.Hotbar10.performed -= Hotbar10;
        _playerControls.Player.UseItem.performed -= UseItem;
    }

    #region Hotbar Select Methods

    private void Hotbar1(InputAction.CallbackContext obj)
    {
        SetIndex(0);
        pegaritem();
    }

    private void Hotbar2(InputAction.CallbackContext obj)
    {
        SetIndex(1);
        pegaritem();
    }

    private void Hotbar3(InputAction.CallbackContext obj)
    {
        SetIndex(2);
        pegaritem();
    }

    private void Hotbar4(InputAction.CallbackContext obj)
    {
        SetIndex(3);
        pegaritem();
    }

    private void Hotbar5(InputAction.CallbackContext obj)
    {
        SetIndex(4);
        pegaritem();
    }

    private void Hotbar6(InputAction.CallbackContext obj)
    {
        SetIndex(5);
        pegaritem();
    }

    private void Hotbar7(InputAction.CallbackContext obj)
    {
        SetIndex(6);
        pegaritem();
    }

    private void Hotbar8(InputAction.CallbackContext obj)
    {
        SetIndex(7);
        pegaritem();
    }

    private void Hotbar9(InputAction.CallbackContext obj)
    {
        SetIndex(8);
        pegaritem();
    }

    private void Hotbar10(InputAction.CallbackContext obj)
    {
        SetIndex(9);
        pegaritem();
    }

    #endregion

    public void ClearSelectedItem()
    {
        InventorySlot selectedSlot = slots[_currentIndex].AssignedInventorySlot;
        
        if (selectedSlot.ItemData != null)
        {
            selectedSlot.RemoveFromStack(1); // Remover 1 unidade do stack do item
            slots[_currentIndex].UpdateUISlot();

            // Remover o item atualmente instanciado na mão, se houver
            Destroy(spawnObject2);
            hasSpawned = true;
        }
    }
    /*
    public void ClearSelectedItem()
    {

        InventorySlot selectedSlot = slots[_currentIndex].AssignedInventorySlot;

        if (selectedSlot.ItemData != null)
        {
            if (selectedSlot.ItemData.MaxStackSize > 1)
            {
                selectedSlot.ItemData.MaxStackSize--; <- Aqui ele vai remover do scripttableobject 
                slots[_currentIndex].UpdateUISlot();
            }
            else
            {
                selectedSlot.ClearSlot();
                slots[_currentIndex].UpdateUISlot();
            }
            Destroy(spawnObject2);
            hasSpawned = true;
        }

    }
    */
    private void Update()
    {
    //   if (slots[_currentIndex].AssignedInventorySlot.ItemData == null)
    //     {
    //         Debug.LogWarning("A");
    //     }
    
    if (_playerControls.Player.MouseWheel.ReadValue<float>() > 0.1f)
    {
        ChangeIndex(1);
        pegaritem();
    }
    if (_playerControls.Player.MouseWheel.ReadValue<float>() < -0.1f)
    {
        ChangeIndex(-1);
        pegaritem();
    }
        
    }

    public void pegaritem()
    {
        

        Destroy(spawnObject2);
        hasSpawned = true;

        
       if (slots[_currentIndex].AssignedInventorySlot.ItemData != null)
       {
        
        itemId = slots[_currentIndex].AssignedInventorySlot.ItemData.ID;
        var db = Resources.Load<Database>("Database");
        //string itemName = db.GetItemNameById(itemId);
        if(hasSpawned)
        {
        InstantiateItemInHand(itemId);
            hasSpawned = false;
        }
        
        
        }
        
    }

    public void InstantiateItemInHand(int itemId)
    {
        
        var db = Resources.Load<Database>("Database");
        string itemName = db.GetItemNameById(itemId);

        //itemPrefab = Instantiate(itemName, transform.position, Quaternion.identity);

       
        InventoryItemData item = db.GetItem(itemId);

        if (item != null)
        {

            GameObject spawnedObject = Instantiate(item.ItemPrefab, itemPrefab.transform.position, Quaternion.identity);
            spawnedObject.transform.SetParent(itemPrefab);
           Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            spawnObject2 = spawnedObject;



            //Debug.Log("Item instanciado: " + item.DisplayName);
        }
        else
        {
            Debug.LogWarning("Item não encontrado no banco de dados. ID do item: " + itemPrefab);
        }
        
    }

    
    public void UseItem(InputAction.CallbackContext obj)
    {
        Debug.Log("1");
        ClearSelectedItem();
        if (slots[_currentIndex].AssignedInventorySlot.ItemData != null) slots[_currentIndex].AssignedInventorySlot.ItemData.UseItem();
    }

    private void ChangeIndex(int direction)
    {
        slots[_currentIndex].ToggleHighlight();
        _currentIndex += direction;

        if (_currentIndex > _maxIndexSize) _currentIndex = 0;
        if (_currentIndex < 0) _currentIndex = _maxIndexSize;
        
        slots[_currentIndex].ToggleHighlight();
    }

    private void SetIndex(int newIndex)
    {
        slots[_currentIndex].ToggleHighlight();
        if (newIndex < 0) _currentIndex = 0;
        if (newIndex > _maxIndexSize) _currentIndex = _maxIndexSize;
        
        _currentIndex = newIndex;
        slots[_currentIndex].ToggleHighlight();
    }
}

