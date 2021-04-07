using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    #region Singleton
    public static InstanceManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            InstanceManager[] managers = FindObjectsOfType(typeof(InstanceManager)) as InstanceManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("InstanceManager not present on the scene. Creating a new one.");
                InstanceManager manager = new GameObject("Instance Manager").AddComponent<InstanceManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one InstanceManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static InstanceManager _instance = null;
    #endregion

    public enum PoolTag {
        UNKNOWN
    }
    
    [System.Serializable]
    public class Pool {
        public PoolTag tag;
        public GameObject prefab;
        public int poolSize;
        public Transform parentObject;
    }

    public Dictionary<PoolTag,Queue<GameObject>> poolDictionary;
    public List<Pool> poolInitList;
    private Vector3 outPosition = new Vector3(0f,-5000f,0f);

    private void Awake() {
        poolDictionary = new Dictionary<PoolTag,Queue<GameObject>>();

        foreach(Pool pool in poolInitList) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject instance = Instantiate(pool.prefab,outPosition,Quaternion.identity,pool.parentObject);
                instance.SetActive(false);
                objectPool.Enqueue(instance);
            }

            poolDictionary.Add(pool.tag,objectPool);
        }
    }
    
    public GameObject Spawn(PoolTag tag,Vector3 position,Quaternion rotation) {
        GameObject spawnedObject = poolDictionary[tag].Dequeue();

        spawnedObject.SetActive(true);
        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = rotation;

        IPooledObject pooledObject = spawnedObject.GetComponent<IPooledObject>();
        if(pooledObject != null) {
            pooledObject.OnObjectSpawned();
        }

        poolDictionary[tag].Enqueue(spawnedObject);

        return spawnedObject;
    }
}
