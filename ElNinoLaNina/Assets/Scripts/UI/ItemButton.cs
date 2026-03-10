using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour,
IPointerEnterHandler,
IPointerExitHandler {

    public GameObject imageButton;
    public GameObject selectOutline;

    public ItemData Item { get; private set; }

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

            var inv = InventoryManager.Instance;

            if (inv.SelectedItem == null) {
                inv.SelectItem(Item);
            } else {
                if (!inv.TryCraft(inv.SelectedItem, Item)) {
                    inv.SelectItem(Item);
                }
            }

        });
    }

    public void SetOutline(bool visible) {
        selectOutline.SetActive(visible);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ScreenEdgeInput.Instance.OnInventoryHover(Item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        ScreenEdgeInput.Instance.OnInventoryExit();
    }

}