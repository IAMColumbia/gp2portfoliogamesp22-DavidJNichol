using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MarineObject, ICatchable
{
    public int Durability { get { return durability; } set { durability = value;} }
    protected int durability;

    protected override void Start()
    {
        base.Start();
    }
}
