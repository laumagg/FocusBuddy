using Oculus.Interaction;
using UnityEngine;

public class FocusAreaEraser : MonoBehaviour
{
    public PointableUnityEventWrapper ButtonWrapper;

    private void OnEnable()
    {
        ButtonWrapper.WhenRelease.AddListener(RemoveSelf);
    }
    private void OnDisable()
    {
        ButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
    }
    private void RemoveSelf(PointerEvent e)
    {
        Destroy(this, .5f);
    }
}
