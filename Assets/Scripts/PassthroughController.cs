using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    [Header("Underlay Passthrough")]
    [SerializeField] private OVRPassthroughLayer underlayPt;
    [SerializeField] private float startOpacity = 0.5f;
    [SerializeField] private float transitionSpeed = 0.1f;

    [Header("Overlay Passthrough")]
    [SerializeField] private OVRPassthroughLayer overlayPt;
    [SerializeField] private GameObject focusAreaPrefab;

    [Header("Others")]
    [SerializeField] private FocusAreaUI focusAreaUI;

    private List<GameObject> _focusAreas = new();

    private void OnEnable()
    {
        focusAreaUI.OnMoreOpacity.AddListener(() => OnChangeOpacity_Toggle(true));
        focusAreaUI.OnLessOpacity.AddListener(() => OnChangeOpacity_Toggle(false));
        focusAreaUI.OnAddFocusArea.AddListener(SpawnNewFocusArea);
        //TODO trigger changing opacity from Pomodoro timer
    }
    private void Start()
    {
        if (underlayPt == null) return;

        underlayPt.enabled = true;
        underlayPt.textureOpacity = startOpacity;
    }
    #region Underlay PT
    private void OnChangeOpacity_Toggle(bool increase)
    {
        if (underlayPt == null) return;

        underlayPt.textureOpacity = increase ?
            underlayPt.textureOpacity + transitionSpeed :
            underlayPt.textureOpacity - transitionSpeed;
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
    #endregion

    #region Overlay PT
    private void SpawnNewFocusArea()
    {
        if (!focusAreaPrefab) return;

        Vector3 pos = focusAreaUI.transform.position;
        pos.x += 1;
        GameObject newArea = Instantiate(focusAreaPrefab, pos, Quaternion.identity);
        _focusAreas.Add(newArea);
    }

    #endregion
}
