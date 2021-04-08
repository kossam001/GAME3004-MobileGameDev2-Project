using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class TowerManagement : MonoBehaviour
{
    private static TowerManagement instance;
    public static TowerManagement Instance { get { return instance; } }

    [Header("Menus")]
    public GameObject towerMenu;
    public GameObject upgradeMenu;
    public GameObject repairMenu;

    [Header("Tower specific buttons")]
    public GameObject repairButton;
    public GameObject upgradeButton;

    public TMP_Text towerNameLabel;

    public LayerMask rayLayer;

    private Tower tower;

    [SerializeField] private float refundRatio;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Pause.gameIsPaused)
        {
            Click();
        }
    }

    private void Click()
    {
        if (towerMenu.activeInHierarchy) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ((Physics.Raycast(ray, out hit, 1000, rayLayer)) && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            SetActiveMenu(towerMenu);

            GameObject targetTower = hit.collider.gameObject;
            tower = targetTower.GetComponent<Tower>();
            towerNameLabel.text = tower.towerName;

            if (tower.GetComponent<Resource>())
            {
                repairButton.SetActive(false);
                upgradeButton.SetActive(false);
            }
            else
            {
                repairButton.SetActive(true);
                upgradeButton.SetActive(true);
            }
        }
    }

    public void RemoveTower()
    {
        tower.tile.RemoveTower();
        towerMenu.SetActive(false);

        if (tower.GetComponent<Resource>())
        {
            GameStats.Instance.AddResources(0, 0, 0, (int)tower.GetComponent<Resource>().GetValue(tower.resourceCost[3]));
        }
        else
        {
            GameStats.Instance.AddResources((int)((float)tower.resourceCost[0] * refundRatio),
                                        (int)((float)tower.resourceCost[1] * refundRatio),
                                        (int)((float)tower.resourceCost[2] * refundRatio),
                                        tower.SellPrice);
        }

        tower = null;

        StatisticsTracker.Instance.UpdateStats(6, 1);
    }

    public void SetActiveMenu(GameObject menu)
    {
        MenuManagement.Instance.SetActiveMenu(menu);
        
        if (ReferenceEquals(upgradeMenu, MenuManagement.Instance.activeMenu))
        {
            upgradeMenu.GetComponent<UpgradeMenu>().Activate(tower);
        }
        if (ReferenceEquals(repairMenu, MenuManagement.Instance.activeMenu))
        {
            repairMenu.GetComponent<RepairMenu>().Activate(tower);
        }
    }
}
