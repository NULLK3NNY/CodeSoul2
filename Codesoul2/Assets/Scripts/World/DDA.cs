using TMPro;
using UnityEngine;

public class DDA : MonoBehaviour
{
    // Enable
    public bool activated;
    // Player reference
    Player player;
    WeaponManager weapon;
    // Player variables
    int playerHP;
    double playerDeaths;
    // Adjustable Variables
    public double DDA_PointRewardMultiplier;
    public double DDA_DiscountMultiplier;
    // UI
    public TMP_Text DDA_PlayerDeathTextCounter;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            playerDeaths++;
            LowerPrices();
        }

        if (activated)
        {
            // Multipliers tied to player death count
            DDA_DiscountMultiplier = playerDeaths * 0.05;
            DDA_PointRewardMultiplier = playerDeaths * 0.05;

            UpdateUI();
        }
        else
        {

        }
    }

    void UpdateUI()
    {
        DDA_PlayerDeathTextCounter.text = "Deaths: " + playerDeaths.ToString();
    }

    void LowerPrices()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Wallbuy").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Wallbuy")[i].GetComponent<Wallbuy>().cost -= DDA_DiscountMultiplier;
            GameObject.FindGameObjectsWithTag("Wallbuy")[i].GetComponent<Wallbuy>().ammoCost -= DDA_DiscountMultiplier;
        }
    }
}
