using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

using NUnit.Framework;
using UnityEngine;

public class CraftingTests
{
    InventoryManager inventory;

    ItemData itemA;
    ItemData itemB;
    ItemData result;

    CraftingRecipe recipe;

    [SetUp]
    public void Setup()
    {
        var go = new GameObject();
        inventory = go.AddComponent<InventoryManager>();

        itemA = ScriptableObject.CreateInstance<ItemData>();
        itemB = ScriptableObject.CreateInstance<ItemData>();
        result = ScriptableObject.CreateInstance<ItemData>();

        recipe = ScriptableObject.CreateInstance<CraftingRecipe>();
        recipe.itemA = itemA;
        recipe.itemB = itemB;
        recipe.result = result;

        recipe.consumeA = true;
        recipe.consumeB = true;

        // inject recipes manually (bypass Resources)
        typeof(InventoryManager)
            .GetField("recipes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(inventory, new CraftingRecipe[] { recipe });

        inventory.AddItem(itemA);
        inventory.AddItem(itemB);
    }

    [Test]
    public void TryCraft_ValidRecipe_ReturnsTrue()
    {
        bool crafted = inventory.TryCraft(itemA, itemB);

        Assert.IsTrue(crafted);
    }

    [Test]
    public void TryCraft_AddsResultItem()
    {
        inventory.TryCraft(itemA, itemB);

        Assert.Contains(result, inventory.GetItems());
    }

    [Test]
    public void TryCraft_RemovesIngredients()
    {
        inventory.TryCraft(itemA, itemB);

        Assert.IsFalse(inventory.GetItems().Contains(itemA));
        Assert.IsFalse(inventory.GetItems().Contains(itemB));
    }

    [Test]
    public void TryCraft_OrderDoesNotMatter()
    {
        bool crafted = inventory.TryCraft(itemB, itemA);

        Assert.IsTrue(crafted);
    }

    [Test]
    public void TryCraft_NoRecipe_ReturnsFalse()
    {
        var wrongItem = ScriptableObject.CreateInstance<ItemData>();

        bool crafted = inventory.TryCraft(itemA, wrongItem);

        Assert.IsFalse(crafted);
    }
}
