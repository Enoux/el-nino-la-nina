using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests {

    private InventoryManager manager;
    private GameObject managerGO;

    private ItemData CreateItem(string name = "TestItem") {
        var item = ScriptableObject.CreateInstance<ItemData>();
        item.itemName = name;
        return item;
    }

    [SetUp]
    public void Setup() {
        managerGO = new GameObject("InventoryManager_Test");
        manager = managerGO.AddComponent<InventoryManager>();
    }

    [TearDown]
    public void TearDown() {
        Object.DestroyImmediate(managerGO);
    }

    [Test]
    public void AddItem_AddsItemToInventory() {
        var item = CreateItem();

        manager.AddItem(item);

        CollectionAssert.Contains(manager.GetItems(), item);
    }

    [Test]
    public void AddItem_DoesNotDuplicateItem() {
        var item = CreateItem();

        manager.AddItem(item);
        manager.AddItem(item);

        var items = manager.GetItems();
        Assert.AreEqual(1, items.Count);
    }

    [Test]
    public void RemoveItem_RemovesItemFromInventory() {
        var item = CreateItem();

        manager.AddItem(item);
        manager.RemoveItem(item);

        CollectionAssert.DoesNotContain(manager.GetItems(), item);
    }

    [Test]
    public void SelectItem_SetsSelectedItem() {
        var item = CreateItem();

        manager.AddItem(item);
        manager.SelectItem(item);

        Assert.AreEqual(item, manager.SelectedItem);
    }

    [Test]
    public void ClearSelection_ResetsSelectedItem() {
        var item = CreateItem();

        manager.AddItem(item);
        manager.SelectItem(item);
        manager.ClearSelection();

        Assert.IsNull(manager.SelectedItem);
    }

    [Test]
    public void RemovingSelectedItem_ClearsSelection() {
        var item = CreateItem();

        manager.AddItem(item);
        manager.SelectItem(item);
        manager.RemoveItem(item);

        Assert.IsNull(manager.SelectedItem);
    }
}