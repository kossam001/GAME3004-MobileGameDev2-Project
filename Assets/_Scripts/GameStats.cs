using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    private static GameStats instance;
    public static GameStats Instance { get { return instance; } }

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

    [Header("Resource Labels")]
    public TMP_Text resource1Label;
    public TMP_Text resource2Label;
    public TMP_Text resource3Label;
    public TMP_Text coinsLabel;

    [Header("Game Elements Labels")]
    public TMP_Text scoreLabel;
    public TMP_Text healthLabel;

    [Header("Resource Values")]
    public int resource1;
    public int resource2;
    public int resource3;
    public int resource4;

    [Header("Game Element Values")]
    public int coins;
    public int score;
    public int health = 20;

    public void AddResources(int yield1, int yield2, int yield3, int yield4)
    {
        resource1 += yield1;
        resource2 += yield2;
        resource3 += yield3;
        resource4 += yield4;

        resource1Label.text = resource1.ToString();
        resource2Label.text = resource2.ToString();
        resource3Label.text = resource3.ToString();
        coinsLabel.text = resource4.ToString();
    }

    public void UpdateResourcesUI()
    {
        resource1Label.text = resource1.ToString();
        resource2Label.text = resource2.ToString();
        resource3Label.text = resource3.ToString();
        coinsLabel.text = resource4.ToString();

        healthLabel.text = health.ToString();
    }

    public bool UseResources(int consumption1, int consumption2, int consumption3, int consumption4)
    {
        if (resource1 < consumption1 || resource2 < consumption2 || resource3 < consumption3 || resource4 < consumption4)
            return false;

        resource1 -= consumption1;
        resource2 -= consumption2;
        resource3 -= consumption3;
        resource4 -= consumption4;

        resource1Label.text = resource1.ToString();
        resource2Label.text = resource2.ToString();
        resource3Label.text = resource3.ToString();
        coinsLabel.text = resource4.ToString();

        return true;
    }

    public void ModifyBaseHealth(int damage)
    {
        health += damage;

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }

        healthLabel.text = health.ToString();
    }
}
