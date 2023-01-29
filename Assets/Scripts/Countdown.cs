using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;



public class Countdown : MonoBehaviour
{
    //TextLocation
    public TMP_Text printCountdown;
    public TMP_Text printResult;

    public int countdownStart;
    private int savedStart;
    //HandTracking for position
    public HandTracking HandTracking;
    //create empty list for sending gripper postion
    public List<Vector3> graspPosList = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        //hide text
        printCountdown.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    {

    }

    public void Counter()
    {
        // Initiate the coroutine "CountdownApp"
        StartCoroutine(CountdownApp());
    }

     IEnumerator CountdownApp()
    {
        //show text 
        printCountdown.gameObject.SetActive(true);

        //store countdown amount to reset countdown when done
        savedStart = countdownStart;

        //countdown
        while (countdownStart > 0) 
        {
            //print the number
            printCountdown.text = $"Save in: {countdownStart}";

            //wait for one second...
            yield return new WaitForSeconds(1f);

            //...before reducing the countdown by one
            countdownStart--;
        }
        //when countdown done, print a final text
        printCountdown.text = "SAVE!";

        //function to save the positions of the fingers
        SavePos();

        //reset the countdown to the stored amount "savedStart"
        countdownStart = savedStart;

        //wait for one second ...
        yield return new WaitForSeconds(1f);

        //... before hiding the text again 
        printCountdown.gameObject.SetActive(false);


    }

    public void SavePos()
    {
        graspPosList.Add(HandTracking.NewCentrePosition);

        if (graspPosList.Count > 0)
        {

            printResult.gameObject.SetActive(true);
            
            printResult.text += Environment.NewLine + graspPosList[graspPosList.Count-1].ToString();

        }
    }
}
