using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public GameObject imageButton; 
    public GameObject selectOutline; 

    public ItemData Item {get; private set;} 

    public void Initialize(ItemData item) {
        SetOutline(false);
        Item = item;
        var button = imageButton.GetComponent<Button>();
        var image = imageButton.GetComponent<Image>();

        image.sprite = item.icon;
        SpriteState spriteState = button.spriteState;
        spriteState.highlightedSprite = item.hoverIcon;
        spriteState.selectedSprite = item.hoverIcon;
        spriteState.pressedSprite = item.hoverIcon;
        button.spriteState = spriteState;

        button.onClick.AddListener(() => {
            InventoryManager.Instance.SelectItem(Item);
        });
    }

    public void SetOutline(bool visible) {
        selectOutline.SetActive(visible);
    }


}
