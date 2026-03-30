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

    public virtual void Use() { }
    public virtual bool CanCombine(ItemData other) => false;
    public virtual ItemData Combine(ItemData other) => null;
}
