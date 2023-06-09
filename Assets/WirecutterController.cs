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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pickupKey) && inRange) {
            // StartCoroutine(showPickupText());
            if(!player.GetComponent<PlayerController>().isHoldingWireCutter())
                player.GetComponent<PlayerController>().toggleWireCutter(); 
            Destroy(gameObject); 
        }
    }

    // IEnumerator showPickupText() {

    //     //string temp = instructionText.text; 
    //     //instructionText.text = "You picked up a wirecutter";
    //     //instructionPanel.SetActive(true);
    //     //yield return new WaitForSeconds(2); 
    //     //instructionPanel.SetActive(false);
    //     //instructionText.text = temp; 
    //     //Just wait a bit more to avoid bugs when destroying
        
    // }

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
