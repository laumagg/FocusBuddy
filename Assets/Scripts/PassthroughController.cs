using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    [SerializeField] private OVRPassthroughLayer underlayPt;
    [SerializeField] private float startOpacity = 0.5f;

    private void Start()
    {
        if (underlayPt == null) return;

        underlayPt.enabled = true;
        underlayPt.textureOpacity = startOpacity;
    }


}
