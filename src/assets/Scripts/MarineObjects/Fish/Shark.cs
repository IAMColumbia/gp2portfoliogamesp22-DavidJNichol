using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Fish
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1;
        durability = 10;
        spawnProbability = .2f;
    }
}
