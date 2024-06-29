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
        overlayPt.enabled = false;
        underlayPt.enabled = startInVR;
        if (startInVR)
        {
            underlayPt.textureOpacity = 1;
            StartCoroutine(ChangeOpacity(defaultOpacity));
        }

    }
    private void OnEnable()
    {
        //TODO trigger changing opacity from Pomodoro timer
        settingsUI.OnMoreOpacity.AddListener(() => OnChangeOpacity_Toggle(true));
        settingsUI.OnLessOpacity.AddListener(() => OnChangeOpacity_Toggle(false));
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
        overlayPt.enabled = true;

        Vector3 pos = settingsUI.transform.position;
        pos.x += 1;
        GameObject newArea = Instantiate(focusAreaPrefab, pos, Quaternion.identity);

        if (newArea.TryGetComponent(out FocusAreaUI areaUI))
        {
            areaUI.RemoveButtonWrapper.WhenRelease.AddListener((PointerEvent e) => RemoveFocusArea(areaUI));
            _focusAreas.Add(areaUI);
        }

    }

    private void SaveFocusAreas()
    {
        //Anchoring
        foreach (FocusAreaUI areaUI in _focusAreas)
        {
            if (areaUI != null)
                areaUI.SaveSelf(new());
        }
    }

    private void RemoveFocusArea(FocusAreaUI focusArea)
    {
        _focusAreas.Remove(focusArea);
        if (_focusAreas.Count == 0) overlayPt.enabled = false;
    }
    private void RemoveAllFocusAreas()
    {
        foreach (FocusAreaUI areaUI in _focusAreas)
        {
            if (areaUI != null)
                areaUI.RemoveSelf(new());
        }
        _focusAreas.Clear();
        overlayPt.enabled = false;
    }

    #endregion
}
