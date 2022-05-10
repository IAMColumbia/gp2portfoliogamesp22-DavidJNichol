using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    public bool TimerIsRunning { get { return timerIsRunning; } set { timerIsRunning = value; } }
    private bool timerIsRunning;
    public float TimeRemaining { get { return timeRemaining; } set { timeRemaining = value; } }
    private float timeRemaining = 5;

    public void SetTimerDuration(float duration)
    {
        timeRemaining = duration;
    }

    private void Update()
    {
        if(timerIsRunning)
        {
            RunTimer();
        }      
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timeRemaining = 0;
        timerIsRunning = false;
    }

    private void RunTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            StopTimer();
        }
    }
}
