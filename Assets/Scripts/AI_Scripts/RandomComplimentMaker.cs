using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomComplimentMaker : MonoBehaviour
{
    [SerializeField] private AI_TextToSpeech tts;

    [TextArea]
    public List<string> Compliments = new();

    private string GetRandomElement()
    {
        int index = Random.Range(0, Compliments.Count);
        return Compliments[index];
    }

    [ContextMenu("SayRandomCompliment")]
    public void SayRandomCompliment()
    {
        string compliment = GetRandomElement();
        tts.ConvertTextToSpeechAsync(compliment);
    }
}
