using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get { return sharedInstance; } }

    private static ObjectPool sharedInstance;

    public List<MarineObject> objectsToPool;

    public List<MarineObject> pooledObjects;

    [HideInInspector] public int minimumAmountOfObjects;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this.gameObject); // persist across scenes
    }

    void Start()
    {
        sharedInstance.pooledObjects = new List<MarineObject>();

        InstantiateMarineObjectsForScene();

        sharedInstance.minimumAmountOfObjects = sharedInstance.pooledObjects.Count;
    }

    public MarineObject GetPooledObject()
    {

        for (int i = 0; i < sharedInstance.minimumAmountOfObjects; i++)
        {
            int randNum = Random.Range(0, sharedInstance.pooledObjects.Count);
            if (!sharedInstance.pooledObjects[randNum].gameObject.activeInHierarchy)
            {
                if (Random.value <= sharedInstance.pooledObjects[randNum].SpawnProbability)
                    return sharedInstance.pooledObjects[randNum];
            }
        }
        return null;
    }

    private void InstantiateMarineObjectsForScene()
    {
        MarineObject tmp;
        for (int i = 0; i < sharedInstance.minimumAmountOfObjects; i++)
        {
            tmp = Instantiate(sharedInstance.objectsToPool[i]);
            tmp.transform.parent = gameObject.transform;
            tmp.gameObject.SetActive(false);
            sharedInstance.pooledObjects.Add(tmp);
        }
    }
}
