using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescriptionPanel : MonoBehaviour
{
    public TMP_Text itemDescriptionText;

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
