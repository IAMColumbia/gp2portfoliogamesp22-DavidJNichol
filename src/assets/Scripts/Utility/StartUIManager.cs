using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    [SerializeField] private GameObject game;
    
    public void ShowGame()
    {
        game.SetActive(true);
        gameObject.SetActive(false);
    }
}
