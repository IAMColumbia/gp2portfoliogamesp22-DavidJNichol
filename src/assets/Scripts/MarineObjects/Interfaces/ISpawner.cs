using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    List<MarineObject> ObjectsInScene { get; set; }
    float SpawnOffsetY { get; set; }
}
