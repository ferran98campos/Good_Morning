
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneBefore : MonoBehaviour
{
 //public SceneAsset sceneToLoad;
        private Animator anim;
        private GameObject canvas;
        private bool control = false;
        private int followingScene;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                control = true;
                anim.Play("FadeIn");
                StartCoroutine(DelayedTransition(anim.GetCurrentAnimatorStateInfo(0).length));
            }
        }
        
        void Start()
        {
            followingScene = SceneManager.GetActiveScene().buildIndex-1;
            canvas = GameObject.FindWithTag("Transition");
            anim = canvas.GetComponent<Animator>();
            anim.Play("FadeOut");
        }

        IEnumerator DelayedTransition(float _delay = 0){
            yield return new WaitForSeconds(_delay);
            SceneManager.LoadScene(followingScene);
            
        }
        void Update()
        {    
        }
}
