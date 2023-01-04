using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTest : MonoBehaviour
{
    public TMP_Text printTestText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TestText()
    {
        printTestText.text = "Hello Holo";

    }
}
