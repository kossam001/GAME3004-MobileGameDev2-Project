using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class BulletPooling
{
    private static BulletPooling m_instance = null;

    private BulletPooling()
    {
        _Initialize();
    }

    public static BulletPooling Instance()
    {
        if (m_instance == null)
        {
            m_instance = new BulletPooling();
        }

        return m_instance;
    }


    // Prefab references
    private GameObject bucketBullet;
    private GameObject pumpkinBullet;

    public int MaxBullets { get; set; }

    private Queue<GameObject> m_bucketPool;
    private Queue<GameObject> m_pumpkinPool;


    // Load prefabs using for instantiating any types of Bullet
    private void _Initialize()
    {
        bucketBullet = Resources.Load("Prefabs/Bullet_Bucket") as GameObject;
        pumpkinBullet = Resources.Load("Prefabs/Bullet_Pumpkin") as GameObject;
    }


    // Initiate a Bullet depends on what type of Bullet
    public GameObject createBullet(BulletType type = BulletType.RANDOM)
    {
        if (type == BulletType.RANDOM)
        {
            var randomBullet = Random.Range(1, Enum.GetNames(typeof(BulletType)).Length);
            type = (BulletType)randomBullet;
        }

        GameObject tempBullet = null;
        switch (type)
        {
            case BulletType.BUCKET_BULLET:
                tempBullet = MonoBehaviour.Instantiate(bucketBullet);
                //tempBullet.GetComponent<BulletBehaviour>().damage = 20;
                break;
            case BulletType.PUMPKIN_BULLET:
                tempBullet = MonoBehaviour.Instantiate(pumpkinBullet);
                //tempBullet.GetComponent<BulletBehaviour>().damage = 10;
                break;
        }

        tempBullet.SetActive(false);

        return tempBullet;
    }


    // Create a Bullet pool belongs to input type and number of Bullet as well
    public void Init(int max_bullets = 50, BulletType b_type = BulletType.BUCKET_BULLET, BulletType p_Type = BulletType.PUMPKIN_BULLET)
    {
        MaxBullets = max_bullets;
        _BuildBulletPool(b_type);
        _BuildBulletPool(p_Type);
    }


    // Build Bucket Bullet pool

    private void _BuildBulletPool(BulletType type = BulletType.RANDOM)
    {
        // create empty Queue structure
        Queue<GameObject> pool = new Queue<GameObject>();

        switch (type)
        {
            case BulletType.BUCKET_BULLET:
                m_bucketPool = pool;
                break;
            case BulletType.PUMPKIN_BULLET:
                m_pumpkinPool = pool;
                break;
            default:
                m_bucketPool = pool;
                break;
        }

        for (int count = 0; count < MaxBullets; count++)
        {
            var tempBullet = createBullet(type);
            pool.Enqueue(tempBullet);
        }
    }


    // Get a Bullet from pool to process
    public GameObject GetBullet(Vector3 position, Quaternion rotation, BulletType type = BulletType.RANDOM)
    {
        GameObject newBullet;// = new GameObject();

        switch (type)
        {
            case BulletType.BUCKET_BULLET:
                newBullet = m_bucketPool.Dequeue();
                break;
            case BulletType.PUMPKIN_BULLET:
                newBullet = m_pumpkinPool.Dequeue();
                break;
            default:
                newBullet = m_bucketPool.Dequeue();
                break;
        }

        //var newBullet = m_bucketPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        newBullet.transform.rotation = rotation;
        return newBullet;
    }


    // Check the remaining Bullets inside pool
    public bool HasBullets(BulletType type = BulletType.RANDOM)
    {
        switch(type)
        {
            case BulletType.BUCKET_BULLET:
                return m_bucketPool.Count > 0;
            case BulletType.PUMPKIN_BULLET:
                return m_pumpkinPool.Count > 0;
            default:
                return m_bucketPool.Count > 0;
        }

    }


    // Return Bullet back to Bullet pool
    public void ReturnBullet(GameObject returnedBullet, BulletType type = BulletType.RANDOM)
    {
        returnedBullet.SetActive(false);

        switch (type)
        {
            case BulletType.BUCKET_BULLET:
                m_bucketPool.Enqueue(returnedBullet);
                break;
            case BulletType.PUMPKIN_BULLET:
                m_pumpkinPool.Enqueue(returnedBullet);
                break;
            default:
                m_bucketPool.Enqueue(returnedBullet);
                break;
        }
        
    }

}
