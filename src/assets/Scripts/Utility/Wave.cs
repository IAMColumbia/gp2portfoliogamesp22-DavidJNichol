using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : IWave
{
    public int NumObjectsToSpawn { get { return numObjectsToSpawn; } set { numObjectsToSpawn = value; } }
    private int numObjectsToSpawn;
    public int GoalAmount { get { return goalAmount; } set { goalAmount = value; } }
    private int goalAmount = 10;

    public void SetMarineObjectChanceOfSpawn(MarineObject obj, float probability)
    {
        obj.SpawnProbability = probability;
    }

    public void SetFishDurability(Fish obj, int durability)
    {
        obj.Durability = durability;
    }

    public void SetMarineObjectSpeed(MarineObject obj, float speed)
    {
        obj.MoveSpeed = speed;
    }
}
