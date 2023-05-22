using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    public TMP_Text text;
    public List<string> exposition = new List<string> { "Good morning, world! ",
		"Welcome to another day filled with promise.", "This is your humble host,", "spinning the globe's tales on 'Good Morning World!'",
        "Our small town seems a little quieter today,", "a touch less vibrant.", "Here's hoping for brighter days ahead!",
        "Tonight, we dive into stories of the lost and found.", "Tune in, as every tale unraveled, every clue gathered,", "may light the path to the missing pieces."};
    public int index = 0;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        text.text = exposition[index];
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if(index==0 &  count % 900 == 0){ index++; }
        if (index<6 & count % 1500 == 0) { index++; }
        else if(count % 1700 == 0)
        {
            index++;
        }
        text.text = exposition[index];
    }
}
