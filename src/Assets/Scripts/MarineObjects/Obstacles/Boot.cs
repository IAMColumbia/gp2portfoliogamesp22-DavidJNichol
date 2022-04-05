using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Obstacle
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = .8f;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<MarineObject>()) // if it's a marine object
        {
            if (Player.SharedInstance)
            {
                Player.SharedInstance.LoseFish(); 
            }
        }
        if (col.name == "RightBound")
        {
            Deactivate(); // spawn new delegate fire
        }
    }
}
