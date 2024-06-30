using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class SettingsUI : MonoBehaviour
{
    //Variables
    [SerializeField] private PokeInteractable addFocusAreaInteractable;
    [SerializeField] private PokeInteractable resetFocusAreasInteractable;
    [SerializeField] private PokeInteractable saveAllFocusAreasInteractable;

    //Events
    [HideInInspector] public UnityEvent OnAddFocusArea = new();
    [HideInInspector] public UnityEvent OnSaveAllFocusAreas = new();
    [HideInInspector] public UnityEvent OnResetAllFocusAreas = new();

    private void OnEnable()
    {
        addFocusAreaInteractable.WhenStateChanged += OnAddFocusAreaPoked;
        resetFocusAreasInteractable.WhenStateChanged += OnResetFocusAreasPoked;
        saveAllFocusAreasInteractable.WhenStateChanged += OnSaveFocusAreasPoked;
    }
    private void OnAddFocusAreaPoked(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
            OnAddFocusArea?.Invoke();
    }
    private void OnResetFocusAreasPoked(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
            OnResetAllFocusAreas?.Invoke();
    }
    private void OnSaveFocusAreasPoked(InteractableStateChangeArgs args)
    {
        if (args.NewState != InteractableState.Select) return;

        OnSaveAllFocusAreas?.Invoke();
        gameObject.SetActive(false);
    }
}
