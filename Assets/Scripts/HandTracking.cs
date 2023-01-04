using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using Unity.VisualScripting;
using UnityEngine.XR.OpenXR.Input;
public class HandTracking : MonoBehaviour
{
    public GameObject ObjectToGrasp;

    public GameObject sphereMarker;
    public GameObject platonicMarker;
    public GameObject knuckleMarker;

    public GameObject thumbObject;
    public GameObject indexObject;
    public GameObject middleObject;
    public GameObject knuckleObject;

    MixedRealityPose ThumbPose;
    MixedRealityPose KnucklePose;
    MixedRealityPose IndexPose;

    public Vector3 CentrePosition;
    public Vector3 NewCentrePosition;


    // Start is called before the first frame update
    void Start()
    {
        thumbObject = Instantiate(sphereMarker, this.transform);
        indexObject = Instantiate(sphereMarker, this.transform);
        middleObject = Instantiate(platonicMarker, this.transform);
        knuckleObject = Instantiate(knuckleMarker, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //hide the markers
        thumbObject.GetComponent<Renderer>().enabled = false; // red sphere
        indexObject.GetComponent<Renderer>().enabled = false; // red sphere
        middleObject.GetComponent<Renderer>().enabled = false; // blue prism
        knuckleObject.GetComponent<Renderer>().enabled = false; // green cube

        //if finger tips are visible:
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out ThumbPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out IndexPose) &&
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Right, out KnucklePose))

        {
            //show thumb marker
            thumbObject.GetComponent<Renderer>().enabled = true;
            thumbObject.transform.position = ThumbPose.Position;



            indexObject.GetComponent<Renderer>().enabled = true;
            indexObject.transform.position = IndexPose.Position;

            Vector3 CentrePosition = (indexObject.transform.position + thumbObject.transform.position) / 2f;

            middleObject.GetComponent<Renderer>().enabled = true;
            middleObject.transform.position = CentrePosition;

            //change coordinate origin to object origin
                        Vector3 ObjectsOrigin = ObjectToGrasp.transform.position;

            NewCentrePosition = CentrePosition - ObjectsOrigin;
            //Debug.Log($"NewCentrePosition: {NewCentrePosition}");



            Vector3 indexKnuckle = KnucklePose.Position;
            Vector3 pointerAxis = indexKnuckle - ((indexObject.transform.position + thumbObject.transform.position) / 2);

            Vector3 vecStart = indexKnuckle;
            Vector3 vecEnd = indexKnuckle - pointerAxis;

            //debug draw pointer line
            Debug.DrawLine(vecStart, vecEnd, new Color(0, 255, 0));
            //Debug.Log($"index : {pose.Position}");
            //Vector3 Normal = Vector3.Cross().normalized;

            knuckleObject.GetComponent<Renderer>().enabled = true;
            knuckleObject.transform.position = KnucklePose.Position;
            //Debug.Log($"knuckle: {KnucklePose}");




            //NORMAL
            Vector3 Vec1 = (indexObject.transform.position - thumbObject.transform.position);
            Vector3 Vec2 = pointerAxis;

            Vector3 normalVec = Vector3.Cross(Vec1, Vec2).normalized;


            //debug draw normal line
            Debug.DrawLine((indexObject.transform.position + thumbObject.transform.position) / 2, ((indexObject.transform.position + thumbObject.transform.position) / 2) + normalVec, new Color(0, 0, 0));

            //debug draw finger tips line
            Debug.DrawLine(indexObject.transform.position, thumbObject.transform.position, new Color(255, 0, 0));


            //Debug.DrawLine(platonicPosition, platonicPosition + new Vector3(0, 1, 0), new Color(255, 0, 0));
        }
    }
}
