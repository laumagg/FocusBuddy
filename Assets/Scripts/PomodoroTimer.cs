using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    public UnityEvent onStartTimer;
    public UnityEvent onEndTimer;
    public UnityEvent onEndBreak;
    private float pomodoroTimer = 60;//1500 -- 25min
    private float breakTimer = 10;//240  -- 4min
    public GameObject pomodoroObject;
   
    private IEnumerator coroutine;
    private float elapsedSeconds;
    private float angle;
    private float minutes;
    private float seconds;
    public TMP_Text mText;
    private bool isBreak = false;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer(){
        onStartTimer.Invoke();
        isBreak = false;

        coroutine = timer(pomodoroTimer);
        StartCoroutine(coroutine);
    }

    public void StartBreak(){
        onEndTimer.Invoke();
        if(!isBreak){
          isBreak = true;
          coroutine = timer(breakTimer);
          StartCoroutine(coroutine);
        }
        else{
          onEndBreak.Invoke();
          return;
        }
        
    }
    private IEnumerator timer(float time){
      Quaternion startingRotation = pomodoroObject.transform.rotation;
      Quaternion targetRotation = startingRotation;
      angle = 0.0f; //restart to 0
      float angleIncrement = 360.0f / time;// full rotation in n minute
      elapsedSeconds = 0;
      while (elapsedSeconds < time)
      {
        DisplayTime(elapsedSeconds);
        //waiting 1 second in real time and increasing the timer value
        yield return new WaitForSecondsRealtime(1);
        
        angle += angleIncrement;
        pomodoroObject.transform.rotation = targetRotation * Quaternion.Euler(0,0,angle);
        Debug.Log($"Time elapsed: {elapsedSeconds}s, Rotation: {pomodoroObject.transform.rotation.eulerAngles}");
        elapsedSeconds++;
      }
      pomodoroObject.transform.rotation = targetRotation; //fix position if not exact
      StartBreak();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        mText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
