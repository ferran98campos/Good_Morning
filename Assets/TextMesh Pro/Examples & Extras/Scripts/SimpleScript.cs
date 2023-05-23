using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TMPro.Examples
{
  

    public class SimpleScript : MonoBehaviour
    {
        //public SceneAsset sceneToLoad;
        private Animator anim;
        private GameObject canvas;
        private bool control = false;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Hola");
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
            SceneManager.LoadScene("Scenes/first-cave");
            
        }
        void Update()
        {    
           /* Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && control)
            {
                Debug.Log("Animation is Done");
                control = false;
                SceneManager.LoadScene("Scenes/first-cave");
            }*/

        }

    }
}
