using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairMenu : MonoBehaviour
{
    private Tower tower;

    [Header("UI")]
    public TMP_Text message;

    [Tooltip("Panel for insufficient funds")]
    [SerializeField] protected ItemDescriptionPanel insufficientFundsPanel;

    public void Activate(Tower _tower)
    {
        tower = _tower;
        message.text = "Cost\n\n" + "Wood: " + tower.repairCost[0] + "\nMeat: " + tower.repairCost[1] + "\nMetal: " + tower.repairCost[2] + "\nCoins: " + tower.repairCost[3];
    }

    public void Repair()
    {
        if (tower.GetComponent<Health>().IsMaxHealth()) return;
        if (!UseResources()) return;

        tower.GetComponent<Health>().ResetHealth();
    }

    private bool UseResources()
    {
        if (GameStats.Instance.UseResources(tower.repairCost[0], tower.repairCost[1], tower.repairCost[2], tower.repairCost[3]))
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
