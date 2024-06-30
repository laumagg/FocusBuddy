using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static OVRHand;

public class HandTrackingRotateYWithPinch : MonoBehaviour
{
    public float Speed = 10;
    public Vector3 DesiredAxis = Vector3.up;

    public UnityEvent StartRotation;
    //public UnityEvent StopRotation;

    public Transform objectToRotate;
    public Collider objectCollider;

    public OVRHand hand;
    public bool isIndexFingerPinching;
    public bool IsIndexFingerPinching
    {
        get { return isIndexFingerPinching; }
        set
        {
            if (isIndexFingerPinching != value)
            {
                isIndexFingerPinching = value;
            }
        }
    }

    private Vector3 previousHandPosition;

    public bool inCollider;

    public bool startedRotation;

    void Start()
    {
        hand = GetComponent<OVRHand>();
    }

    void Update()
    {
        IsIndexFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
        inCollider = IsHandInCollider(hand);
        if (!IsIndexFingerPinching || !inCollider) return;

        Vector3 currentHandPosition = hand.transform.position;
        Vector3 handMovement = currentHandPosition - previousHandPosition;
        //float movement = 0;
        //if (DesiredAxis.Equals(Vector3.up))
        //    movement = handMovement.y;
        //if (DesiredAxis.Equals(Vector3.right))
        //    movement = handMovement.x;
        //if (DesiredAxis.Equals(Vector3.forward))
        //    movement = handMovement.z;

        float rotationY = Math.Abs(handMovement.x) * 100f * Speed;

        if (!startedRotation)
        {
            StartRotation.Invoke();
            startedRotation = true;
        }

        objectToRotate.Rotate(DesiredAxis, rotationY * Time.deltaTime);
    }

    private bool IsHandInCollider(OVRHand hand)
    {
        // Überprüfung, ob die Hand im Collider ist
        Vector3 handPosition = hand.transform.position + hand.transform.forward * 0.2f;
        bool inCollider = objectCollider.bounds.Contains(handPosition);


        return inCollider;
    }
}
