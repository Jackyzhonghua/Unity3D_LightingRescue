using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapMediator : EventMediator {
    [Inject]
    public SelectMapView SelectMapView { get; set; }

    public override void OnRegister()
    {
        SelectMapView.Init();
        for(int i = 0; i < SelectMapView.MapViewArray.Length;++i)
        {
            SelectMapView.MapViewArray[i].OnClick += OnMapClick;
        }
        SelectMapView.Btn_Back.onClick.AddListener(OnBackClick);

        //bind event
        dispatcher.AddListener(GameEvents.ViewEvent.UPDATESHow, OnUpdateGridShow);
        //request data
        dispatcher.Dispatch(GameEvents.CommandEvnt.UPDATE_SHow);
    }


    public override void OnRemove()
    {
        SelectMapView.Btn_Back.onClick.RemoveListener(OnBackClick);

        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATESHow, OnUpdateGridShow);
    }


    private void OnMapClick(int level)
    {
        //command to start game
        dispatcher.Dispatch(GameEvents.CommandEvnt.START_GAME, level);
    }

    private void OnBackClick()
    {
        SceneManager.LoadScene(Consts.Scene_welcome, LoadSceneMode.Single);
    }

    private void OnUpdateGridShow(IEvent evt)
    {
        int reachedLevel = (int)evt.data;
        for (int i = 0; i < SelectMapView.MapViewArray.Length; ++i)
        {
            SelectMapView.MapViewArray[i].Locked = SelectMapView.MapViewArray[i].Level > reachedLevel;
        }
         
    }
}
