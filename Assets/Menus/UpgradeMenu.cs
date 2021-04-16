using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    private Tower tower;

    [Header("UI")]
    public Button rangeButton;
    public Button fireRateButton;
    public Button strengthButton;
    public TMP_Text message;

    public float fireRateIncrease = 0.5f;
    public int strengthIncrease = 1;
    public float rangeIncrease = 1;

    [Tooltip("Panel for insufficient funds")]
    [SerializeField] protected ItemDescriptionPanel insufficientFundsPanel;

    public void Activate(Tower _tower)
    {
        tower = _tower;
        strengthButton.gameObject.SetActive(!tower.IsMaxStrength());
        fireRateButton.gameObject.SetActive(!tower.IsMaxFireRate());
        rangeButton.gameObject.SetActive(!tower.IsMaxRange());

        if (tower.IsMaxStats())
            message.text = "MAXED";
        else
            message.text = "Cost\n\n" + "Wood: " + tower.upgradeCost[0] + "\nMeat: " + tower.upgradeCost[1] + "\nMetal: " + tower.upgradeCost[2] + "\nCoins: " + tower.upgradeCost[3];
    }

    public void UpgradeStrength()
    {
        if (!UseResources()) return;

        tower.strength += strengthIncrease;

        StatisticsTracker.Instance.UpdateStats(7, 1);

        if (tower.IsMaxStrength())
            strengthButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    public void UpgradeFireRate()
    {
        if (!UseResources()) return;

        tower.fireRate += fireRateIncrease;

        StatisticsTracker.Instance.UpdateStats(7, 1);

        if (tower.IsMaxFireRate())
            fireRateButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    public void UpgradeRange()
    {
        if (!UseResources()) return;

        tower.range += rangeIncrease;

        StatisticsTracker.Instance.UpdateStats(7, 1);

        if (tower.IsMaxRange())
            rangeButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    private bool UseResources()
    {
        if (GameStats.Instance.UseResources(tower.upgradeCost[0], tower.upgradeCost[1], tower.upgradeCost[2], tower.upgradeCost[3]))
        {
            return true;
        }
        else
        {
            insufficientFundsPanel.gameObject.SetActive(true);

            return false;
        }
    }
}
