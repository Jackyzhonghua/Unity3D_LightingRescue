using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameContextView : ContextView
{
    public static GameFacede Instance;
    private IAssetsManager m_assetsManager;
    public IAssetsManager AssetsManager { get { return m_assetsManager; } }
    private SoundManager m_soundManager;

    protected virtual void Awake()
    {
        InitManager();
    }

    void InitManager()
    {
        m_assetsManager = new ResourceAssetsProxyManager();
        m_soundManager = new SoundManager(GetComponent<AudioSource>());
    }

    public void PlayBGM(string name)
    {
        m_soundManager.PlayBGM(name);
    }

    public void PlayAudio(string name)
    {
        m_soundManager.PlayAudio(name);
    }

    public bool SoundMute
    {
        get { return m_soundManager.Mute; }
        set { m_soundManager.Mute = value; }
    }
}
