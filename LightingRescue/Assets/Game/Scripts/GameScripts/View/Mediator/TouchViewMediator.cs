using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchViewMediator : EventMediator
{
    [Inject]
    public TouchView TouchView { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(GameEvents.ViewEvent.UPDATESTOPGAME, OnUpdateStopGame);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATESTOPGAME, OnUpdateStopGame);
    }

    private void OnUpdateStopGame(IEvent vet)
    {
        TouchView.Stop = (bool)vet.data;
    }

}
