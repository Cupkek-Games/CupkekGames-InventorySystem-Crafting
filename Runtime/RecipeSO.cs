using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "CupkekGames/Inventory/Crafting/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] public Recipe Recipe;
    }
}
