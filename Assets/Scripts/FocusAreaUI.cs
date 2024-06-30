using Oculus.Interaction;
using System;
using UnityEngine;

public class FocusAreaUI : MonoBehaviour
{
    public GameObject PTSurface;
    public PokeInteractable SaveButtonInteractable;
    public PokeInteractable RemoveButtonInteractable;

    [SerializeField] private GameObject sideButtonsParent;
    [SerializeField] private MeshRenderer mover;

    private void OnEnable()
    {
        SaveButtonInteractable.WhenStateChanged += SaveSelf;
        RemoveButtonInteractable.WhenStateChanged += RemoveSelf;
    }
    private void OnDisable()
    {
        SaveButtonInteractable.WhenStateChanged -= SaveSelf;
        RemoveButtonInteractable.WhenStateChanged -= RemoveSelf;
    }
    public void RemoveSelf()
    {
        Destroy(gameObject, .3f);
    }

    private void RemoveSelf(InteractableStateChangeArgs args)
    {
        //Remove anchors?
        if (args.NewState != InteractableState.Select) return;

        RemoveSelf();
    }
    public void SaveSelf()
    {
        sideButtonsParent.SetActive(false);
        mover.enabled = false;
    }
    private void SaveSelf(InteractableStateChangeArgs args)
    {
        //Create anchors?
        if (args.NewState != InteractableState.Select) return;

        SaveSelf();

    }

    #region Testing
    [ContextMenu("RemoveSelf_CM")]
    public void RemoveSelf_CM() { RemoveSelf(); }


    [ContextMenu("SaveSelf_CM")]
    public void SaveSelf_CM() { SaveSelf(); }
    #endregion
}
