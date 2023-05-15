using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{

    private Collider2D rock_collider;
    
    void Start() {
        rock_collider = GetComponent<Collider2D>(); 
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            rock_collider.isTrigger = true; 
        };
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            rock_collider.isTrigger = false; 
        };
    }

    
}
