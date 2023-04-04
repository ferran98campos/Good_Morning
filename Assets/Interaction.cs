using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }
    

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);


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
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class NPC : MonoBehaviour
// {
//     public GameObject dialoguePanel;
//     public TextMeshProUGUI dialogueText;
//     public string[] dialogue;
//     private int index = 0;

//     public float wordSpeed;
//     public bool playerIsClose;


//     void Start()
//     {
//         dialogueText.text = "";
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
//         {
//             if (!dialoguePanel.activeInHierarchy)
//             {
//                 dialoguePanel.SetActive(true);
//                 StartCoroutine(Typing());
//             }
//             else if (dialogueText.text == dialogue[index])
//             {
//                 NextLine();
//             }

//         }
//         if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
//         {
//             RemoveText();
//         }
//     }

//     public void RemoveText()
//     {
//         dialogueText.text = "";
//         index = 0;
//         dialoguePanel.SetActive(false);
//     }

//     IEnumerator Typing()
//     {
//         foreach(char letter in dialogue[index].ToCharArray())
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(wordSpeed);
//         }
//     }

//     public void NextLine()
//     {
//         if (index < dialogue.Length - 1)
//         {
//             index++;
//             dialogueText.text = "";
//             StartCoroutine(Typing());
//         }
//         else
//         {
//             RemoveText();
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerIsClose = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerIsClose = false;
//             RemoveText();
//         }
//     }
// }