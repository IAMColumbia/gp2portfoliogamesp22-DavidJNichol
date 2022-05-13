using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePebble : Upgrade
{
    public void ApplyEffect()
    {
        Player.SharedInstance.GetComponent<MouseMovement>().speed += 2;
    }
}
