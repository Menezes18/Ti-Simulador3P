using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseViewer : MonoBehaviour
{
    public Database itemDatabase; // Referência para o objeto Database que contém os dados dos itens

    private void Start()
    {
        FindItemsWithDurability(10);
    }

    private void FindItemsWithDurability(int durability)
    {
        foreach (InventoryItemData itemData in itemDatabase._itemDatabase)
        {
            if (itemData.durabilidade == durability)
            {
                Debug.Log("Item com durabilidade " + durability + ": " + itemData.DisplayName);
            }
        }
    }
}
