using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    bool TimerIsRunning { get; set; }
    float TimeRemaining { get; set; }
}
