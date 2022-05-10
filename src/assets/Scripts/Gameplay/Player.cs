using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player SharedInstance { get { return sharedInstance; } } // singleton
    private static Player sharedInstance;

    public int Score { get { return score; } set { score = value; } }
    private int score;

    public bool IsFishOnLine { get { return isFishOnLine; } set { isFishOnLine = value; } }
    private bool isFishOnLine;

    public delegate void OnScoreUpdateHandler(int score);
    public event OnScoreUpdateHandler OnScoreUpdate;

    public delegate void OnMeterGainHandler(float maxMeterScaleY, int targetTimesReeled, int timesReeled);
    public event OnMeterGainHandler OnMeterGain;

    public delegate void OnMeterLossHandler(float lossAmount);
    public event OnMeterLossHandler OnMeterLoss;

    public delegate void OnMeterResetHandler();
    public event OnMeterResetHandler OnMeterReset;

    [SerializeField] private KeyCode fightFishKey;
    private float maxMeterScaleY;
    
    private Timer timer;
    private int timesReeled;
    private int targetTimesReeled;
    private Fish fishOnTheLine;
    private Collider2D hookCollider;
    private float meterLossAmount;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        sharedInstance.timer = gameObject.AddComponent<Timer>();
        sharedInstance.hookCollider = transform.GetChild(2).transform.GetChild(1).GetComponent<Collider2D>();

        sharedInstance.targetTimesReeled = 5;
        sharedInstance.meterLossAmount = .1f;
        sharedInstance.maxMeterScaleY = 46;
    }

    void Update()
    {
        if (sharedInstance.timer.TimerIsRunning)
        {
            FightFish();
        }        
    }

    private void FightFish()
    {
        if (sharedInstance.timesReeled < sharedInstance.targetTimesReeled)
        {
            if (sharedInstance.timer.TimeRemaining == 0)
            {
                if (sharedInstance.timesReeled < sharedInstance.targetTimesReeled)
                    LoseFish();
            }

            if (Input.GetKeyDown(sharedInstance.fightFishKey))
            {
                OnMeterGain(sharedInstance.maxMeterScaleY, sharedInstance.targetTimesReeled, sharedInstance.timesReeled);
                sharedInstance.timesReeled++;
                Debug.Log("Reeled " + sharedInstance.timesReeled + " times. " + sharedInstance.timer.TimeRemaining + " time remaining.");
            }

            MoveMeter();
        }
        else
            CatchFish();
    }

    public void HookFish(MarineObject marineObject)
    {
        if(!sharedInstance.timer.TimerIsRunning)
        {
            Fish fish = marineObject as Fish;

            if (fish)
            {
                sharedInstance.timer.SetTimerDuration(5);
                sharedInstance.targetTimesReeled = fish.Durability;
                sharedInstance.timer.StartTimer();
                sharedInstance.fishOnTheLine = fish;
                sharedInstance.hookCollider.enabled = false;
                sharedInstance.isFishOnLine = true;
                sharedInstance.fishOnTheLine.gameObject.AddComponent<Rigidbody2D>();
            }
            else
            {
                Debug.Log("Obstacle");
            }

            Debug.Log("timer started! " + marineObject.name);
        }       
        else
        {
            marineObject.CanMove = true;
        }
    }

    private void CatchFish()
    {
        Debug.Log("Fish caught!: " + sharedInstance.fishOnTheLine.name);
        EndFight();
        sharedInstance.fishOnTheLine.GetCaught(); // make fish disappear
        sharedInstance.score++;
        sharedInstance.OnScoreUpdate(sharedInstance.score); // fire delegate to set UI text
    }

    public void LoseFish()
    {
        Debug.Log("Fish lost!: " + sharedInstance.fishOnTheLine.name);
        EndFight();
        sharedInstance.fishOnTheLine.CanMove = true; // resume fish movement
        sharedInstance.fishOnTheLine.CanCollideWithHook = false; // this fish gets immunity  
    }

    private void MoveMeter()
    {
        sharedInstance.OnMeterLoss(meterLossAmount);
    }

    private void EndFight()
    {
        sharedInstance.timesReeled = 0; // qte button reset
        sharedInstance.timer.TimerIsRunning = false; // deactivate timer
        sharedInstance.hookCollider.enabled = true;
        sharedInstance.isFishOnLine = false;
        sharedInstance.fishOnTheLine.IsOnHook = false; // fish movement no longer tied to hook
        Destroy(sharedInstance.fishOnTheLine.GetComponent<Rigidbody2D>());
        sharedInstance.OnMeterReset();
    }

    public void UpdateScore(int score)
    {
        OnScoreUpdate(score);
        sharedInstance.score = score; 
    }
}
