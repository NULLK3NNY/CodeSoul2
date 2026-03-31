using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 8;
    public List<InventoryItem> items = new();

    // Inventory UI
    public GameObject inventoryUI;
    bool isInventoryOpen = false;

    // Example item pickup and inventory toggle
    public ItemData testItem;

    private void Update()
    {
        ShowInventoryUI();

        if(Input.GetKeyDown(KeyCode.E))
        {
            // Example item pickup
            var itemToAdd = Resources.Load<ItemData>("ExampleItem");
            if (AddItem(itemToAdd))
            {
                Debug.Log("Item added to inventory!");
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

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

    public void ShowInventoryUI()
    {
        // Open and close inventory
        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryOpen)
        {
            isInventoryOpen = true;
            inventoryUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen)
        {
            isInventoryOpen = false;
            inventoryUI.SetActive(false);
        }

        // Show items
        for (int i = 0; i < items.Count; i++)
        {
            var slot = inventoryUI.transform.GetChild(i);
            var item = items[i];
            slot.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = item.data.itemSprite;
            slot.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = item.quantity > 1 ? item.quantity.ToString() : "";
        }
    }
}
