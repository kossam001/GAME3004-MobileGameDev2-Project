using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{
    private static MenuManagement instance;
    public static MenuManagement Instance { get { return instance; } }

    public GameObject activeMenu;

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

    public void SetActiveMenu(GameObject menu)
    {
        CloseMenu();

        activeMenu = menu;
        activeMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        if (activeMenu)
            activeMenu.SetActive(false);
    }
}
