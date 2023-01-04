using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;

public class Trajectory : MonoBehaviour
{
    MixedRealityPose Index;

    public TMP_Text printCountdown;
    public HandTracking HandTracking;
    public GameObject trajectorySphere;
    public int countdownStart;
    private int savedStart;
    private bool TrajectoryStatus = false;
    private GameObject trajectorySpheres;
    
    [SerializeField] private List<Vector3> graspPosList;
    public TMP_Text printResultRemove;

    public List<Vector3> trajPosList = new List<Vector3>();
    public List<GameObject> trajSpheresList = new List<GameObject>();


    // Update is called once per frame
    public void Update()
    {
        //Instantiate(trajectorySphere, HandTracking.NewCentrePosition, Quaternion.identity);

        float pinchStatus = Microsoft.MixedReality.Toolkit.Utilities.HandPoseUtils.CalculateIndexPinch(Microsoft.MixedReality.Toolkit.Utilities.Handedness.Right);
        float pinchThreshold = 0.5f;

        //check if right hand is visible
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out Index))
        {
            //Debug.Log(pinchStatus);
            //Debug.Log("right hand visible");

            //check at every frame if the TrajectoryStatus has been changed to true                                 3
            if (TrajectoryStatus == true)
            {
                //Debug.Log("teaching status is true");

                // if right hand index pinches
                if (pinchStatus > pinchThreshold)
                {
                    //start the coroutine TrajectoryTracer
                    StartCoroutine(TrajectoryTracer());                                                             //4

                }
                //if right hand doesn't pinch anymore
                else if (pinchStatus <= pinchThreshold)
                {
                    //set back the TrajectoryStatus to false to stop the recording
                    TrajectoryStatus = false;
                }
            }
            //check at every frame if the TrajectoryStatus has been changed to false
            else if (TrajectoryStatus == false)
            {
               // Debug.Log("done saving");
            }

        }

    }

    //run when teach button is pressed                                                                              1
    public void TeachTrajectory()
    {
        //Debug.Log("Button pressed");

        //Initiate the coroutine "CountdownApp"
        StartCoroutine(TeachTrajectoryApp());                                                                       //2
    }

    //statrted through the function TeachTrajectory                                                                 2
    IEnumerator TeachTrajectoryApp()
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

            //Debug.Log(countdownStart);

            //wait for one second...
            yield return new WaitForSeconds(1f);

            //...before reducing the countdown by one
            countdownStart--;
        }

        //when countdown done, print a final text
        printCountdown.text = "SAVING!";

        //function to save the positions of the fingers
        //StartCoroutine(TeachTrajectoryApp());
        //TrajectoryTracer();

        //change TrajectoryStatus to true to initiates the if-loop in the Update()
        TrajectoryStatus = true;                                                                                    //3

        //Debug.Log($"trajectoryStatus = {TrajectoryStatus}");

        //reset the countdown to the stored amount "savedStart"
        countdownStart = savedStart;

        //wait for one second ...
        yield return new WaitForSeconds(1f);

        //... before hiding the text again 
        printCountdown.gameObject.SetActive(false);
    }

    //start the recording of the position                                                                           4
    public IEnumerator TrajectoryTracer()                                                                

    {
        {
            // variable for waiting time between instantiation of every sphere
            float waitTime = 0.3f;
            
            // set status back to false to stop the loop
            TrajectoryStatus = false;

            // instantiate the sphere at the index position
            trajectorySpheres = Instantiate(trajectorySphere, Index.Position, Quaternion.identity);

            // add the instantiated sphere to a list for destroying them later
            trajSpheresList.Add(trajectorySpheres);

            // add the position to a list 
            trajPosList.Add(Index.Position);

            // Wait for waitTime between every trajectory sphere
            yield return new WaitForSeconds(waitTime);

            // set TrajectoryStatus back to true again to start the loop in Update()
            TrajectoryStatus = true;
        }
    }
    public void Reset()
    {
        // loop through each traj sphere to destroy them
        foreach (GameObject trajSphere in trajSpheresList)
        {
            // destroy each traj sphere
            Destroy(trajSphere);
        }

        // empty list of trajectory positions
        trajPosList.Clear();
        
        // empty list of grasp position
        graspPosList.Clear();

        // replacing text in console with original = deleting
        string txt = "Taught poses:";
        printResultRemove.text = txt;
    }

}


