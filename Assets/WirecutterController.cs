using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WirecutterController : MonoBehaviour
{

    GameObject player; 
    private KeyCode pickupKey = KeyCode.E;
    private bool inRange = false; 
    public GameObject instructionPanel;
    public Text instructionText;
    string temp; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pickupKey) && inRange) {
            temp = instructionText.text; 
            StartCoroutine(showPickupText());
            player.GetComponent<PlayerController>().toggleWireCutter();  
        }
    }

    IEnumerator showPickupText() {
        instructionText.text = "You picked up a wirecutter";
        instructionPanel.SetActive(true);
        yield return new WaitForSeconds(2); 
        instructionPanel.SetActive(false);
        instructionText.text = temp; 
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            inRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            inRange = false; 
        }
    }
}
