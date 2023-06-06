using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public InventoryItemData craftedItem;
    public ItemRequirement[] requirements;
}

[System.Serializable]
public class ItemRequirement
{
    public InventoryItemData item;
    public int id;
    public int quantity;
}
