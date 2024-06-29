using Oculus.Interaction;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    public PointableUnityEventWrapper SaveButtonWrapper;
    public PointableUnityEventWrapper RemoveButtonWrapper;

    private void OnEnable()
    {
        //TODO Save Anchors
        RemoveButtonWrapper.WhenRelease.AddListener(RemoveSelf);
    }
    private void OnDisable()
    {
        RemoveButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
    }
    public void RemoveSelf(PointerEvent e)
    {
        //Remove anchors

        Destroy(this, .5f);
    }
}
