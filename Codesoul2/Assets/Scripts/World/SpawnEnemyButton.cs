using TMPro;
using UnityEngine;

public class SpawnEnemyButton : MonoBehaviour
{
    TMP_Text interactText;
    public GameObject enemyToSpawn;
    public GameObject spawnPoint;
    bool inRange;
    AudioSource audioSource;
    public AudioClip spawnSound;

    private void Start()
    {
        // Get audio source
        audioSource = GetComponent<AudioSource>();
        // Get interact text
        interactText = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(spawnSound);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.text = "Press E to spawn basic enemy";
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            interactText.text = string.Empty;
            inRange = false;
        }
    }
}
