using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float distance = 0.015f;
    
    [ContextMenu("Push")]
    public void Push()
    {
        StartCoroutine(Translate(transform.localPosition.z + distance));
    }
    [ContextMenu("Pull")]
    public void Pull()
    {
        StartCoroutine(Translate(transform.localPosition.z + (distance * -1)));
    }

    private IEnumerator Translate(float endPos)
    {
        float startPos = transform.localPosition.z;
        float valueToLerp, timeElapsed = 0;
        Vector3 pos = Vector3.zero;

        while (timeElapsed < duration)
        {
            valueToLerp = Mathf.Lerp(startPos, endPos, timeElapsed / duration);
            pos.z = valueToLerp;
            transform.localPosition = pos;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
