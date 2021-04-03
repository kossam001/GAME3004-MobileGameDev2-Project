using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    private Tower tower;

    public Button rangeButton;
    public Button fireRateButton;
    public Button strengthButton;

    public void Activate(Tower _tower)
    {
        tower = _tower;
        strengthButton.gameObject.SetActive(!tower.IsMaxStrength());
        fireRateButton.gameObject.SetActive(!tower.IsMaxFireRate());
        rangeButton.gameObject.SetActive(!tower.IsMaxRange());
    }

    public void UpgradeStrength()
    {
        tower.strength += 0.5f;
    }

    public void UpgradeFireRate()
    {
        tower.fireRate += 0.5f;
    }

    public void UpgradeRange()
    {
        tower.range += 0.5f;
    }
}
