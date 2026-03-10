using UnityEngine;

[CreateAssetMenu(menuName = "CustomObjects/Recipe")]
public class CraftingRecipe : ScriptableObject {
    public ItemData itemA;
    public ItemData itemB;
    public ItemData result;

    public bool consumeA = true;
    public bool consumeB = true;
}
