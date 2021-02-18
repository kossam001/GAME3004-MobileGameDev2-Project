using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// Original Health Script taken from Jason Weimann Unity tutorial

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    public int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        currentHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ModifyHealth(-10);
        //}
    }
}
