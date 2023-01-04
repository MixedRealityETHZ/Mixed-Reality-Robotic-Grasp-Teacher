using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneUIHandler : MonoBehaviour
{
    public GameObject gameObjectToToggle;
    void Start()
    {
        // set env inactive as app starts
        gameObjectToToggle.SetActive(false);
    }

    public List<GameObject> abstractDataVizList = new List<GameObject>();
    //public GameObject abstractDataViz;
    [Serializable]
    public struct MeshScalePair
        {
        public Mesh mesh;
        public Vector3 scaleVector;
        }
    public List <MeshScalePair> meshScaleList = new List<MeshScalePair>();

    public void ToggleObject(GameObject gameObjectToToggle)
    {
        gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
    }

    public void SwitchMesh(int meshScalePairIndex)
    {
        foreach (var abstractDataViz in abstractDataVizList)
        {
            Mesh newMesh = meshScaleList[meshScalePairIndex].mesh;
            abstractDataViz.GetComponent<MeshFilter>().mesh = newMesh;
            abstractDataViz.transform.localScale = meshScaleList[meshScalePairIndex].scaleVector;

            BoxCollider dataVizBoxCollider = abstractDataViz.GetComponent<BoxCollider>();
            dataVizBoxCollider.size = newMesh.bounds.size;
            dataVizBoxCollider.center = newMesh.bounds.center;
            dataVizBoxCollider.GetComponent<BoundsControl>().UpdateBounds();
        }
    }
}
