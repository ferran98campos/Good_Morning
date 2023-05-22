using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowPersonTrigger : MonoBehaviour
{
    private PlayerController playerController;
    //public GameObject playerObject;
    public GameObject shadowPerson;
    public bool playerIsClose;
    private bool NPCIsShown = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose && NPCIsShown)
        {
            NPCIsShown = false;
            shadowPerson.SetActive(true);
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
        }
    }
}
