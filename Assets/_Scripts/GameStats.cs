using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Game Element Values")]
    public int coins;
    public int score;
    public int health;

    public void AddResources(int yield1, int yield2, int yield3)
    {
        resource1 += yield1;
        resource2 += yield2;
        resource3 += yield3;

        resource1Label.text = resource1.ToString();
        resource2Label.text = resource2.ToString();
        resource3Label.text = resource3.ToString();
    }

    public void UseResources(int consumption1, int consumption2, int consumption3)
    {
        resource1 -= consumption1;
        resource2 -= consumption2;
        resource3 -= consumption3;

        resource1Label.text = resource1.ToString();
        resource2Label.text = resource2.ToString();
        resource3Label.text = resource3.ToString();
    }
}
