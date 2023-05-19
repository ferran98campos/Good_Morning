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
    // public AudioClip audioClip;
    public AudioSource audioSource;
    private bool audioPlayed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // audioSource.clip = audioClip;
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
        Debug.Log("播放音频");
        if (audioSource != null && !audioPlayed)
        {
            audioSource.Play();
            audioPlayed = true;
        }
    }

    void StopAudio()
    {
        Debug.Log("停止音频");
        if (audioSource != null && audioPlayed)
        {
            audioSource.Stop();
        }
    }
}
