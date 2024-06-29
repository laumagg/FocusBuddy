using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static AI_Model;

public class AI_Conversator : AI_Base
{
    public List<ChatCompletionMessage> AllMessages = new();

    public StringEvent AnswerWasGiven;

    [ContextMenu("TestRequest")]
    public void TestRequest()
    {
        AllMessages.Add(new ChatCompletionMessage
        {
            role = "user",
            content = "What is your favourite color?"
        });

        CreateRequest(AllMessages.ToArray());
    }

    public async Task<string> RequestPromptAnswerAsync(string prompt)
    {
        AllMessages.Add(new ChatCompletionMessage
        {
            role = "user",
            content = prompt
        });

        string result = await CreateRequest(AllMessages.ToArray());

        if (AnswerWasGiven != null && result != "")
            AnswerWasGiven.Invoke(result);

        Debug.Log(result);
        AllMessages.Add(new ChatCompletionMessage
        {
            role = "assistant",
            content = result
        });

        return result;
    }

    public async Task<string> CreateRequest(ChatCompletionMessage[] messages)
    {
        string model = Model.FromChatModel(ChatModel.ChatGPT4).ModelName;

        ChatCompletionRequest requestData = new ChatCompletionRequest
        {
            model = model,
            messages = messages,
            temperature = Temperature,
            stream = false // Check this todo
        };

        UnityWebRequest webRequest = CreateRequest(requestData, urlChatCompletion);
        DownloadHandler dh = await SendWebRequestAsync(webRequest);


        if (dh != null)
        {
            string responseJson = dh.text;
  
            try
            {
                ChatCompletionResponse response = JsonConvert.DeserializeObject<ChatCompletionResponse>(responseJson);
                if (response != null && response.choices != null && response.choices.Length > 0)
                {
                    string responseContent = response.Text;

                    return responseContent;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing response: " + ex.Message);
            }
        }

        return null;
    }

}
