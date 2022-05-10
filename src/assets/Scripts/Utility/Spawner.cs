using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public List<MarineObject> ObjectsInScene { get { return objectsInScene; } set { objectsInScene = value; } }
    private List<MarineObject> objectsInScene;
    public float SpawnOffsetY { get; set; }
    private float offsetY;

    private List<float> spawnYCoordinates;

    void Start()
    {
        spawnYCoordinates = new List<float>();
        objectsInScene = new List<MarineObject>();

        WaveManager.SharedInstance.OnWaveStart += SpawnMultiple;

        InitializeSpawnOffsetList();

        SpawnMultiple(0);
    }

    //void OnEnable()
    //{
    //    InitializeSpawnOffsetList();

    //    for (int i = 0; i < ObjectPool.SharedInstance.objectsToPool.Count; i++)
    //        SpawnRandomMarineObject();
    //}

    private void RespawnObjectsIfDeactivated()
    {
        for (int i = 0; i < objectsInScene.Count; i++)
        {
            if (!objectsInScene[i].gameObject.activeInHierarchy)
            {
                objectsInScene[i].OnDeactivate -= RespawnObjectsIfDeactivated; // unbind delegate
                objectsInScene.RemoveAt(i); // object has been deactivated, remove from current pool
                SpawnRandomMarineObject();
            }
        }
    }

    private void SpawnRandomMarineObject()
    {
        // create temp marine obj and assign to OP singleton get pooled object
        MarineObject marineObject = ObjectPool.SharedInstance.GetPooledObject();

        if (marineObject != null)
        {
            objectsInScene.Add(marineObject);

            marineObject.OnDeactivate += RespawnObjectsIfDeactivated; // bind respawn delegate
            marineObject.OnHookCollision += Player.SharedInstance.HookFish; // bind fighting delegate

            marineObject.ResetValuesOnSpawn();

            if (spawnYCoordinates.Count == 0)
            {
                InitializeSpawnOffsetList();
            }

            int randIndex = Random.Range(0, spawnYCoordinates.Count);

            AdjustSpawnOffset(spawnYCoordinates[randIndex]);

            //Debug.LogError("Random Index: " + randIndex + "spawn offset: " + (spawnYCoordinates[randIndex]) + "total offset: " + (this.transform.position.y + offsetY) + " for " + marineObject.name);

            // Set spawn position for object
            marineObject.transform.position =
                new Vector3(this.transform.position.x,
                this.transform.position.y + offsetY, this.transform.position.z);

            spawnYCoordinates.RemoveAt(randIndex);
        }
    }

    private void AdjustSpawnOffset(float amount)
    {
        offsetY += amount;

        if (offsetY < -110 || offsetY > 112)
            offsetY = 0;
    }

    private void SpawnMultiple(int goalAmount)
    {
        for (int i = 0; i < ObjectPool.SharedInstance.objectsToPool.Count; i++)
            SpawnRandomMarineObject();
    }

    private void InitializeSpawnOffsetList()
    {
        spawnYCoordinates.Add(112);
        spawnYCoordinates.Add(90);
        spawnYCoordinates.Add(60);
        spawnYCoordinates.Add(30);
        spawnYCoordinates.Add(0);
        spawnYCoordinates.Add(-30);
        spawnYCoordinates.Add(-60);
        spawnYCoordinates.Add(-90);
        spawnYCoordinates.Add(-110);
    }
}