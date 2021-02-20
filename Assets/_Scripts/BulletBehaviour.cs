using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Transform target;

    public float speed = 70.0f;
    public GameObject impactEffect;

    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // The bullet aproach enemy position
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Moving the bullet relative to World Space
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        Debug.Log("Hitting Target!");

        Health enemyHealth = target.GetComponent<Health>();
        enemyHealth.ModifyHealth(-damage);

        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);

        Destroy(gameObject);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }
}
