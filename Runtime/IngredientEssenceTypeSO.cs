using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting
{
    [CreateAssetMenu(fileName = "IngredientEssenceType", menuName = "CupkekGames/Inventory/Crafting/Ingredient Essence Type")]
    public class IngredientEssenceTypeSO : ScriptableObject
    {
        [SerializeField] private string _displayName;
        [SerializeField] private Color _color = Color.white;

        public string DisplayName => _displayName;
        public Color Color => _color;
    }
}
