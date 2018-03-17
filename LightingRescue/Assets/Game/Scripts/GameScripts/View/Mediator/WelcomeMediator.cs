using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeMediator : Mediator {
    [Inject]
    public WelcomeView m_welcomeView { get; set; }

    public override void OnRegister()
    {
        m_welcomeView.btn_Play.onClick.AddListener(OnPlayClick);
        m_welcomeView.btn_Music.onClick.AddListener(OnMusicClick);
        m_welcomeView.SetMusicImageState(Saver.GetMute());
    }

    public override void OnRemove()
    {
        m_welcomeView.btn_Play.onClick.RemoveListener(OnPlayClick);
        m_welcomeView.btn_Music.onClick.RemoveListener(OnMusicClick);
    }

    private void OnPlayClick()
    {
        SceneManager.LoadScene(Consts.Scene_selectMap, LoadSceneMode.Single);
    }

    private void OnMusicClick()
    {
        GameFacede.Instance.SoundMute = !GameFacede.Instance.SoundMute;
        m_welcomeView.SetMusicImageState(Saver.GetMute());
    }
}
