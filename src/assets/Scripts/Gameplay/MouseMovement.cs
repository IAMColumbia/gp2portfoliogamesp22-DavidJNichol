using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Rigidbody2D hookRB;
    private Camera mainCamera;

    private float mouseY;
    private float lastY;

    private float bottomBoundY;
    private float topBoundY;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        mainCamera = Camera.main;
        hookRB = transform.GetChild(1).GetComponent<Rigidbody2D>();

        speed = 4;
        bottomBoundY = -295;
        topBoundY = -64;
    }
    private void FixedUpdate()
    {
        mouseY = mainCamera.ScreenToWorldPoint(Input.mousePosition).y;

        if (hookRB.position.y - mouseY > 6)
        {
            MoveDown();
        }
        else if (hookRB.transform.position.y - mouseY < -6)
        {
            MoveUp();
        }

        lastY = mouseY;

        KeepInBounds();
    }

    private void MoveUp()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + speed, this.transform.localPosition.z);
    }

    private void MoveDown()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - speed, this.transform.localPosition.z);
    }

    private void KeepInBounds()
    {
        if (this.transform.localPosition.y < bottomBoundY)
            this.transform.localPosition = new Vector3(transform.localPosition.x, bottomBoundY, transform.localPosition.z);
        else if (this.transform.localPosition.y > topBoundY)
            this.transform.localPosition = new Vector3(transform.localPosition.x, topBoundY, transform.localPosition.z);
    }
}
