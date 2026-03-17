using UnityEngine;

public class Wallbuy : MonoBehaviour
{
    // Dependency references
    GameManager gm;
    InteractUI ui;
    WeaponManager wm;

    // Wallbuy stats
    [Header("Wallbuy Config")]
    public Weapon weapon;
    public double cost;
    public double ammoCost;

    // Interaction
    bool inRange;
    string text;

    private void Awake()
    {
        // Get script references
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<InteractUI>();
        wm = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>();
    }

    private void Start()
    {
        // Change chalkoutline
        gameObject.GetComponent<SpriteRenderer>().sprite = weapon.weaponChalkSprite;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F) && gm.playerScore >= cost && !gm.HasPlayerGotThisWeapon(weapon))
        {
            Purchase(weapon);
        }

        if (inRange && Input.GetKeyDown(KeyCode.F) && gm.playerScore >= ammoCost && gm.HasPlayerGotThisWeapon(weapon))
        {
            PurchaseAmmo(weapon);
        }

        UpdateDisplayText();
    }

    void UpdateDisplayText()
    {
        if (gm.HasPlayerGotThisWeapon(weapon))
        {
            text = "Press and hold F to purchase ammo for " + weapon.weaponName + " for " + ammoCost + " points!";
        }
        else
        {
            text = "Press and hold F to purchase " + weapon.weaponName + " for " + cost + " points!";
        }
    }

    public void Purchase(Weapon weapon)
    {
        gm.playerScore -= (int)cost;
        weapon.reservedAmmo = weapon.maxReservedAmmo;

        if (!gm.HasPlayerGotASecondary())
        {
            gm.SetPlayerWeapon(1, weapon);
        }
        else
        {
            gm.SetPlayerWeapon(wm.currentWeaponSlot, weapon);
        }
    }

    public void PurchaseAmmo(Weapon weapon)
    {
        if (wm.GetCurrentWeapon().reservedAmmo != wm.GetCurrentWeapon().maxReservedAmmo && wm.GetCurrentWeapon() == weapon)
        {
            gm.playerScore -= (int)ammoCost;
            weapon.reservedAmmo = weapon.maxReservedAmmo;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            ui.ShowPrompt(text);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            ui.ShowPrompt(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            ui.HidePrompt();
        }
    }
}
