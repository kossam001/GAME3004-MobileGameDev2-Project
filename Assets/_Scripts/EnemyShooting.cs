using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float range = 10.0f;
    public string targetTag = "Tower";
    public float fireRate = 1.0f;
    private float fireCountDown = 0.0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private AudioSource audioSource;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1.0f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        //GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        GameObject bulletGO = new GameObject();

        if (BulletPooling.Instance().HasBullets(BulletType.BUCKET_BULLET))
        {
            bulletGO = BulletPooling.Instance().GetBullet(firePoint.position, firePoint.rotation, BulletType.BUCKET_BULLET);
        }
        else
        {
            return;
        }

        Debug.Log("Enemy Shooting");
        audioSource.Play();

        BulletBehaviour bullet = bulletGO.GetComponent<BulletBehaviour>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
