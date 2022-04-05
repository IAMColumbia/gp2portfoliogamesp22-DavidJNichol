using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private GameObject hook;

    [SerializeField] private Camera mainCamera;

    private float lastY;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }
    private void Update()
    {
        mouseY = mainCamera.ScreenToWorldPoint(Input.mousePosition).y*2;
    }
    private void FixedUpdate()
    {
        if (mouseY != lastY)
        {
            this.transform.localPosition = new Vector3(transform.localPosition.x, mouseY, transform.localPosition.z);
            lastY = this.transform.localPosition.y;
        }
        //gradual
        //this.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + mouseY, transform.localPosition.z);
    }
}
