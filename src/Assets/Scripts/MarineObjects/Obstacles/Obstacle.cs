using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MarineObject, IObstacle
{
    protected override void Start()
    {
        base.Start();
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
        if (col.CompareTag("RightBound"))
        {
            Deactivate(); // spawn new delegate fire
        }
    }
}
