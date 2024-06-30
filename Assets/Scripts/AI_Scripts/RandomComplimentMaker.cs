using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomComplimentMaker : MonoBehaviour
{
    [SerializeField] private AI_TextToSpeech tts;

    [SerializeField] private string welcomeMessage = "Hi, I am Tommy. You will keep me.";
    [SerializeField] private string ptTutorialMessage = "Let us create focus areas";
    [SerializeField] private string goodJobMessage = "Good Job. You are the best, like no one ever was";
    [SerializeField] private string rotateTutorialMessage = "Spin my head right round";
    [SerializeField] private string rewardMessage = "Your earned a tomato. Let us make a pause.";
    [SerializeField] private string restartMessage = "Time to go work back, my slave";

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

    public void SayStuff(int messageNumber)
    {
        string message = "Ok";
        switch (messageNumber)
        {
            case 0:
                message = welcomeMessage;
                break;
            case 1:
                message = ptTutorialMessage;
                break;
            case 2:
                message = rotateTutorialMessage;
                break;
            case 3:
                message = goodJobMessage;
                break;
            case 4:
                message = rewardMessage;
                break;
            case 5:
                message = restartMessage;
                break;
            default:
                break;
        }
        tts.ConvertTextToSpeechAsync(message);
    }
}
