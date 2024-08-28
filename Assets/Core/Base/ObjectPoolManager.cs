using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();
    
    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;
    
    public enum PoolType
    {
        ParticleSystem,
        Gameobject,
        None
    }

    public static PoolType PoolingType;

    protected override void Awake()
    {
        base.Awake();
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(transform);
        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(transform);
    }

    public T SpawnObject<T>(GameObject objectToSpawn,Vector3 spawnPosition,Quaternion spawnRotation,PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() {LookupString = objectToSpawn.name};
            ObjectPools.Add(pool);
        }

        GameObject spawnedObject = pool.InactiveObjects.FirstOrDefault();
        if (spawnedObject == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if (parentObject != null)
            {
                spawnedObject.transform.SetParent(parentObject.transform);
            }
        }
        else
        {   
            spawnedObject.transform.position = spawnPosition;
            spawnedObject.transform.rotation = spawnRotation;
            spawnedObject.SetActive(true);
            pool.InactiveObjects.Remove(spawnedObject);
            
        }

        return spawnedObject.GetComponent<T>();
    }
    public GameObject SpawnObject(GameObject objectToSpawn,Vector3 spawnPosition,Quaternion spawnRotation,PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() {LookupString = objectToSpawn.name};
            ObjectPools.Add(pool);
        }

        GameObject spawnedObject = pool.InactiveObjects.FirstOrDefault();
        if (spawnedObject == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if (parentObject != null)
            {
                spawnedObject.transform.SetParent(parentObject.transform);
            }
        }
        else
        {   
            spawnedObject.transform.position = spawnPosition;
            spawnedObject.transform.rotation = spawnRotation;
            spawnedObject.SetActive(true);
            pool.InactiveObjects.Remove(spawnedObject);
            
        }

        return spawnedObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0,obj.name.Length - 7);
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == goName);
        if (pool == null)
        {
            Debug.LogError("Trying to release an object that is not pooled" + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType){
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;
            case PoolType.Gameobject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;}
   
    }
    


}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
 
 
 