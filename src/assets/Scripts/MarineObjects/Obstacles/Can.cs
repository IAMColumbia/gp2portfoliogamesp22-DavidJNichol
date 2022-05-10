using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : Obstacle
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1.3f;
        spawnProbability = .5f;
    }
}
