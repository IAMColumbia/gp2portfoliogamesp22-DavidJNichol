using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarineObject
{
    float MoveSpeed { get; set; }

    bool CanMove { get; set; }

    float SpawnOffsetY { get; set; }

    void Move();

    void Deactivate();
}
