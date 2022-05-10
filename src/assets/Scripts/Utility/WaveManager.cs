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

    private bool isRoundActive;

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

            //for(int i = 0; i < ObjectPool.SharedInstance.pooledObjects.Count; i++) //wave one all objs have 50% chance
            //{
            //    ObjectPool.SharedInstance.pooledObjects[i].SpawnProbability = .5f;
            //}
        }
        else if (roundNum == 2)
        {
            NewWave.GoalAmount = 10;
            //NewWave.GoalAmount = 12;

            //Shark extraShark = Instantiate(sharkCopy, sharkCopy.transform.parent.transform);
            //extraShark.gameObject.SetActive(false);

            //ObjectPool.SharedInstance.objectsToPool.Add(extraShark);
            //ObjectPool.SharedInstance.pooledObjects.Add(extraShark);
            //ObjectPool.SharedInstance.minimumAmountOfObjects = ObjectPool.SharedInstance.objectsToPool.Count;

            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[0], .5f); //boot 
            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[1], .5f); //eel
            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[2], .5f); //nemo
            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[3], .5f); //can
            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[4], .5f); //shark
            //NewWave.SetMarineObjectChanceOfSpawn(ObjectPool.SharedInstance.objectsToPool[5], .5f); //shark

        }
        else if (roundNum == 3)
        {
            sharedInstance.currentWave.NumObjectsToSpawn = 4;
        }
        else if (roundNum == 4)
        {
            sharedInstance.currentWave.NumObjectsToSpawn = 4;
        }
        else if (roundNum == 5)
        {
            sharedInstance.currentWave.NumObjectsToSpawn = 5;
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
        Time.timeScale = 0; // stop time
        hook.GetComponent<Collider2D>().enabled = false;
    }
    
    public void BeginRound()
    {

    }
}
