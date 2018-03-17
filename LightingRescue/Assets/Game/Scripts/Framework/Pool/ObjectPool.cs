using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectPool {
    //object
    public GameObject Prefab;

    //name of pool
    public string Name;

    //max number
    public int MaxCount;

    //the collection of the objects in the pool
    [NonSerialized]
    private List<GameObject> PrefabList = new List<GameObject>();


    //get a hided object
    public GameObject GetObject()
    {
        GameObject go = null;
        foreach(var o in PrefabList)
        {
            if (o.activeSelf == false)
            {
                go = o;
                go.SetActive(true);
                break;
            }
        }
        if(go == null)
        {
            if(PrefabList.Count >= MaxCount)
            {
                GameObject.Destroy(PrefabList[0]);
                PrefabList.RemoveAt(0);
            }
            go = GameObject.Instantiate<GameObject>(Prefab);
            PrefabList.Add(go);
        }
        else
        {
            PrefabList.Remove(go);
            PrefabList.Add(go);
        }
        go.SendMessage("BeforeGetObject", SendMessageOptions.DontRequireReceiver);
        return go;
    }

    //hide on specific object
    public void HideObject(GameObject go)
    {
        if(PrefabList.Contains(go))
        {
            go.SendMessage("BeforeHideObject", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
            PrefabList.Remove(go);
            PrefabList.Insert(0, go);
        }
    }

    //hide all
    public void HideAllObject()
    {
        foreach(var o in PrefabList)
        {
            if (o.activeSelf == true)
                o.SetActive(false);
        }
    }

    public void InitPool()
    {
        PrefabList = new List<GameObject>();
    }

}
