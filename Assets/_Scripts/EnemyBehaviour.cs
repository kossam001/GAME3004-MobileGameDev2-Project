using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // NOTE: These are here right now purely for testing
    public static int numEnemiesWon = 0;
    public static int numEnemiesDefeated = 0;

    [Tooltip("The amount of damage this enemy does when reaching the base.")]
    [SerializeField]
    private int damage;

    [Tooltip("This enemy's default speed.")]
    [SerializeField]
    public float defaultSpeed;

    [SerializeField]
    AudioClip[] audioClips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<NavMeshAgent>().speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

        // Enemy reaches objective
        //if (transform.position.x <= -12)
        //{
        //    Respawn();
        //    numEnemiesWon++;
        //}

        //if(health.currentHealth <= 0)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void Respawn()
    {
        // NOTE: Because of the current hierarchy/layout of the enemy prefab. The capsule child will likely be offset with the parent. This will likely need to be changed.
        //transform.position = new Vector3(13, 2, transform.position.z);
    }

    public void PlayCollisionSound()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Waypoint")
        {
            Debug.Log("Enemy reached waypoint");
            GameStats.Instance.ModifyBaseHealth(-damage);
            gameObject.SetActive(false);
        }
    }
}
