using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{
    private GameObject activeMenu;

    public void SetActiveMenu(GameObject menu)
    {
        
        if (activeMenu)
            activeMenu.SetActive(false);

        activeMenu = menu;
        activeMenu.SetActive(true);
    }
}
