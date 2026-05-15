using TMPro;
using UnityEngine;

public class DDA : MonoBehaviour
{
    // Toggle
    public bool isDDAEnabled = false;

    // DDA stats
    float DDAMultiplier = 1.0f;
    public float damageMultiplier = 1.0f;
    public float healthMultiplier = 1.0f;

    // Kills to death ratio
    int deaths;
    int kills;

    public TMP_Text ddaStatsText;

    private void Update()
    {
        DrawDDAStats();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isDDAEnabled = !isDDAEnabled;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AddDeaths();
        }
    }

    private void DrawDDAStats()
    {
        ddaStatsText.text =
            "DDA Stats:\n" +
            "Damage Multiplier: " + damageMultiplier.ToString("F2") + "\n" +
            "Health Multiplier: " + healthMultiplier.ToString("F2") + "\n" +
            "Kills: " + kills + "\n" +
            "Deaths: " + deaths + "\n";
    }

    void UpdateDDA()
    {
        DDAMultiplier = (deaths + 1.0f) / (kills + 1.0f);

        damageMultiplier = Mathf.Clamp(DDAMultiplier, 0.8f, 2.0f);
        healthMultiplier = Mathf.Clamp(DDAMultiplier, 0.8f, 2.0f);
    }

    public void AddDeaths()
    {
        deaths++;
        UpdateDDA();
    }

    public void AddKills()
    {
        kills++;
        UpdateDDA();
    }
}
