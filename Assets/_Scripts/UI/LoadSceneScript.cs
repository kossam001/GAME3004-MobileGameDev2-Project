using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void LoadSceneMode(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
