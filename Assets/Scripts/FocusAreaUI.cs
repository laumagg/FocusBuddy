using Oculus.Interaction;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    public PointableUnityEventWrapper SaveButtonWrapper;
    public PointableUnityEventWrapper RemoveButtonWrapper;

    [SerializeField] private GameObject scalersParent;
    [SerializeField] private GameObject sideButtonsParent;
    [SerializeField] private MeshRenderer mover;

    private void OnEnable()
    {
        SaveButtonWrapper.WhenRelease.AddListener(SaveSelf);
        RemoveButtonWrapper.WhenRelease.AddListener(RemoveSelf);
    }
    private void OnDisable()
    {
        SaveButtonWrapper.WhenRelease.RemoveListener(SaveSelf);
        RemoveButtonWrapper.WhenRelease.RemoveListener(RemoveSelf);
    }


    public void RemoveSelf(PointerEvent e)
    {
        //Remove anchors?
        Destroy(gameObject, .3f);
    }
    public void SaveSelf(PointerEvent e)
    {
        //Create anchors?
        scalersParent.SetActive(false);
        mover.enabled = false;
    }

    #region Testing
    [ContextMenu("RemoveSelf_CM")]
    public void RemoveSelf_CM() { RemoveSelf(new()); }
    [ContextMenu("SaveSelf_CM")]
    public void SaveSelf_CM() { SaveSelf(new()); }
    #endregion
}
