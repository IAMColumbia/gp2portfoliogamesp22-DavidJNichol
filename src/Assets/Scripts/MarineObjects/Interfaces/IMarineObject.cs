using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarineObject
{
    float MoveSpeed { get; set; }
    float SpawnProbability { get; set; }
    bool CanMove { get; set; }
    bool CanCollideWithHook { get; set; }
    bool IsOnHook { get; set; }
    float SpawnOffsetY { get; }
    Rigidbody2D Rigidbody { get; }
    void Move();
    void Deactivate();
}
