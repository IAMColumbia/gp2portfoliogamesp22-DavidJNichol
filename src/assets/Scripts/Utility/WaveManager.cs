using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager SharedInstance { get { return sharedInstance; } set { sharedInstance = value; } }
    private static WaveManager sharedInstance;
    public Wave CurrentWave { get { return currentWave; } set { currentWave = value; } }
    private Wave currentWave;

    [SerializeField] private Shark sharkCopy;

    [SerializeField] private float roundTime;

    public delegate void OnWaveEndHandler(bool success);
    public event OnWaveEndHandler OnWaveEnd;

    public delegate void OnWaveStartHandler(int goalAmount);
    public event OnWaveStartHandler OnWaveStart;

    [SerializeField] private GameObject hook;

    [HideInInspector]
    public Timer timer;

    [HideInInspector] public bool isRoundActive;

    [HideInInspector]
    public List<Wave> waveList;

    public int roundNum;
    private bool roundSuccess;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this.gameObject); // persist across scenes
    }

    // Start is called before the first frame update
    void Start()
    {
        sharedInstance.waveList = new List<Wave>();
        sharedInstance.timer = GetComponent<Timer>();
        sharedInstance.roundTime = roundTime;

        isRoundActive = true;

        StartNewRound();

        Wave NewWave = new Wave();
        sharedInstance.currentWave = NewWave;
    }

    private void Update()
    {
        if (isRoundActive)
        {
            if (sharedInstance.timer.TimerIsRunning)
            {
                if (sharedInstance.timer.TimeRemaining <= 0)
                {
                    EndRound();
                    isRoundActive = false;
                }
            }
        }
    }

    public void StartNewRound()
    {
        sharedInstance.roundNum++;
        Wave NewWave = new Wave();
   
        if (roundNum == 1)
        {
            NewWave.GoalAmount = 6;
        }
        else if (roundNum == 2)
        {
            NewWave.GoalAmount = 8;
        }
        else if (roundNum == 3)
        {
            NewWave.GoalAmount = 10;
        }
        else if (roundNum == 4)
        {
            NewWave.GoalAmount = 12;
        }
        else if (roundNum == 5)
        {
            NewWave.GoalAmount = 14;
        }
        else
        {
            NewWave.GoalAmount = 6;
        }

        sharedInstance.currentWave = NewWave;
        sharedInstance.OnWaveStart(sharedInstance.currentWave.GoalAmount);
        sharedInstance.timer.SetTimerDuration(sharedInstance.roundTime);
        sharedInstance.timer.StartTimer();
        isRoundActive = true;
        hook.GetComponent<Collider2D>().enabled = true;
    }

    private void EndRound()
    {
        Debug.Log(Player.SharedInstance.Score);
        if (Player.SharedInstance.Score >= currentWave.GoalAmount)
        {
            roundSuccess = true;
        }
        else
            roundSuccess = false;

        sharedInstance.OnWaveEnd(roundSuccess);
        hook.GetComponent<Collider2D>().enabled = false;
    }
    
    public void BeginRound()
    {

    }
}
