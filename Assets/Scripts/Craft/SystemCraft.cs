using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SystemCraft : MonoBehaviour
{
    public HotbarDisplay _hotbarDisplay;
    public Text craftResultText;
    public Button[] recipeButtons; 

    public CraftingRecipe[] craftingRecipes;
    private ItemRequirement _itemRequirement;
    private InventoryItemData _inventoryItemData;
    private CraftingRecipe _craftRecipe;
    public Image imageIcon;

    private StaticInventoryDisplay _staticInventoryDisplay;
    public bool canCraft = false;
    public string recipeName;
    public CraftingRecipe selectedRecipe;

    private int selectedRecipeIndex = 0; // Índice da receita selecionada

    //public GameObject Botao;

    private void Start()
    {
        ClearItemPreview();
        _craftRecipe = FindObjectOfType<CraftingRecipe>();
        //craftResultText.text = "";

        // Adicionar os listeners de clique aos botões das receitas
        for (int i = 0; i < recipeButtons.Length; i++)
        {
            int recipeIndex = i; // Armazenar o índice para uso no listener

            // Adicionar listener ao botão de cada receita
            recipeButtons[i].onClick.AddListener(() => SelectRecipe(recipeIndex));
        }
    }

    public void CheckHotbar()
    {
        _hotbarDisplay.CheckHotbar();
    }

    public void SelectRecipe(int index)
    {
        selectedRecipeIndex = index;
        ShowCraftButton();
        DisplayRecipeName(); 
    }

    private void ShowCraftButton()
    {
        // receita selecionada
    }

    private void DisplayRecipeName()
    {
        ClearItemPreview();
        recipeName = craftingRecipes[selectedRecipeIndex].craftedItem.DisplayName;
        Debug.Log("Crafting: " + recipeName);
        Sprite itemId = craftingRecipes[selectedRecipeIndex].craftedItem.Icon;
        imageIcon.color = Color.white;
        imageIcon.sprite = itemId;
        selectedRecipe = craftingRecipes[selectedRecipeIndex];
        //CraftItem();
    }
    public void ClearItemPreview()
    {
        imageIcon.sprite = null;
        imageIcon.color = Color.clear;

    }

    public void CraftItem()
    {
        
        selectedRecipe = craftingRecipes[selectedRecipeIndex]; // Receita selecionada

        bool canCraft = true;

        foreach (ItemRequirement requirement in selectedRecipe.requirements)
        {
            if (!_hotbarDisplay.CheckItemInHotbar(requirement.id))
            {
                canCraft = false;
                Debug.Log("O item necessário não está presente no inventário. para o " + recipeName);
                break;
            }
        }

        if (canCraft)
        {
            foreach (ItemRequirement requirement in selectedRecipe.requirements)
            {
                _hotbarDisplay.RemoveItem(requirement.id, requirement.quantity);
            }

            AddToInventory(selectedRecipe.craftedItem, 1);

            //craftResultText.text = "Crafted: " + selectedRecipe.craftedItem.DisplayName;
        }
    }
    public void RemoveItem(){

        foreach (ItemRequirement requirement in selectedRecipe.requirements) //remover mennos o item que foi feito
            {
                _hotbarDisplay.RemoveItem(requirement.id, requirement.quantity);
            }
    }
    private void AddToInventory(InventoryItemData id, int quantity)
    {
        _hotbarDisplay.AddItemToHotbar(id, quantity);
    }

    public void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            CraftItem();
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            CheckHotbar();
        }
    }
}
