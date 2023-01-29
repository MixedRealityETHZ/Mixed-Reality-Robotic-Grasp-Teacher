using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    public TMP_Text printText;
    public HandTracking HandTracking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        printText.text = $"Centre Pose: {HandTracking.middleObject.transform.position}";
    }

    public void SetBtnText()
    {
        printText.text = "Hello Holo";
    }
}
