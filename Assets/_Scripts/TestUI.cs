using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    [SerializeField]
    Text enemiesWonText;
    [SerializeField]
    Text enemiesDefeatedText;

    // Update is called once per frame
    void Update()
    {
        enemiesWonText.text = "Enemies Won: " + EnemyBehaviour.numEnemiesWon.ToString();
        enemiesDefeatedText.text = "Enemies Defeated: " + EnemyBehaviour.numEnemiesDefeated.ToString();
    }
}
