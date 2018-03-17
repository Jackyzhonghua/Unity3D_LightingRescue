using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingMediator : EventMediator {
    [Inject]
    public PlayingView PlayingView { get; set; }


    public override void OnRegister()
    {
        //only communitate with view
        PlayingView.dispatcher.AddListener(GameEvents.ViewEvent.GAMEOVER, OnGameOver);
        PlayingView.dispatcher.AddListener(GameEvents.ViewEvent.STOPGAME, OnStopGame);

        dispatcher.AddListener(GameEvents.ViewEvent.UPDATESHow, OnUpdateShow);
        dispatcher.AddListener(GameEvents.ViewEvent.CHANGESCORE, OnChangeScore);
        dispatcher.Dispatch(GameEvents.CommandEvnt.UPDATE_SHow);
    }

    public override void OnRemove()
    {
        PlayingView.dispatcher.RemoveListener(GameEvents.ViewEvent.GAMEOVER, OnGameOver);
        PlayingView.dispatcher.RemoveListener(GameEvents.ViewEvent.STOPGAME, OnStopGame);

        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATESHow, OnUpdateShow);
        dispatcher.RemoveListener(GameEvents.ViewEvent.CHANGESCORE, OnChangeScore);
    }

    private void OnUpdateShow(IEvent evt)
    {
        UpdateShowArgs e = evt.data as UpdateShowArgs;
        PlayingView.UpdateShow(e.Level, e.Score, e.Target);
        StartCoroutine(PlayingView.StartGame(e.LimitTime));
    }

    private void OnGameOver()
    {
        //start an event
        dispatcher.Dispatch(GameEvents.CommandEvnt.GAME_OVER);
    }

    private void OnChangeScore(IEvent evt)
    {
        PlayingView.ChangeScore((int)evt.data);
    }

    private void OnStopGame(IEvent evt)
    {
        dispatcher.Dispatch(GameEvents.CommandEvnt.STOP_GAME, evt.data);
    }
}
