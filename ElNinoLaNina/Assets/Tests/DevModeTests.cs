using NUnit.Framework;
using UnityEngine;

public class DevModeTests
{
    private GameObject playerObj;
    private HealthSystem healthSystem;

    [SetUp]
    public void Setup() {
        // Setup PlayerSaveFile
        PlayerSaveFile.currentSaveFile = new PlayerSaveFile();
        PlayerSaveFile.currentSaveFile.godModeEnabled = false;
        PlayerSaveFile.currentSaveFile.devModeEnabled = false;

        // Setup Health Data
        HealthData.currentHP = 100;
        HealthData.maxHP = 100;

        // Create GameObject with HealthSystem
        playerObj = new GameObject("Player");
        healthSystem = playerObj.AddComponent<HealthSystem>();
    }

    [TearDown]
    public void Teardown() {
        Object.DestroyImmediate(playerObj);
    }

    [Test]
    public void TakeDamage_NormalMode_ReducesHealth() {
        healthSystem.TakeDamage(20, "test");

        Assert.AreEqual(80, HealthData.currentHP);
    }

    [Test]
    public void TakeDamage_GodMode_DoesNotReduceHealth() {
        PlayerSaveFile.currentSaveFile.godModeEnabled = true;

        healthSystem.TakeDamage(50, "test");

        Assert.AreEqual(100, HealthData.currentHP);
    }

    [Test]
    public void ReduceMax_GodMode_DoesNothing() {
        PlayerSaveFile.currentSaveFile.godModeEnabled = true;

        healthSystem.ReduceMax(30, "test");

        Assert.AreEqual(100, HealthData.maxHP);
        Assert.AreEqual(100, HealthData.currentHP);
    }

    [Test]
    public void DevModeFlag_CanBeEnabled() {
        PlayerSaveFile.currentSaveFile.devModeEnabled = true;

        Assert.IsTrue(PlayerSaveFile.currentSaveFile.devModeEnabled);
    }

    [Test]
    public void UniversalDevMode_OverridesIndividual() {
        PlayerSaveFile.universalDevMode = true;

        Assert.IsTrue(PlayerSaveFile.universalDevMode);
    }
}
