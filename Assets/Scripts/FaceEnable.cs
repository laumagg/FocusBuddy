using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceEnable : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDissable;
    public void ToggleFace(bool enable)
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(enable);
        }

        foreach (GameObject obj in objectsToDissable)
        {
            obj.SetActive(!enable);
        }
    }
}
