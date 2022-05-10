using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MarineObject, ICatchable
{ 
    public GameObject particleRef;

    public int Durability { get { return durability; } set { durability = value;} }
    protected int durability;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void GetCaught()
    {
        Instantiate(particleRef, this.transform.position, this.transform.rotation, transform.parent).SetActive(true);
        Deactivate();
    }
}
