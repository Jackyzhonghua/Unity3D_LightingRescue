using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCommand : EventCommand {

    public override void Execute()
    {
        //play BG music
        GameFacede.Instance.PlayBGM("music");

        //Make game not to be destroied
        Object.DontDestroyOnLoad(GameObject.Find("Game"));

        //get data
        dispatcher.Dispatch(GameEvents.CommandEvnt.REQUEST_MAPINFO);

        //load next scene, get into game
        SceneManager.LoadScene(Consts.Scene_welcome, LoadSceneMode.Single);
    }

}
