#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalScript : MonoBehaviour
{
     Vector3 position;
     float timeToReachTarget;
     Animator anim;
     Animator anim2;
     Animator anim3;
     Animator anim4;
     Camera camera;
     Transform cameraTransform;
     Transform shadowPerson;
     GameObject canvas;
     AudioSource audio;
     public AudioClip clip;
     int control = 0;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        
        anim = GetComponent<Animator>();
        anim.Play("finalwalk");
        StartCoroutine(MoveToPosition(transform, new Vector3(0.5f, -0.4f, 0.0f), 4.0f));

        canvas = GameObject.FindWithTag("Transition");
        anim3 = canvas.GetComponent<Animator>();
        anim4 = GameObject.FindWithTag("Logo").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == 0.5f){
            anim.Play("finalidle");
            StartCoroutine(MoveCamera(cameraTransform, new Vector3(-0.5f, 0.4f, -2.0f), 4.0f, 2.3f));
            
        }

        if(control > 0 && !audio.isPlaying){
            anim4.Play("logo");
        }

    
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
   {
      var currentPos = transform.position;
      var t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, position, t);
             yield return null;
      }
    }

    public IEnumerator MoveShadow(Transform transform, Vector3 position, float timeToMove)
   {
      var currentPos = transform.position;
      var t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, position, t);
             yield return null;
      }

      anim3.Play("FadeIn");
    if(!audio.isPlaying && control < 1){
        audio.PlayOneShot(clip, 1.0f);
        control++;
        
    }
      
    }

    public IEnumerator MoveCamera(Transform transform, Vector3 position, float timeToMove, float size)
   {
      yield return new WaitForSeconds(1);
        anim2 = GameObject.FindWithTag("ShadowPerson").GetComponent<Animator>();
        anim2.Play("shadowfinalwalk");
      shadowPerson = GameObject.FindWithTag("ShadowPerson").GetComponent<Transform>();
      StartCoroutine(MoveShadow(shadowPerson, new Vector3(-0.5f, -0.4f, 0.0f), timeToMove + 3.0f));
      var currentPos = transform.position;
      var currentSize = camera.orthographicSize;
      var t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, position, t);
             camera.orthographicSize = Mathf.Lerp(currentSize, size, t);
             yield return null;
      }
    }
}
