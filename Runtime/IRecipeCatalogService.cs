using System;
using System.Collections.Generic;
using CupkekGames.InventorySystem;

namespace CupkekGames.InventorySystem.Crafting
{
  /// <summary>
  /// Recipe keys and <see cref="RecipeSO"/> from game-provided <c>IAssetCatalog&lt;RecipeSO&gt;</c> instances,
  /// plus <see cref="RecipeSettingsSO"/> for cooking time / essence rules.
  /// </summary>
  public interface IRecipeCatalogService
  {
    RecipeSettingsSO Settings { get; }

    RecipeSO GetRecipeOrNull(string key);

    IEnumerable<string> GetRecipeKeys();

    RecipeSearchResult Search(
      IngredientEssenceRatio ingredientEssenceRatio,
      IInventoryItemDatabase itemDatabase,
      Func<InventoryItemDefinition, bool> resultFilter = null);

    List<string> Filter(
      IInventoryItemDatabase itemDatabase,
      Func<InventoryItemDefinition, bool> resultFilter,
      ICollection<string> unlocked,
      bool showLocked);
  }
}
