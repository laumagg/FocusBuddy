using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FlowMaster : MonoBehaviour
{
    [SerializeField] private List<GameObject> Libs = new();
    [SerializeField] private GameObject awaker;
    [SerializeField] private List<GameObject> InteractionUIs = new();

    [SerializeField] private GameObject clock;

    [SerializeField] private Collider RotationCollider;

    private void Start()
    {     
        ToggleFaceParts(false);
        ToggleIteractionUIs(false);
        ToggleClock(false);
        ToggleRotation(false);
    }

    public void ToggleFaceParts(bool activate)
    {
        foreach (GameObject item in Libs)
        {
            item.SetActive(activate);
        }
    }

    public void RemoveAwaker()
    {
        Destroy(awaker);
    }

    public void ToggleIteractionUIs(bool activate)
    {
        foreach (GameObject item in InteractionUIs)
        {
            item.SetActive(activate);
        }
    }

    public void ToggleClock(bool activate)
    {
        clock.SetActive(activate);
    }

    public void ToggleRotation(bool enable)
    {
        RotationCollider.enabled = enable;
    }

    public async void DelayBeforePT(int seconds)
    {
        await Task.Delay(seconds * 1000);
        // Timer starts break status
        Timer timer = FindAnyObjectByType<Timer>();
        timer.StartBreak();
    }
}
