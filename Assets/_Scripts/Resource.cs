using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : InteractableObject
{
    private float accumulatedResourcePoints;

    public float growthSpeed;

    public int maxResourceYield1;
    public int maxResourceYield2;
    public int maxResourceYield3;
    public int maxSeeds;

    public Image uiResourceAmountIndicator;
    public Item seed;

    public override void Use()
    {
        int yield1 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield1);
        int yield2 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield2);
        int yield3 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield3);

        GameStats.Instance.AddResources(yield1, yield2, yield3);
        accumulatedResourcePoints = 0;

        int seedsGenerated = (int) (uiResourceAmountIndicator.fillAmount * (float) maxSeeds);

        InventoryController.Instance.AddToInventory(Instantiate(seed), seedsGenerated);
    }

    private void Update()
    {
        if (accumulatedResourcePoints <= 100.0f)
        {
            Mathf.Clamp(accumulatedResourcePoints += growthSpeed * Time.deltaTime, 0, 100.0f);

            uiResourceAmountIndicator.fillAmount = (float)accumulatedResourcePoints / 100.0f;
        }
    }
}
