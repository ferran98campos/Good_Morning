using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCurrentScene : MonoBehaviour
{
    public void LoadScene(int sceneBuildIndex)
    {
        // SceneManager.LoadScene("house");
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
