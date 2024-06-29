using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    [Header("Underlay Passthrough")]
    [SerializeField] private bool startInVR = true;
    [SerializeField] private OVRPassthroughLayer underlayPt;
    [SerializeField] private float defaultOpacity = 0.7f;
    [SerializeField] private float transitionSpeed = 0.1f;

    [Header("Overlay Passthrough")]
    [SerializeField] private OVRPassthroughLayer overlayPt;
    [SerializeField] private GameObject focusAreaPrefab;

    [Header("Others")]
    [SerializeField] private SettingsUI settingsUI;

    private List<FocusAreaUI> _focusAreas = new();

    private void Start()
    {
        overlayPt.hidden = true;
        underlayPt.hidden = startInVR;
        if (startInVR)
        {
            underlayPt.textureOpacity = 1;
            StartCoroutine(ChangeOpacity(defaultOpacity));
        }

    }
    private void OnEnable()
    {
        //TODO trigger changing opacity from Pomodoro timer
        settingsUI.OnAddFocusArea.AddListener(SpawnNewFocusArea);
        settingsUI.OnResetAllFocusAreas.AddListener(RemoveAllFocusAreas);
        settingsUI.OnSaveAllFocusAreas.AddListener(SaveFocusAreas);
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
        underlayPt.hidden = false;

        float startOpacity = underlayPt.textureOpacity;
        bool incrementing = startOpacity < endOpacity;
        float speed = 0;
        while (incrementing ? underlayPt.textureOpacity < endOpacity : underlayPt.textureOpacity > endOpacity)
        {
            speed += transitionSpeed * Time.deltaTime;
            underlayPt.textureOpacity = Mathf.Lerp(startOpacity, endOpacity, speed);
            yield return null;
        }
        underlayPt.textureOpacity = endOpacity;
    }
    #endregion

    #region Overlay PT
    private void SpawnNewFocusArea()
    {
        if (!focusAreaPrefab) return;
        overlayPt.hidden = false;

        Vector3 pos = settingsUI.transform.position;
        pos.y += 0.5f;
        GameObject newArea = Instantiate(focusAreaPrefab, pos, Quaternion.identity);


        if (newArea.TryGetComponent(out FocusAreaUI areaUI))
        {
            areaUI.RemoveButtonWrapper.WhenRelease.AddListener((PointerEvent e) => RemoveFocusArea(areaUI));
            _focusAreas.Add(areaUI);
        }
    }

    private void SaveFocusAreas()
    {
        overlayPt.hidden = false;
        foreach (FocusAreaUI areaUI in _focusAreas)
        {
            if (areaUI != null)
                areaUI.SaveSelf(new());
        }
    }

    private void RemoveFocusArea(FocusAreaUI focusArea)
    {
        _focusAreas.Remove(focusArea);
        if (_focusAreas.Count == 0)
            overlayPt.hidden = true;

        if (overlayPt.IsSurfaceGeometry(focusArea.PTSurface))
            overlayPt.RemoveSurfaceGeometry(focusArea.PTSurface);
    }
    private void RemoveAllFocusAreas()
    {
        foreach (FocusAreaUI areaUI in _focusAreas)
        {
            if (areaUI == null) continue;

            if (overlayPt.IsSurfaceGeometry(areaUI.PTSurface))
                overlayPt.RemoveSurfaceGeometry(areaUI.PTSurface);

            areaUI.RemoveSelf(new());
        }

        _focusAreas.Clear();
        overlayPt.hidden = true;
    }

    #endregion
}
