using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnimation : MonoBehaviour
{
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D()
    {
        ani.Play("window");
    }
}
