using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private bool onlyOnStart = false;
    [SerializeField] private bool reversed = false;
    [SerializeField] private bool onlyY = false;
    private Transform target;
    private void Init()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
        if (target == null)
            Init();

        if (target != null && !onlyY)
        {
            transform.LookAt(reversed ? 2 * transform.position - target.position : target.position);

            if (onlyOnStart)
                enabled = false;
        }

        if (onlyY)
            LookAtY();
    }

    private void LookAtY()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y; // Keep the target position at the same height as this object

            Vector3 direction = targetPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            Quaternion rotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);

            transform.rotation = rotation;
        }
    }
}