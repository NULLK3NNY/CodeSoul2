using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MysteryBox : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Weapon[] weaponsInBox;
    [SerializeField] Weapon[] upgradedWeapons;
    [SerializeField] WeaponManager weaponManager;
    Animator animator;
    public GameObject weaponSprite;
    bool inRange;
    bool canGrabWeapon;
    public bool opened = false;
    public bool dupeDetected = false;
    public Weapon boxWeapon;
    public float timer;
    public int pickUpTime = 5;
    public float boxTimer;
    public int timeUntilCanOpenBox = 2;
    public int price;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {

        if (!opened)
        {
            boxTimer += Time.deltaTime;
        }
        else
        {
            boxTimer = 0;
        }

        if (timeUntilCanOpenBox < boxTimer)
        {
            OpenBox();
        }


        TimeToGrab();
    }

    private void OpenBox()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange && !opened && gameManager.playerScore >= price)
        {
            gameManager.playerScore -= price;
            opened = true;
            animator.SetBool("open", true);
            StartCoroutine(GiveRandomWeapon(80, .05f));
        }
    }

    private void TimeToGrab()
    {
        if (canGrabWeapon)
        {
            timer += Time.deltaTime;

            if (boxWeapon.weaponName == weaponManager.weapons[0].weaponName)
            {
                //Debug.Log("Dupe detected");
                dupeDetected = true;
            }
            else if (weaponManager.weapons[1] != null && boxWeapon.weaponName == weaponManager.weapons[1].weaponName)
            {
                //Debug.Log("Dupe detected");
                dupeDetected = true;
            }
            else
            {
                dupeDetected = false;
            }


            if (Input.GetKeyDown(KeyCode.F) && inRange)
            {
                if (weaponManager.weapons[1] == null)
                {
                    weaponManager.weapons[1] = boxWeapon;
                }
                else
                {
                    weaponManager.weapons[weaponManager.currentWeaponSlot] = boxWeapon;
                    weaponManager.EquipWeapon(weaponManager.currentWeaponSlot);
                }

                boxWeapon = null;
                canGrabWeapon = false;
                weaponSprite.SetActive(false);
                timer = 0;
                opened = false;
                animator.SetBool("open", false);
            }

            if (dupeDetected)
            {
                Weapon randomWeapon = weaponsInBox[Random.Range(0, weaponsInBox.Length)];
                weaponSprite.GetComponent<SpriteRenderer>().sprite = randomWeapon.weaponSprite;
                boxWeapon = randomWeapon;
            }


            if (timer > pickUpTime)
            {
                timer = 0;
                weaponSprite.SetActive(false);
                canGrabWeapon = false;
                opened = false;
                animator.SetBool("open", false);
            }
        }
    }

    private IEnumerator GiveRandomWeapon(int amountOfSwaps, float duration)
    {
        weaponSprite.SetActive(true);

        for (int i = 0; i < amountOfSwaps; i++)
        {
            Weapon randomWeapon = weaponsInBox[Random.Range(0, weaponsInBox.Length)];
            weaponSprite.GetComponent<SpriteRenderer>().sprite = randomWeapon.weaponSprite;
            boxWeapon = randomWeapon;
            yield return new WaitForSeconds(duration);
        }

        canGrabWeapon = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!opened)
            {
                interactText.text = "Press and hold F to buy random weapon for " + price + ".";
                interactPrompt.SetActive(true);
            }
            else
            {
                interactText.text = string.Empty;
                interactPrompt.SetActive(false);

                if (canGrabWeapon)
                {
                    interactText.text = "Press and hold F to grab " + boxWeapon.weaponName + ".";
                    interactPrompt.SetActive(true);
                }
                else
                {
                    interactText.text = string.Empty;
                    interactPrompt.SetActive(false);
                }
            }


        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!opened)
            {
                interactText.text = "Press and hold F to buy random weapon for " + price + ".";
                interactPrompt.SetActive(true);
            }
            else
            {
                interactText.text = string.Empty;
                interactPrompt.SetActive(false);

                if (canGrabWeapon)
                {
                    interactText.text = "Press and hold F to grab " + boxWeapon.weaponName + ".";
                    interactPrompt.SetActive(true);
                }
                else
                {
                    interactText.text = string.Empty;
                    interactPrompt.SetActive(false);
                }
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;

            interactText.text = string.Empty;
            interactPrompt.SetActive(false);
        }
    }
}