using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : InteractableObject
{
    public int resourcePoints;
    public int growthSpeed;

    public override void Use()
    {
        Debug.Log(resourcePoints);
        resourcePoints = 0;
    }

    private void Update()
    {
        resourcePoints += (int)(growthSpeed * Time.deltaTime);
    }
}
