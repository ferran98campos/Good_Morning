// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class TextScript : MonoBehaviour
// {
//     public TMP_Text text;
//     public List<string> exposition = new List<string> { "Good morning, world! ",
// 		"Welcome to another day filled with promise.", "This is your humble host,", "spinning the globe's tales on 'Good Morning World!'",
//         "Our small town seems a little quieter today,", "a touch less vibrant.", "Here's hoping for brighter days ahead!",
//         "Tonight, we dive into stories of the lost and found.", "Tune in, as every tale unraveled, every clue gathered,", "may light the path to the missing pieces."};
//     public int index = 0;
//     public int count = 0;
//     // Start is called before the first frame update
//     void Start()
//     {
//         text.text = " ";
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         text.text = exposition[index];
//         count++;
//         if(index==0 &  count % 900 == 0){ index++;}
//         else if (index < 6 & count % 1500 == 900) { index++;}
//         else if(index > 5 & index < 9 & count % 2500 == 0){ index++;}
        
//     }
// }


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
    public List<float> switchIntervals = new List<float> { 2f, 2.5f, 1.8f, 1.8f, 2.5f, 1.5f, 1.8f, 5f, 3.8f, 3.1f };

    private int index = 0;

    void Start()
    {
        text.text = " ";
        StartCoroutine(StartTextSwitchCoroutine());
    }

    private IEnumerator StartTextSwitchCoroutine()
    {
        for (int i = 0; i < exposition.Count; i++)
        {
            text.text = exposition[i];
            yield return new WaitForSeconds(switchIntervals[i]);
        }
    }
}
