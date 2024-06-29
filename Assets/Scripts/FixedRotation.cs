using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion originalRotation;

    void Start()
    {
        // Ursprüngliche Rotation speichern
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Rotation auf ursprünglichen Wert zurücksetzen
        transform.rotation = originalRotation;
    }
}
