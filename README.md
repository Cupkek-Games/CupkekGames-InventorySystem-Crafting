# CupkekGames InventorySystem Crafting

Recipe + crafting system that extends `com.cupkekgames.inventory`. Defines recipe assets, a recipe database, and the crafting manager that consumes ingredients and produces results.

## What's inside

**Runtime** (`CupkekGames.InventorySystem.Crafting.asmdef`)

- `RecipeSO` / `RecipeDatabase` — data
- `CraftingManager` — runtime executor
- `RecipeIngredient`, `RecipeResult`, etc. — value types

**Editor** (`CupkekGames.InventorySystem.Crafting.Editor.asmdef`)

- Custom inspectors for recipe assets.

## Dependencies

Asmdef references resolve via the CupkekGames scoped registry: `inventory`, `data`, `services`, `keyvaluedatabases`, `addressableassets`. Bring your own copy via the registry.
