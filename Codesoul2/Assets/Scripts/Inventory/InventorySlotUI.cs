using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public ItemData itemData;

    Image itemImage;
    TMP_Text quantityText;

    private void Awake()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        quantityText = transform.Find("ItemQuantity").GetComponent<TMP_Text>();
    }

    public void RefreshSlot(InventoryItem item)
    {
        if (itemData != null)
        {
            itemImage.sprite = itemData.itemSprite;
            itemImage.enabled = true;
            if (itemData.canStack && item.quantity > 1)
            {
                quantityText.text = item.quantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.enabled = false;
            }
        }
    }

    public void SetItem(ItemData item)
    {
        itemData = item;
    }
}
