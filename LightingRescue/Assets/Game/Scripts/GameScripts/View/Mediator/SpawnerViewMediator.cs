using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerViewMediator : EventMediator {
    [Inject]
    public SpawnerView SpawnerView { get; set; }

    public override void OnRegister()
    {
        SpawnerView.dispatcher.AddListener(GameEvents.ViewEvent.CREATETOKEN, OnViewInitFruit);
        dispatcher.AddListener(GameEvents.ViewEvent.FILLTOKENS, OnFillFruits);

        SpawnerView.InitFruit();
    }

    public override void OnRemove()
    {
        SpawnerView.dispatcher.RemoveListener(GameEvents.ViewEvent.CREATETOKEN, OnViewInitFruit);
        dispatcher.RemoveListener(GameEvents.ViewEvent.FILLTOKENS, OnFillFruits);
    }

    private void OnViewInitFruit(IEvent evt)
    {
        dispatcher.Dispatch(GameEvents.CommandEvnt.CREATE_TOKEN, evt.data);
    }

    private void OnFillFruits(IEvent evt)
    {
        var e = evt.data as FillTokensArgs;
        SpawnerView.FillFruits(e.xList, e.yList);
    }

}
