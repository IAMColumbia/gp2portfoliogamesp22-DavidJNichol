using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject UICard;

    [SerializeField] private Texture2D HoverCursor;

    [SerializeField] private int cost;

    private bool isHoveringCursor;

    public delegate void OnItemBoughtHandler(int fishLeft);
    public event OnItemBoughtHandler OnItemBought;

    private void Update()
    {
        if (isHoveringCursor)
            if (Input.GetMouseButtonDown(0))
                if (Player.SharedInstance.Score >= cost)
                {
                    OnMouseExit();
                    Player.SharedInstance.Score -= cost;
                    OnItemBought(Player.SharedInstance.Score);
                    gameObject.SetActive(false); //TODO implement purchase here
                }
                    
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(HoverCursor, Vector2.zero, CursorMode.ForceSoftware);
        UICard.SetActive(true);
        isHoveringCursor = true;
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        UICard.SetActive(false);
        isHoveringCursor = false;
    }
}