using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "CupkekGames/Crafting/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] public Recipe Recipe;
    }
}
