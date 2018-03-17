using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager {

    public SoundManager(AudioSource aud)
    {
        m_audioShource = aud;
        m_audioShource.loop = true;
        m_audioShource.playOnAwake = false;
        Mute = Saver.GetMute();
    }

    #region BGM

    private AudioSource m_audioShource;

    public bool Mute
    {
        get { return m_audioShource.mute; }
        set {
            m_audioShource.mute = value;
            Saver.SetMute(value);
        }
    }

    public float BGMVolume //0-1
    {
        get { return m_audioShource.volume; }
        set { m_audioShource.volume = value; }
    }

    public void PlayBGM(string name)
    {
        AudioClip ac = GameContextView.Instance.AssetsManager.LoadAudioClip(name);
        if (ac != null)
        {
            m_audioShource.clip = ac;
            m_audioShource.Play();
        }
      
    }

    public void StopBGM()
    {
        m_audioShource.clip = null;
        m_audioShource.Stop();
    }


    #endregion

    //sound effect
    public void PlayAudio(string name)
    {
        if (Mute)
            return;
        AudioClip ac = GameContextView.Instance.AssetsManager.LoadAudioClip(name);
        if(ac != null)
            AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);
    }
}
