using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PassthroughController : MonoBehaviour
{
    [SerializeField] private OVRPassthroughLayer underlayPt;
    [SerializeField] private float startOpacity = 0.5f;
    [SerializeField] private float transitionSpeed = 0.1f;

    //TODO trigger changing opacity from Pomodoro timer

    private void Start()
    {
        if (underlayPt == null) return;

        underlayPt.enabled = true;
        underlayPt.textureOpacity = startOpacity;
    }

    private IEnumerator ChangeOpacity(float endOpacity)
    {
        float startOpacity = underlayPt.textureOpacity;
        bool incrementing = startOpacity < endOpacity;

        while (incrementing ? underlayPt.textureOpacity < endOpacity : underlayPt.textureOpacity > endOpacity)
        {
            underlayPt.textureOpacity = Mathf.Lerp(startOpacity, endOpacity, transitionSpeed * Time.deltaTime);

            yield return null;
        }
        underlayPt.textureOpacity = endOpacity;
    }


}
