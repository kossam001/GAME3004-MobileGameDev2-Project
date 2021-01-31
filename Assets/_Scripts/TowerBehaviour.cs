using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // NOTE: Maybe would be better to use some sort of enumeration here... or make each tower have it's own script...

        if (other.gameObject.tag == "Enemy" && this.gameObject.tag == "NormalTower")
        {
            Debug.Log("Enemy Detected");

            other.gameObject.GetComponent<EnemyBehaviour>().Respawn();
        }
        else if (other.gameObject.tag == "Enemy" && this.gameObject.tag == "DelayTower")
        {
            Debug.Log("Enemy Detected");
            EnemyBehaviour.numEnemiesDefeated++;

            StartCoroutine(Fired());
            
            other.gameObject.GetComponent<EnemyBehaviour>().Respawn();
        }
    }

    IEnumerator Fired()
    {
        // "Disable" Tower
        this.gameObject.transform.parent.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        this.GetComponent<SphereCollider>().enabled = false;
        Debug.Log("Tower can't shoot");

        yield return new WaitForSeconds(2);

        // "Re-enable" Tower
        this.gameObject.transform.parent.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        this.GetComponent<SphereCollider>().enabled = true;
        Debug.Log("Tower can shoot");
    }
}
