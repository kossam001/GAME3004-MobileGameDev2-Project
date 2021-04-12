using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
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
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.speed = defaultSpeed;
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
