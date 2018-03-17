using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMapInfoCommand : Command {
    [Inject]
    public ISomeService GameService { get; set; }

    [Inject]
    public GameModel GameModel { get; set; }


    public override void Execute()
    {
        Retain();
        GameService.URL = "Map";
        GameService.AddListerner(OnComplete);
    }

    private void OnComplete(string json)
    {
        Release();
        MapInfoList mapList = JsonUtility.FromJson<MapInfoList>(json);
        GameModel.MapList = mapList.MapList;
    }
}
