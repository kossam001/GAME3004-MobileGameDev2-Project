using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : InteractableObject
{
    private float accumulatedResourcePoints;

    //public float growthSpeed;

    //public int maxResourceYield1;
    //public int maxResourceYield2;
    //public int maxResourceYield3;
    //public int maxResourceYield4;
    public int maxSeeds;

    public Image uiResourceAmountIndicator;
    public Item seed;

    public int maxValueMultiplier = 1;

    // Maybe AudioClip array can be added in the future to add additional sounds...
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        int yield1 = (int) (uiResourceAmountIndicator.fillAmount * (float) seed.Yield1);
        int yield2 = (int) (uiResourceAmountIndicator.fillAmount * (float) seed.Yield2);
        int yield3 = (int) (uiResourceAmountIndicator.fillAmount * (float) seed.Yield3);
        int yield4 = (int) (uiResourceAmountIndicator.fillAmount * (float) seed.Yield4);

        GameStats.Instance.AddResources(yield1, yield2, yield3, yield4);
        accumulatedResourcePoints = 0;

        audioSource.Play();

        int seedsGenerated = (int) (uiResourceAmountIndicator.fillAmount * (float) maxSeeds);

        InventoryController.Instance.AddToInventory(Instantiate(seed), seedsGenerated);

        StatisticsTracker.Instance.UpdateStats(5, 1);
    }

    private void Update()
    {
        if (accumulatedResourcePoints <= 100.0f)
        {
            Mathf.Clamp(accumulatedResourcePoints += seed.GrowthSpeed * Time.deltaTime, 0, 100.0f);

            uiResourceAmountIndicator.fillAmount = (float)accumulatedResourcePoints / 100.0f;
        }
    }

    public float GetMaturity()
    {
        return uiResourceAmountIndicator.fillAmount;
    }

    // base value will be cost
    public float GetValue(int baseValue)
    {
        return baseValue * maxValueMultiplier * GetMaturity();
    }
}
