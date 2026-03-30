using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 8;
    public List<InventoryItem> items = new();

    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item.canStack)
        {
            foreach (var slot in items)
            {
                if (slot.data == item)
                {
                    slot.quantity += amount;
                    return true;
                }
            }
        }

        if (items.Count >= maxSlots)
        {
            return false;
        }

        items.Add(new InventoryItem { data = item, quantity = amount });

        return true;
    }
}
