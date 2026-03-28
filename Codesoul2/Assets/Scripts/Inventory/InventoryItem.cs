using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData")]
public class InventoryItem : ScriptableObject
{
    [Header("Inventory Item Properties")]
    public Sprite itemSprite;
    public string itemName;
}
