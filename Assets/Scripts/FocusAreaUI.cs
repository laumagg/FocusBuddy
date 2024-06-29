using Oculus.Interaction;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    public PointableUnityEventWrapper SaveButtonWrapper;
    public PointableUnityEventWrapper RemoveButtonWrapper;

    private void OnEnable()
    {
        SaveButtonWrapper.WhenRelease.AddListener(SaveSelf);
        RemoveButtonWrapper.WhenRelease.AddListener(RemoveSelf);
    }
    private void OnDisable()
    {
        RemoveButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
    }
    public void RemoveSelf(PointerEvent e)
    {
        //Remove anchors?

        Destroy(this, .5f);
    }
    private void SaveSelf(PointerEvent e)
    {
        uiParent.gameObject.SetActive(false);

        //Create anchors?
    }
}
