using Oculus.Interaction;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    public GameObject PTSurface;
    public PokeInteractable SaveButtonWrapper;
    public PokeInteractable RemoveButtonWrapper;

    [SerializeField] private GameObject sideButtonsParent;
    [SerializeField] private MeshRenderer mover;

    private void OnEnable()
    {
        SaveButtonWrapper.WhenStateChanged += SaveSelf;
        RemoveButtonWrapper.WhenStateChanged += RemoveSelf;
    }
    private void OnDisable()
    {
        SaveButtonWrapper.WhenStateChanged -= SaveSelf;
        RemoveButtonWrapper.WhenStateChanged -= RemoveSelf;
    }


    public void RemoveSelf(InteractableStateChangeArgs args)
    {
        //Remove anchors?
        if (args.NewState == InteractableState.Select)
            Destroy(gameObject, .3f);
    }
    public void SaveSelf(InteractableStateChangeArgs args)
    {
        //Create anchors?
        if (args.NewState != InteractableState.Select) return;

        sideButtonsParent.SetActive(false);
        mover.enabled = false;
    }

    #region Testing
    [ContextMenu("RemoveSelf_CM")]
    public void RemoveSelf_CM() { RemoveSelf(new()); }
    [ContextMenu("SaveSelf_CM")]
    public void SaveSelf_CM() { SaveSelf(new()); }
    #endregion
}
