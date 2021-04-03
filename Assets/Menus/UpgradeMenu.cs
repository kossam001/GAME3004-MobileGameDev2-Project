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
    public float strengthIncrease = 1;
    public float rangeIncrease = 1;

    public void Activate(Tower _tower)
    {
        tower = _tower;
        strengthButton.gameObject.SetActive(!tower.IsMaxStrength());
        fireRateButton.gameObject.SetActive(!tower.IsMaxFireRate());
        rangeButton.gameObject.SetActive(!tower.IsMaxRange());

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    public void UpgradeStrength()
    {
        tower.strength += strengthIncrease;

        if (tower.IsMaxStrength())
            strengthButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    public void UpgradeFireRate()
    {
        tower.fireRate += fireRateIncrease;

        if (tower.IsMaxFireRate())
            fireRateButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }

    public void UpgradeRange()
    {
        tower.range += rangeIncrease;

        if (tower.IsMaxRange())
            rangeButton.gameObject.SetActive(false);

        if (tower.IsMaxStats())
            message.text = "MAXED";
    }
}
