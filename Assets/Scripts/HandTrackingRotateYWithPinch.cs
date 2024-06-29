using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static OVRHand;

public class HandTrackingRotateYWithPinch : MonoBehaviour
{
    public float Speed = 10;

    public UnityEvent StartRotation;
    public UnityEvent StopRotation;

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
        float rotationY = Math.Abs(handMovement.x) * 100f * Speed;

        StartRotation.Invoke();

        objectToRotate.Rotate(Vector3.up, rotationY * Time.deltaTime);
    }

    private bool IsHandInCollider(OVRHand hand)
    {
        // Überprüfung, ob die Hand im Collider ist
        Vector3 handPosition = hand.transform.position + hand.transform.forward * 0.2f;
        bool inCollider = objectCollider.bounds.Contains(handPosition);


        return inCollider;
    }
}
