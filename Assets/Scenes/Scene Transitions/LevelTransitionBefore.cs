
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionBefore : MonoBehaviour
{
    [SerializeField]
    private Text instructionText;
    public GameObject instructionPanel;
    public int followingScene;
    public int sceneBuildIndex;
    private bool playerCollided = false;
    private Animator anim;
    private GameObject canvas;
    
    void Start()
    {
        followingScene = SceneManager.GetActiveScene().buildIndex-1;
        canvas = GameObject.FindWithTag("Transition");
        anim = canvas.GetComponent<Animator>();
        instructionText.gameObject.SetActive(false);
        anim.Play("FadeOut");
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
        anim.Play("FadeIn");
        StartCoroutine(DelayedTransition(anim.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator DelayedTransition(float _delay = 0){
            yield return new WaitForSeconds(_delay);
            SceneManager.LoadScene(followingScene);
    }
}
