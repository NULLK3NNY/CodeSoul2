using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 8;
    public List<InventoryItem> items = new();
    public List<InventoryItem> itemsToRemove = new();

    // Inventory UI
    public GameObject inventoryUI;
    bool isInventoryOpen = false;

    // Weapon UI
    [SerializeField] TMP_Text primaryWeaponName;
    [SerializeField] TMP_Text primaryWeaponMag;
    [SerializeField] Image primaryWeaponIcon;

    [SerializeField] TMP_Text secondaryWeaponName;
    [SerializeField] TMP_Text secondaryWeaponMag;
    [SerializeField] Image secondaryWeaponIcon;

    // Example item pickup and inventory toggle
    public ItemData testItem;
    public ItemData shells;

    // Inventory slots
    public GameObject[] slotsUI;

    // Player 
    Player player;

    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();

        AddItem(testItem, 50);
        AddItem(shells, 50);
    }

    private void Update()
    {
        ShowInventoryUI();

        if(Input.GetKeyDown(KeyCode.P))
        {
            PrintInventoryToConsole();
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

    public InventoryItem GetItemInInventory(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].data.itemName == itemName)
            {
                return items[i];
            }
        }

        return null;
    }

    public void PrintInventoryToConsole()
    {
        foreach (var item in items)
        {
            Debug.Log("Item: " + item.data.itemName + ", Quantity: " + item.quantity);
        }
    }

    public void ShowInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryUI.SetActive(isInventoryOpen);

            if (isInventoryOpen)
            {
                RefreshInventoryUI();
            }
        }
    }

    public void RefreshInventoryUI()
    {
        items.RemoveAll(item => item.quantity <= 0);
        
        for (int i = 0; i < maxSlots; i++)
        {
            slotsUI[i].GetComponent<InventorySlotUI>().SetItem(null);
            slotsUI[i].GetComponent<InventorySlotUI>().UnloadSlot();
        }

        for (int i = 0; i < items.Count && i < maxSlots; i++)
        {
            if (items[i] != null && items[i].quantity > 0)
            {
                slotsUI[i].GetComponent<InventorySlotUI>().SetItem(items[i].data);
                slotsUI[i].GetComponent<InventorySlotUI>().RefreshSlot(items[i]);
            }
        }

        // Set Weapon UI
        // Primary
        if (player.GetComponentInChildren<WeaponManager>().weapons[0] != null)
        {
            primaryWeaponIcon.GetComponent<Image>().enabled = true;


            primaryWeaponName.text = player.GetComponentInChildren<WeaponManager>().weapons[0].weaponName;
            primaryWeaponMag.text = player.GetComponentInChildren<WeaponManager>().weapons[0].ammoInMag.ToString();
            primaryWeaponIcon.sprite = player.GetComponentInChildren<WeaponManager>().weapons[0].weaponSprite;
        }
        else
        {
            primaryWeaponIcon.GetComponent<Image>().enabled = false;

            primaryWeaponName.text = string.Empty;
            primaryWeaponMag.text = string.Empty;
            primaryWeaponIcon.sprite = null;
        }
        // Secondary
        if (player.GetComponentInChildren<WeaponManager>().weapons[1] != null)
        {
            secondaryWeaponIcon.GetComponent<Image>().enabled = true;

            secondaryWeaponName.text = player.GetComponentInChildren<WeaponManager>().weapons[1].weaponName;
            secondaryWeaponMag.text = player.GetComponentInChildren<WeaponManager>().weapons[1].ammoInMag.ToString();
            secondaryWeaponIcon.sprite = player.GetComponentInChildren<WeaponManager>().weapons[1].weaponSprite;
        }
        else
        {
            secondaryWeaponIcon.GetComponent<Image>().enabled = false;

            secondaryWeaponName.text = string.Empty;
            secondaryWeaponMag.text = string.Empty;
            secondaryWeaponIcon.sprite = null;
        }
    }

}
