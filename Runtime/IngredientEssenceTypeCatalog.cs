using CupkekGames.Data;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    [CreateAssetMenu(
        fileName = "IngredientEssenceTypeCatalog",
        menuName = "CupkekGames/Inventory/Crafting/Catalog/Ingredient Essence Types")]
    public class IngredientEssenceTypeCatalog : AssetCatalog<IngredientEssenceTypeSO> { }
}
