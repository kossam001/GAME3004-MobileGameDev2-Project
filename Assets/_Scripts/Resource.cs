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
    public int maxResourceYield4;
    public int maxSeeds;

    public Image uiResourceAmountIndicator;
    public Item seed;

    // Maybe AudioClip array can be added in the future to add additional sounds...
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        int yield1 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield1);
        int yield2 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield2);
        int yield3 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield3);
        int yield4 = (int) (uiResourceAmountIndicator.fillAmount * (float) maxResourceYield4);

        GameStats.Instance.AddResources(yield1, yield2, yield3, yield4);
        accumulatedResourcePoints = 0;

        audioSource.Play();

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
