using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 8;
    public List<InventoryItem> items = new();

    // Inventory UI
    public GameObject inventoryUI;
    bool isInventoryOpen = false;

    // Example item pickup and inventory toggle
    public ItemData testItem;

    // Inventory slots
    public GameObject[] slotsUI;

    private void Start()
    {
        
    }

    private void Update()
    {
        ShowInventoryUI();

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItem);
            Debug.Log("Added Item: " + testItem.itemName);
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

            // Show Items
            for (int i = 0; i < items.Count; i++)
            {
                if (i < maxSlots)
                {
                    slotsUI[i].GetComponent<InventorySlotUI>().SetItem(items[i].data);
                    slotsUI[i].GetComponent<InventorySlotUI>().RefreshSlot(items[i]);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isInventoryOpen)
        {
            isInventoryOpen = false;
            inventoryUI.SetActive(false);
        }
    }
}
