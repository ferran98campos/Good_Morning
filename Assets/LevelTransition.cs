using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [SerializeField]
    private Text instructionText;
    public GameObject instructionPanel;

    public int sceneBuildIndex;
    private bool playerCollided = false;

    void Start()
    {
        instructionText.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (playerCollided && Input.GetKeyDown(KeyCode.E))
        {
            TransitionToNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = true;
            instructionText.gameObject.SetActive(true);
            instructionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollided = false;
            instructionText.gameObject.SetActive(false);
            instructionPanel.SetActive(false);
        }
    }

    private void TransitionToNextLevel()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
