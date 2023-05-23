using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirecutterController : MonoBehaviour
{

    GameObject player; 
    private KeyCode pickupKey = KeyCode.E;
    private bool inRange = false; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pickupKey) && inRange) {
            //Apply logic 
            player.GetComponent<PlayerController>().holdingWireCutter = true;  
            Destroy(gameObject);
        }
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
