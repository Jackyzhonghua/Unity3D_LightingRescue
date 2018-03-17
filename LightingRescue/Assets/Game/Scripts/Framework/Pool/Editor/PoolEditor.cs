using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PoolEditor : MonoBehaviour {
    [MenuItem("Manager/Create PoolConfig")]
    static void CreatePoolList()
    {
        ObjectPoolList poolList = ScriptableObject.CreateInstance<ObjectPoolList>();
        AssetDatabase.CreateAsset(poolList, PoolManager.PoolConfigPath);
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("note", " successfully created", "OK");
    }
}
