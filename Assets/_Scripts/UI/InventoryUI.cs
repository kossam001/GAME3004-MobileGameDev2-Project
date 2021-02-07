using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject towerPanel;
    [SerializeField]
    GameObject seedsPanel;
    [SerializeField]
    GameObject miscPanel;

    //private GameObject currentPanel;

    private void Start()
    {
        // Open Tower Panel by Default
        towerPanel.SetActive(true);
    }

    public void OpenTowerPanelButton()
    {
        seedsPanel.SetActive(false);
        miscPanel.SetActive(false);
        towerPanel.SetActive(true);
    }

    public void OpenSeedPanelButton()
    {
        towerPanel.SetActive(false);
        miscPanel.SetActive(false);
        seedsPanel.SetActive(true);
    }

    public void OpenMiscPanelButton()
    {
        towerPanel.SetActive(false);
        seedsPanel.SetActive(false);
        miscPanel.SetActive(true);
    }

    public void CloseInventoryButton()
    {
        this.gameObject.SetActive(false);
    }

    public void PlantCarrotButton()
    {
        CloseInventoryButton();
        PlantManager.PlantingOn();

    }
}
