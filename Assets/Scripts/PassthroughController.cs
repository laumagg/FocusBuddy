using Oculus.Interaction;
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
    [SerializeField] private SettingsUI settingsUI;

    private List<GameObject> _focusAreas = new();

    private void OnEnable()
    {
        settingsUI.OnMoreOpacity.AddListener(() => OnChangeOpacity_Toggle(true));
        settingsUI.OnLessOpacity.AddListener(() => OnChangeOpacity_Toggle(false));
        settingsUI.OnAddFocusArea.AddListener(SpawnNewFocusArea);
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
        if (overlayPt.enabled == false)
            overlayPt.enabled = true;

        Vector3 pos = settingsUI.transform.position;
        pos.x += 1;
        GameObject newArea = Instantiate(focusAreaPrefab, pos, Quaternion.identity);
        if (newArea.TryGetComponent(out FocusAreaUI areaUI))
        {
            areaUI.SaveButtonWrapper.WhenRelease.AddListener((PointerEvent e) => RemoveFocusArea(newArea));
        }

        _focusAreas.Add(newArea);

    }
    private void RemoveFocusArea(GameObject focusArea)
    {
        _focusAreas.Remove(focusArea);
    }
    private void RemoveAllFocusAreas()
    {
        foreach (GameObject area in _focusAreas)
        {
           //Call remove self
        }
    }

    #endregion
}
