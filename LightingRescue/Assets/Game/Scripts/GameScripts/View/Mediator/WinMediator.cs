using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMediator : EventMediator
{
    [Inject]
    public WinView WinView { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(GameEvents.ViewEvent.WINGAME, OnWinGame);
        WinView.btn_Menu.onClick.AddListener(OnMenuClick);
        WinView.btn_Next.onClick.AddListener(OnNextClick);
        WinView.btn_Retry.onClick.AddListener(OnRetryClick);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(GameEvents.ViewEvent.WINGAME, OnWinGame);
        WinView.btn_Menu.onClick.RemoveListener(OnMenuClick);
        WinView.btn_Next.onClick.RemoveListener(OnNextClick);
        WinView.btn_Retry.onClick.RemoveListener(OnRetryClick);
    }

    private void OnWinGame(IEvent evt)
    {
        GameOverShowArgs e = evt.data as GameOverShowArgs;
        if(WinView == null)
        {
            dispatcher.RemoveListener(GameEvents.ViewEvent.WINGAME, OnWinGame);
            return;
        }
        WinView.Show(e.CurrentScore, e.Target);
    }

    private void OnMenuClick()
    {
        SceneManager.LoadScene(Consts.Scene_selectMap, LoadSceneMode.Single);
    }

    private void OnNextClick()
    {
        dispatcher.Dispatch(GameEvents.CommandEvnt.START_GAME, -2);
    }

    private void OnRetryClick()
    {
        dispatcher.Dispatch(GameEvents.CommandEvnt.START_GAME, -1);
    }
}
