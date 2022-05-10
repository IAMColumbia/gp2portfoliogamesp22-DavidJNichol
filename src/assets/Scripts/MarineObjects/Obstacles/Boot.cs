using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Obstacle
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = .8f;
        spawnProbability = .5f;
    }
}
