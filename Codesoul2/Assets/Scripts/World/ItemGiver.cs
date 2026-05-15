using TMPro;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    Inventory inventory;
    public ItemData itemToGive;

    TMP_Text interactText;

    bool inRange;
    int cost;

    AudioSource audioSource;
    public AudioClip giveItemSound;

    private void Start()
    {
        // Get audio source
        audioSource = GetComponent<AudioSource>();
        // Get player inventory
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        // Get interact text
        interactText = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(giveItemSound);
            GiveItemOnInteract();
            Debug.Log("Gave item to player: " + itemToGive.itemName);
        }
    }

    private void GiveItemOnInteract()
    {
        inventory.AddItem(itemToGive, 30);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.text = "Press E to receive " + itemToGive.itemName + " for " + cost;
            inRange = true;
            Debug.Log("Player in range to receive item.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.text = string.Empty;
            inRange = false;
            Debug.Log("Player out of range to receive item.");
        }
    }
}
