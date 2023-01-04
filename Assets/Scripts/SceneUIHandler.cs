using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIHandler : MonoBehaviour
{
    public GameObject abstractDataViz;
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
        Mesh newMesh = meshScaleList[meshScalePairIndex].mesh;
        abstractDataViz.GetComponent<MeshFilter>().mesh = newMesh;
        abstractDataViz.transform.localScale = meshScaleList[meshScalePairIndex].scaleVector;

        BoxCollider dataVizBoxCollider = abstractDataViz.GetComponent<BoxCollider>();
        dataVizBoxCollider.size = newMesh.bounds.size;
        dataVizBoxCollider.center = newMesh.bounds.center;
        dataVizBoxCollider.GetComponent<BoundsControl>().UpdateBounds();
    }
}
