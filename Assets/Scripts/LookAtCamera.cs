using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
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
            transform.LookAt(2 * transform.position - target.position);
    }
}