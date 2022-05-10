using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWave
{
    int NumObjectsToSpawn { get; set; }

    int GoalAmount { get; set; }

    void SetMarineObjectSpeed(MarineObject obj, float speed);

    void SetFishDurability(Fish obj, int durability);

    void SetMarineObjectChanceOfSpawn(MarineObject obj, float probability);
}
