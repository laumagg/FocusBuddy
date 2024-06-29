using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion originalRotation;

    void Start()
    {
        // Urspr�ngliche Rotation speichern
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Rotation auf urspr�nglichen Wert zur�cksetzen
        transform.rotation = originalRotation;
    }
}
