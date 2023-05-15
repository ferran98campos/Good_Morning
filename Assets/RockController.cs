using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{

    private GameObject forcefield;
    
    void start() {
        forcefield = GameObject.Find("ForceField");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collision other) {
        
    }
}
