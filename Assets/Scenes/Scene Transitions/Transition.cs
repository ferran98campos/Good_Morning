#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
 //public SceneAsset sceneToLoad;
        private Animator anim;
        private GameObject canvas;
        private bool control = false;
        public SceneAsset followingScene;

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
            canvas = GameObject.FindWithTag("Transition");
            anim = canvas.GetComponent<Animator>();
            anim.Play("FadeOut");
        }

        IEnumerator DelayedTransition(float _delay = 0){
            yield return new WaitForSeconds(_delay);
            SceneManager.LoadScene(followingScene.name);
            
        }
        void Update()
        {    
        }
}
