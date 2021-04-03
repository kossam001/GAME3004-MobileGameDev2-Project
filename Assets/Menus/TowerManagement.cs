using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TowerManagement : MonoBehaviour
{
    private static TowerManagement instance;
    public static TowerManagement Instance { get { return instance; } }

    public GameObject towerMenu;
    public LayerMask rayLayer;

    private bool clicked = false;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, rayLayer))
        {
            MenuManagement.Instance.SetActiveMenu(towerMenu);
            GameObject targetTower = hit.collider.gameObject;
        }
    }
}
