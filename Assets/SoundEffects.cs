using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip FootStep;
    //A check so that another routine is not started
    bool running = false; 
    
    void Start()
    {
        sfx.volume = 0.2f;
    }

    public IEnumerator PlayFootStep(float WaitTime) {
        running = true; 
        sfx.clip = FootStep;
        sfx.Play(); 
        yield return new WaitForSeconds(WaitTime);
        running = false; 
    }

    public bool IsRunning() {
        return running; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
