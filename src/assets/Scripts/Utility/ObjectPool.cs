using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance { get { return sharedInstance; } }

    private static ObjectPool sharedInstance;

    public List<MarineObject> objectsToPool;

    private List<MarineObject> pooledObjects;

    [SerializeField] private int minimumAmountOfObjects;

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

        if (sharedInstance.minimumAmountOfObjects == 0)
            sharedInstance.minimumAmountOfObjects = 1;
    }

    public MarineObject GetPooledObject()
    {
        for (int i = 0; i < sharedInstance.minimumAmountOfObjects; i++)
        {
            if (!sharedInstance.pooledObjects[i].gameObject.activeInHierarchy)
            {
                return sharedInstance.pooledObjects[i];
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