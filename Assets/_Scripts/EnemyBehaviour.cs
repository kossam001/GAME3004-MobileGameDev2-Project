using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        speed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    public void Respawn()
    {
        transform.position = new Vector3(13, 1, 1);
    }
}
