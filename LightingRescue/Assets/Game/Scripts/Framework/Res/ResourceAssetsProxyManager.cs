using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAssetsProxyManager : IAssetsManager
{
    private ResourceAssetsManager m_assetManager = new ResourceAssetsManager();
    private Dictionary<string, AudioClip> m_clips = new Dictionary<string, AudioClip>();
    private Dictionary<string, Sprite> m_sprites = new Dictionary<string, Sprite>();
    private Dictionary<string, GameObject> m_effects = new Dictionary<string, GameObject>();

    public AudioClip LoadAudioClip(string name)
    {
        if(!m_clips.ContainsKey(name))
            m_clips.Add(name, m_assetManager.LoadAudioClip(name));
        return m_clips[name];
    }

    public GameObject LoadEffect(string name)
    {
        if (!m_effects.ContainsKey(name))
            m_effects.Add(name, ResourceAssetsManager.LoadAsset<GameObject>(ResourceAssetsManager.EffectPath + name));
        return GameObject.Instantiate(m_effects[name]);
    }

    public Sprite LoadSprite(string name)
    {
        if (!m_sprites.ContainsKey(name))
            m_sprites.Add(name, m_assetManager.LoadSprite(name));
        return m_sprites[name];
    }

    public void ReleaseAll()
    {
        foreach(var k in m_clips.Keys)
            m_clips[k] = null;
        m_clips.Clear();

        foreach (var k in m_sprites.Keys)
            m_sprites[k] = null;
        m_sprites.Clear();

        foreach (var k in m_effects.Keys)
            m_effects[k] = null;
        m_effects.Clear();

        Resources.UnloadUnusedAssets();
    }
}
