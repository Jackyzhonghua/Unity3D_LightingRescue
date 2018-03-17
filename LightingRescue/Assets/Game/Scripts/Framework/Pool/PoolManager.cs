using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {
    public static PoolManager m_instance;
    public static PoolManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new PoolManager();
            }
            return m_instance;
        }
    }

    public PoolManager()
    {
        ObjectPoolList objectPoolList = Resources.Load<ObjectPoolList>("pool");
        foreach(var pool in objectPoolList.PoolList)
        {
            PoolDict.Add(pool.Name, pool);
        }
    }

    public const string PoolConfigPath = "Assets/Game/Scripts/Framework/Pool/Resources/pool.asset";

    private Dictionary<string, ObjectPool> PoolDict = new Dictionary<string, ObjectPool>();

    public GameObject GetObject(string poolName)
    {
        if(!PoolDict.ContainsKey(poolName))
        {
            Debug.LogError("There is no " + poolName + " int the pool!");
            return null;
        }
        ObjectPool pool = PoolDict[poolName];
        return pool.GetObject();
    }

    public void HideObject(GameObject go)
    {
        foreach(var p in PoolDict.Values)
        {
            p.HideObject(go);
        }
    }

    public void HideAllObject(string poolName)
    {
        if(!PoolDict.ContainsKey(poolName))
        {
            Debug.LogError("There is no " + poolName + " int the pool!");
            return;
        }
        PoolDict[poolName].HideAllObject();
    }

    public void InitAllPools()
    {
        foreach (var p in PoolDict.Values)
        {
            p.InitPool();
        }
    }

}
