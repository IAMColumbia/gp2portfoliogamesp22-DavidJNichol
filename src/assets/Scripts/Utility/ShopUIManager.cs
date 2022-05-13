using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private GameObject game;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Camera shopCamera;
    [SerializeField] private GameObject items;
    [SerializeField] private Text currency;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private AudioSource shopMusic;

    private List<ShopItem> Items;

    private void Start()
    {
        Items = new List<ShopItem>();
        
        for(int i = 0; i < items.transform.childCount; i++)
        {
            Items.Add(items.transform.GetChild(i).gameObject.GetComponent<ShopItem>());
            Items[i].OnItemBought += UpdateCurrency;
        }
    }
    void OnEnable()
    {
        shopMusic.Play();
    }
    public void ShowGame()
    {
        transform.parent.gameObject.SetActive(false); // deactivate shop
        game.SetActive(true);
        endScreen.SetActive(false);
        gameCamera.enabled = true;
        shopCamera.enabled = false;

        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        WaveManager.SharedInstance.timer.SetTimerDuration(30);
        WaveManager.SharedInstance.timer.StartTimer();

        WaveManager.SharedInstance.StartNewRound();

    }

    private void UpdateCurrency(int fishLeft)
    {
        currency.text = fishLeft.ToString();
        Player.SharedInstance.UpdateScore(fishLeft);
    }
}
