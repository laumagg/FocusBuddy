using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool onlyOnStart = false;
    [SerializeField] private bool reversed = false;
    private Transform target;
    private void Init()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
        if (target == null)
            Init();

        if (target != null)
        {
            transform.LookAt(reversed ? 2 * transform.position - target.position : target.position);

            if (onlyOnStart)
                enabled = false;
        }
    }
}