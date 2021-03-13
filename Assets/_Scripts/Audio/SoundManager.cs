using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    private EnemyBehaviour[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        enemy = FindObjectsOfType<EnemyBehaviour>();
        //Debug.Log(enemy.Length);

        if (enemy.Length > 0)
        {
            musicSource.clip = battleTheme;
            if(!musicSource.isPlaying)
            {
                musicSource.Play();
            }
            
        }
        else
        {
            musicSource.clip = mainTheme;
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }
}
