using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseMediator : EventMediator
{
    [Inject]
    public LooseView LooseView { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(GameEvents.ViewEvent.LOOSEGAME, OnLooseGame);

        LooseView.btn_Menu.onClick.AddListener(OnMenuClick);
        LooseView.btn_Restart.onClick.AddListener(OnRestart);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(GameEvents.ViewEvent.LOOSEGAME, OnLooseGame);

        LooseView.btn_Menu.onClick.RemoveListener(OnMenuClick);
        LooseView.btn_Restart.onClick.RemoveListener(OnRestart);
    }

    private void OnLooseGame(IEvent evt)
    {
        GameOverShowArgs e = evt.data as GameOverShowArgs;
        if (LooseView == null)
        {
            dispatcher.RemoveListener(GameEvents.ViewEvent.LOOSEGAME, OnLooseGame);
            return;
        }
        LooseView.Show(e.CurrentScore, e.Target);
    }

    private void OnMenuClick()
    {
        SceneManager.LoadScene(Consts.Scene_selectMap, LoadSceneMode.Single);
    }

    public void OnRestart()
    {
        dispatcher.Dispatch(GameEvents.CommandEvnt.START_GAME, -1);
    }
}
