using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inscription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inscriptionText;
    public GameObject inscriptionPanel;

    // Start is called before the first frame update
    void Start()
    {
        inscriptionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            inscriptionText.gameObject.SetActive(true);
            inscriptionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            inscriptionText.gameObject.SetActive(false);
            inscriptionPanel.SetActive(false);
        }
    }
}
