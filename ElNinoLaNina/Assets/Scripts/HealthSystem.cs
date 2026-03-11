using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text healthText;
    public RectTransform healthBarFill;
    public RectTransform topBorder;
    public RectTransform botBorder;
    public RectTransform rightBorder;
    public RectTransform healthTextBox;

    Vector2 origTextPos;
    Vector2 origBorderPos;

    public void TakeDamage(int val, string cause) {
        if (HealthData.currentHP - val <= 0)
        {
            HealthData.currentHP = 0;
            TutorialLevelManager.PlayerDeath(cause);
        }
        else
        {
            HealthData.currentHP -= val;
        } 
    }

    public void ReduceMax(int val, string cause)
    {
        if (HealthData.maxHP - val <= 50)
        {
            HealthData.maxHP = 50;
            TakeDamage(HealthData.currentHP - 50, cause);
        }   
        else
        {
            HealthData.maxHP -= val;
            TakeDamage(val, cause);
        }
        DecMaxHPUpdate();
    }

    void DecMaxHPUpdate()
    {
        //resize top and bottom borders
        int newWidth = HealthData.maxHP * 10;
        topBorder.sizeDelta = new Vector2(newWidth, topBorder.sizeDelta.y);
        botBorder.sizeDelta = new Vector2(newWidth, botBorder.sizeDelta.y);

        Vector2 newPos = origBorderPos;
        newPos.x -= (100 - HealthData.maxHP) * 10;
        rightBorder.anchoredPosition = newPos;

        Vector2 textPos = origTextPos;
        textPos.x -= (100 - HealthData.maxHP) * 5;
        healthTextBox.anchoredPosition = textPos;
    }

    void UpdateText()
    {
        if (healthBarFill != null)
        {
            int newWidth = HealthData.currentHP * 10;
            healthBarFill.sizeDelta = new Vector2(newWidth, healthBarFill.sizeDelta.y);
        }
        healthText.text = HealthData.currentHP + "/" + HealthData.maxHP;
    }

    void Start()
    {
        origBorderPos = rightBorder.anchoredPosition;
        origTextPos = healthTextBox.anchoredPosition;
    }
    void Update()
    {
        UpdateText();
    }
}
