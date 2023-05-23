using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence : MonoBehaviour
{
    private Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a= GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D player){
        if(player.GetComponent<PlayerController>().isHoldingWireCutter()){
            a.Play("fence-animation");
            Destroy(gameObject, 0.5f);
        };
    }
}
