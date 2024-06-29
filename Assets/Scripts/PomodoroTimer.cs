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
    public UnityEvent onBreakStart;
    public UnityEvent onPauseStart;
    public UnityEvent onPauseEnd;
    private float currentTimerInSeconds;
    public float pomodoroTimer = 10;//1500 -- 25min
    public float breakTimer = 240;//240  -- 4min
    private float remainingTimer;
    public GameObject pomodoroObject;
   
    private IEnumerator coroutine;
    private float elapsedSeconds;
    private float angle;
    private float minutes;
    private float seconds;
    public TMP_Text mText;
    private bool isBreak = false;
     public bool isPaused = false;
     public bool isResume = false;
     private AudioSource audio;
     private float displayTime;
    
    private void Start()
    {
      audio = GetComponent<AudioSource>();
      initTimer();
      //StartTimer();//TODO: remove, starts with interaction
    }

    private void Update(){
       if (Input.GetKeyDown("space"))
        {
          if(!isPaused){
            StartTimer();
          }
          else{
            ResumeTimer();
          }
           
        }
        if(Input.GetKeyDown("return"))
        {
           PauseTimer();
        }
    }
    private void initTimer(){
      elapsedSeconds = 0;
    }

    public void StartTimer(){
        onStartTimer.Invoke();
        currentTimerInSeconds=pomodoroTimer;
        isBreak = false;

        coroutine = timer(pomodoroTimer);
        StartCoroutine(coroutine);
    }

     public void ResumeTimer()
    {
        if (isPaused)
        {
            coroutine = timer(pomodoroTimer - elapsedSeconds);
            isPaused = false;
            onPauseEnd.Invoke();
            StartCoroutine(coroutine);
            
        }
    }

    public void StartBreak(){
        onEndTimer.Invoke();
        if(!isBreak){
          currentTimerInSeconds=breakTimer;
          isBreak = true;
          coroutine = timer(breakTimer);
          StartCoroutine(coroutine);
        }
        else{
          onEndBreak.Invoke();
          return;
        }
    }

      public void PauseTimer()
    {
        if (coroutine != null)
        {
          onPauseStart.Invoke();

            StopCoroutine(coroutine);
            isPaused = true;
        }
    }

    private IEnumerator timer(float time){
      Debug.Log("Start");
      audio.Play();
      while (elapsedSeconds < time)
      {
        if (isPaused)
            {
                yield break; // Exit the coroutine if paused
            }
        DisplayTime(elapsedSeconds);
        //waiting 1 second in real time and increasing the timer value
        yield return new WaitForSecondsRealtime(1);
        //Debug.Log("elapsed time:" + elapsedSeconds);
        elapsedSeconds++;
      }
      StartBreak();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        
        float minutes = Mathf.FloorToInt(currentTimerInSeconds - (timeToDisplay / 60)); 
        
        float seconds = Mathf.FloorToInt((60 - (timeToDisplay % 60)) % 60);

        mText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
