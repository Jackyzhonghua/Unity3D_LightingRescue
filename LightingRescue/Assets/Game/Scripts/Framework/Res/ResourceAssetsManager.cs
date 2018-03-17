using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetsManager : IAssetsManager
{
    public static readonly string AudioPath = "Musics/";
    public static readonly string SpritePath = "Textures/";
    public static readonly string EffectPath = "Effects/";

    public AudioClip LoadAudioClip(string name)
    {
        return LoadAsset<AudioClip>(AudioPath + name);
    }

    public GameObject LoadEffect(string name)
    {
        return InstantiateGameObject(EffectPath + name);
    }

    public Sprite LoadSprite(string name)
    {
        return LoadAsset<Sprite>(SpritePath + name);
    }

    private GameObject InstantiateGameObject(string path)
    {
        GameObject o = LoadAsset<GameObject>(path);
        if(o == null)
        {
            return null;
        }
        return UnityEngine.GameObject.Instantiate(o) as GameObject;
    }

    public static T LoadAsset<T>(string path) where T:UnityEngine.Object
    {
        if(path == null || path == string.Empty)
        {
            Debug.LogError("Fail to load resource, path is null or empty.");
            return null;
        }
        T o = Resources.Load<T>(path);
        if(o == null)
        {
            Debug.LogError("Fail to load resource, path: " + path);
            return null;
        }
        return o;
    }

    public void ReleaseAll()
    {
        
    }
}
