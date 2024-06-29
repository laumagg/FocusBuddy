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
        Destroy(gameObject, .5f);
    }
    public void SaveSelf(PointerEvent e)
    {
        //Create anchors?
        uiParent.gameObject.SetActive(false);
    }

    #region Testing
    [ContextMenu("RemoveSelf_CM")]
    public void RemoveSelf_CM() { RemoveSelf(new()); }
    [ContextMenu("SaveSelf_CM")]
    public void SaveSelf_CM() { SaveSelf(new()); }
    #endregion
}
