using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject meter;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Text timer;
    [SerializeField] private Text goalAmount;
    [SerializeField] private GameObject shop;
    [SerializeField] private Text shopCurrency;
    [SerializeField] private Text CaughtAmount;
    [SerializeField] private Camera shopCamera;
    [SerializeField] private GameObject lossMessage;
    public GameObject jeff;

    public bool isTimerRunning;

    private bool wonRound;

    void Start()
    {
        Player.SharedInstance.OnScoreUpdate += UpdateScore;
        Player.SharedInstance.OnMeterLoss += UpdateMeterLoss;
        Player.SharedInstance.OnMeterGain += UpdateMeterGain;
        Player.SharedInstance.OnMeterReset += ResetMeter;
        WaveManager.SharedInstance.OnWaveStart += UpdateGoalAmount;
        WaveManager.SharedInstance.OnWaveEnd += ShowEndScreen;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        var ts = TimeSpan.FromSeconds(WaveManager.SharedInstance.timer.TimeRemaining);
        timer.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateGoalAmount(int amount)
    {
        goalAmount.text = amount.ToString();
    }

    private void UpdateMeterLoss(float lossAmount)
    {
       if(meter.transform.localScale.y > 0)
       {
            meter.transform.localScale = new Vector2(meter.transform.localScale.x,
                meter.transform.localScale.y -
                  lossAmount);
       }
    }

    private void UpdateMeterGain(float maxMeterScaleY, int targetTimesReeled, int timesReeled)
    {
        float meterRemaining = maxMeterScaleY - meter.transform.localScale.y;

        meter.transform.localScale =
                     new Vector2(meter.transform.localScale.x,
                     meter.transform.localScale.y +
                     (meterRemaining / (targetTimesReeled - timesReeled)));
    }

    private void ResetMeter()
    {
        meter.transform.localScale = new Vector2(meter.transform.localScale.x, 0);
    }

    private void ShowEndScreen(bool success)
    {
        wonRound = success;
        if (!success)
            lossMessage.SetActive(true);
        else
            lossMessage.SetActive(false);

        jeff.SetActive(true);

        CaughtAmount.text = Player.SharedInstance.Score.ToString();
        endScreen.gameObject.SetActive(true);

        if(Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }    
    }

    public void ShowShop()
    {
        if(wonRound)
        {
            transform.parent.gameObject.SetActive(false); // turn off game
            endScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
            shop.SetActive(true);
            shopCamera.enabled = true;
            Camera.main.enabled = false;
            shopCurrency.text = Player.SharedInstance.Score.ToString();
        }
        else
        {
            Cursor.visible = false;
            transform.parent.gameObject.SetActive(true);
            endScreen.SetActive(false);
            Player.SharedInstance.UpdateScore(0);
            WaveManager.SharedInstance.roundNum = 0;
            WaveManager.SharedInstance.StartNewRound(); //restart
            Time.timeScale = 1;
        }
    }
}
