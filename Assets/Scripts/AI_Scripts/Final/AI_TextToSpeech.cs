using System;
using UnityEngine;
using UnityEngine.Networking;


public class AI_TextToSpeech : AI_Base
{
    public AudioSource AudioSource;

    [Tooltip("Supported voices are alloy, echo, fable, onyx, nova, and shimmer")]
    public SupportedVoice DesiredVoice = SupportedVoice.alloy;

    [TextArea]
    public string TestInput;


    [ContextMenu("RequestPromptAnswerAsync")]
    public void TestAsync()
    {
        ConvertTextToSpeechAsync(TestInput);
    }

    // Starte die Konvertierung von Text zu Sprache
    public async void ConvertTextToSpeechAsync(string text)
    {
        AudioSource.Stop();

        TTSRequest requestData = new TTSRequest
        {
            model = "tts-1",
            voice = DesiredVoice.ToString(),
            response_format = "wav",
            input = text
        };

        UnityWebRequest webRequest = CreateRequest(requestData, urlTextToSpeech);
        DownloadHandler dh = await SendWebRequestAsync(webRequest);
        if (dh == null) return;
        byte[] audioData = dh.data;

        try
        {
            AudioClip audioClip = WavUtility.ToAudioClip(audioData);
            if (audioClip != null)
            {
                AudioSource.clip = audioClip;
                AudioSource.Play();
            }
            else
            {
                Debug.LogError("AudioClip konnte nicht erstellt werden.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Fehler bei der Konvertierung der Audiodaten: " + ex.Message);
        }

    }

}