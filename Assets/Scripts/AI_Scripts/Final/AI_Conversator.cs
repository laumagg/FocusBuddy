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
    /// <summary>
    /// The temperature to use for text completion.
    /// </summary>
    public static double Temperature { get; set; } = 0.1;

    public List<ChatCompletionMessage> AllMessages = new();

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
        string model = FromChatModel(ChatModel.ChatGPT3).ModelName;

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


    public static Model FromChatModel(ChatModel model)
    {
        return model switch
        {
            ChatModel.ChatGPT3 => new Model("gpt-3.5-turbo"),
            ChatModel.ChatGPT4 => new Model("gpt-4"),
            ChatModel.ChatGPT432K => new Model("gpt-4-32k"),
            ChatModel.ChatGPT316K => new Model("gpt-3.5-turbo-16k"),
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }

}