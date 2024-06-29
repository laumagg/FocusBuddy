using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class AI_ProcessInput : MonoBehaviour
{
    [SerializeField] private AI_Conversator conversator;

    public List<string> CurrentList = new();

    string text = "No worries, let's break it down:\n\n" +
                      "1. Choose your topic.\n" +
                      "2. Research your topic.\n" +
                      "3. Create an outline.\n" +
                      "4. Write your thesis statement.\n" +
                      "5. Write the body.\n" +
                      "6. Write the conclusion.\n" +
                      "7. Revise and edit.\n\n" +
                      "Just take it one bite at a time!";

    private void Start()
    {
        conversator.AnswerWasGiven.AddListener(ProcessAnswer);
    }

    private void ProcessAnswer(string answer)
    {
        CurrentList = ExtractNumberedEntries(answer);

        // Ausgabe der Liste
        foreach (string entry in CurrentList)
        {
            Debug.Log(entry);
        }
    }

    private List<string> ExtractNumberedEntries(string text)
    {
        List<string> entries = new List<string>();
        Regex regex = new Regex(@"\d+\.\s+[^.]+\.");

        foreach (Match match in regex.Matches(text))
        {
            entries.Add(match.Value.Trim());
        }

        return entries;
    }
}
