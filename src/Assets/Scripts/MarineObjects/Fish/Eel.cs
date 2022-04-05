using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : Fish
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = .85f;
        durability = 6;
    }
}
