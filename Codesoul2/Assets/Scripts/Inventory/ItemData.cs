using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Inventory Item Properties")]
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public bool canStack;
    public int maxStackSize;
}
