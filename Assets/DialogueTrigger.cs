using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    private bool dialogueIsShown = true;
    public AudioSource audioSource;
    private bool audioPlayed = false;
    public GameObject playerObject;
    private PlayerController playerController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = playerObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerIsClose && dialogueIsShown)
        {
            dialogueIsShown = false;
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                DisablePlayerController();
                PlayAudio();
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        StopAudio(); 
        EnablePlayerController();
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        yield return new WaitForSeconds(3);
        zeroText();
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    void PlayAudio()
    {
        if (audioSource != null && !audioPlayed)
        {
            audioSource.Play();
            audioPlayed = true;
        }
    }

    void StopAudio()
    {
        if (audioSource != null && audioPlayed)
        {
            audioSource.Stop();
        }
    }

    void DisablePlayerController()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    void EnablePlayerController()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
