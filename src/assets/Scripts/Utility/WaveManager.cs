using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager SharedInstance { get { return sharedInstance; } set { sharedInstance = value; } }
    private static WaveManager sharedInstance;
    public Wave CurrentWave { get { return currentWave; } set { currentWave = value; } }
    private Wave currentWave;

    [SerializeField] private float roundTime;

    public delegate void OnWaveEndHandler();
    public event OnWaveEndHandler OnWaveEnd;

    public delegate void OnWaveStartHandler();
    public event OnWaveStartHandler OnWaveStart;

    [HideInInspector]
    public Timer timer;

    [HideInInspector]
    public List<Wave> waveList;

    private int round;

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
    }

    private void Update()
    {
        if(sharedInstance.timer.TimeRemaining <= 0)
        {
            EndRound();
        }
    }

    public void StartNewWave()
    {
        sharedInstance.round++;
        Wave newWave = new Wave();
        
        if(round == 1)
        {
            newWave.NumObjectsToSpawn = 3;
            newWave.GoalAmount = 10;
        }
        else if (round == 2)
        {
            newWave.NumObjectsToSpawn = 4;
        }
        else if (round == 3)
        {
            newWave.NumObjectsToSpawn = 4;
        }
        else if (round == 4)
        {
            newWave.NumObjectsToSpawn = 4;
        }
        else if (round == 5)
        {
            newWave.NumObjectsToSpawn = 5;
        }

        sharedInstance.currentWave = newWave;
        sharedInstance.timer.SetTimerDuration(roundTime);
        sharedInstance.timer.StartTimer();
    }

    private void EndRound()
    {
        sharedInstance.OnWaveEnd();
    }
}
