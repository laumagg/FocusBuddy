using Oculus.Interaction;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    public PointableUnityEventWrapper SaveButtonWrapper;
    public PointableUnityEventWrapper RemoveButtonWrapper;

    private void OnEnable()
    {
        if (SaveButtonWrapper != null)
                SaveButtonWrapper.WhenRelease.AddListener(RemoveSelf);
        RemoveButtonWrapper.WhenRelease.AddListener(RemoveSelf);
    }
    private void OnDisable()
    {
        if (SaveButtonWrapper != null)
            SaveButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
        RemoveButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
    }
    private void RemoveSelf(PointerEvent e)
    {
        //Remove from anchors

        Destroy(this, .5f);
    }
}
