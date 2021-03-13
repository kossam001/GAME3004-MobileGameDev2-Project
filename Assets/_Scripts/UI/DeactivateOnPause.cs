using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeactivateOnPause : MonoBehaviour
{
    public GameObject shopUI;
    // Update is called once per frame
    void Update()
    {
        if(Pause.gameIsPaused)
        {
            shopUI.SetActive(false);
        }
    }
}
