using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : InteractableObject
{
    public int resourcePoints;
    public int growthSpeed;

    public int maxResource;
    public Image uiResourceAmountIndicator;

    public override void Use()
    {
        Debug.Log(resourcePoints);
        resourcePoints = 0;
    }

    private void Update()
    {
        if (resourcePoints <= maxResource)
        {
            Mathf.Clamp(resourcePoints += (int)(growthSpeed * Time.deltaTime), 0, maxResource);

            uiResourceAmountIndicator.fillAmount = (float) resourcePoints / (float)maxResource;
        }
    }
}
