using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FocusAreaUI : MonoBehaviour
{
    //Variables
    [SerializeField] private Toggle opacityPlusToggle;
    [SerializeField] private Toggle opacityMinusToggle;
    [SerializeField] private Toggle addFocusAreaToggle;

    //Events
    [HideInInspector] public UnityEvent OnMoreOpacity = new();
    [HideInInspector] public UnityEvent OnLessOpacity = new();
    [HideInInspector] public UnityEvent OnAddFocusArea = new();

    private void OnEnable()
    {
        opacityPlusToggle.onValueChanged.AddListener((bool v) => SendEvent(0));
        opacityMinusToggle.onValueChanged.AddListener((bool v) => SendEvent(1));
        opacityPlusToggle.onValueChanged.AddListener((bool v) => SendEvent(2));
    }

    private void SendEvent(int index)
    {
        switch (index)
        {
            case 0: OnMoreOpacity.Invoke(); break;
            case 1: OnLessOpacity.Invoke(); break;
            case 2: OnAddFocusArea.Invoke(); break;
        }
    }
}
