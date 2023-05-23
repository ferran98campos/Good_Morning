using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowPersonDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Image playerImage;
    public Image shadowPersonImage;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    private bool dialogueIsShown = true;
    public AudioSource audioSource;
    private bool audioPlayed = false;
    public GameObject playerObject;
    private ShadowPersonMovement playerController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // playerObject = GameObject.FindWithTag("ShadowPerson");
        playerController = playerObject.GetComponent<ShadowPersonMovement>();
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
                DisablePlayerController();
                dialoguePanel.SetActive(true);

                StartCoroutine(Typing());

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
        for (int i = 0; i < dialogue.Length; i++)
        {
            
            dialogueText.text = "";
            playerImage.gameObject.SetActive(false);
            shadowPersonImage.gameObject.SetActive(false);
            if (index == 1 || index == 3 || index == 5)
            {
                playerImage.gameObject.SetActive(true);
            }
            else
            {
                shadowPersonImage.gameObject.SetActive(true);
            }
            foreach (char letter in dialogue[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
            yield return new WaitForSeconds(2);
            if (i < dialogue.Length - 1)
            {
                index++;
            }
            else
            {
                zeroText();
            }
        }
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
        if (other.CompareTag("ShadowPerson"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ShadowPerson"))
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
            //playerController.enabled = false;
            playerController.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
    }

    void EnablePlayerController()
    {
        if (playerController != null)
        {
            //playerController.enabled = true;
            playerController.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
    }
}
