using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class SettingsUI : MonoBehaviour
{
    //Variables
    [SerializeField] private PointableUnityEventWrapper opacityPlusWrapper;
    [SerializeField] private PointableUnityEventWrapper opacityMinusWrapper;
    [SerializeField] private PointableUnityEventWrapper addFocusAreaWrapper;
    [SerializeField] private PointableUnityEventWrapper resetFocusAreasWrapper;
    [SerializeField] private PointableUnityEventWrapper saveAllFocusAreasWrapper;

    //Events
    [HideInInspector] public UnityEvent OnMoreOpacity = new();
    [HideInInspector] public UnityEvent OnLessOpacity = new();
    [HideInInspector] public UnityEvent OnAddFocusArea = new();
    [HideInInspector] public UnityEvent OnSaveAllFocusAreas = new();
    [HideInInspector] public UnityEvent OnResetAllFocusAreas = new();

    private void OnEnable()
    {
        //Optional
        if (opacityPlusWrapper != null)
            opacityPlusWrapper.WhenRelease.AddListener((PointerEvent v) => SendEvent(0));
        if (opacityMinusWrapper != null)
            opacityMinusWrapper.WhenRelease.AddListener((PointerEvent v) => SendEvent(1));


        addFocusAreaWrapper.WhenRelease.AddListener((PointerEvent v) => SendEvent(2));
        saveAllFocusAreasWrapper.WhenRelease.AddListener((PointerEvent v) => SendEvent(3));
        resetFocusAreasWrapper.WhenRelease.AddListener((PointerEvent v) => SendEvent(4));
    }

    private void SendEvent(int index)
    {
        switch (index)
        {
            case 0: OnMoreOpacity?.Invoke(); break;
            case 1: OnLessOpacity?.Invoke(); break;
            case 2: OnAddFocusArea?.Invoke(); break;
            case 3: OnSaveAllFocusAreas?.Invoke(); break;
            case 4: OnResetAllFocusAreas?.Invoke(); break;
        }
    }

    #region Testing
    [ContextMenu("Reset")]
    public void ResetFocusAreas_ButtonClick()
    {
        SendEvent(4);
    }

    [ContextMenu("Save")]
    public void SaveFocusAreas_ButtonClick()
    {
        SendEvent(3);
    }

    [ContextMenu("Add")]
    public void AddFocusArea_ButtonClick()
    {
        SendEvent(2);
    }
    #endregion
}
