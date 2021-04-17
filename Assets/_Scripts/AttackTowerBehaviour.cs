using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTowerBehaviour : Tower
{

    private Transform target;

    private float fireCountDown = 0.0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10.0f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public BulletType bulletType;

    private AudioSource audioSource;

    private string originalTargetTag;
    public GameObject childObjectContainingTheTargetTag;


    void Awake()
    {
        originalTargetTag = childObjectContainingTheTargetTag.tag;
    }

    public void TurnOn()
    {
        childObjectContainingTheTargetTag.tag = originalTargetTag;

        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    private void OnEnable()
    {
        target = null;
    }

    private void OnDisable()
    {
        CancelInvoke("UpdateTarget");
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

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
        if(target == null)
        {
            return;
        }

        // Lock on Target Enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        if(fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1.0f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        if(BulletPooling.Instance().HasBullets(bulletType))
        {
            GameObject bulletGO = BulletPooling.Instance().GetBullet(firePoint.position, firePoint.rotation, bulletType);

            BulletBehaviour bullet = bulletGO.GetComponent<BulletBehaviour>();
            bullet.damage = (int)((float)bullet.damage * strength);

            Debug.Log("Shooting");
            audioSource.Play();

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
