using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlantAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        // Maybe we can attempt to get the tile that the plant is on instead?
        // Just to ensure we are able to hit the plant properly.
        if (other.CompareTag("Plant"))
        {
            //Debug.Log("OnTriggerEnter - Plant");
            //Destroy(other.gameObject);

            // NOTE: Because the Resource for the plant is also using the health script,
            //       the plant also loses some of it's resources. This wasn't intentional
            //       but it could be used as part of the design.

            Health plantHealth = other.GetComponent<Health>();
            plantHealth.ModifyHealth(-damage);
        }
    }
}
