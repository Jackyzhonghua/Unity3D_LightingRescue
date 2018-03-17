using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCommand : EventCommand {

    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        int level = (int)this.evt.data;

        PoolManager.Instance.InitAllPools();
        //-1 replay current level
        //-2 play next level
        if(level == -1)
        {
            if (GameModel.PlayLevel(GameModel.CurrentLevel))
                SceneManager.LoadScene(Consts.Scene_playing, LoadSceneMode.Single);
        }
        else if(level == -2)
        {
            if (GameModel.PlayLevel(GameModel.CurrentLevel + 1))
                SceneManager.LoadScene(Consts.Scene_playing, LoadSceneMode.Single);
        }
        else
        {
            if (GameModel.PlayLevel(level))
                SceneManager.LoadScene(Consts.Scene_playing, LoadSceneMode.Single);
        }
    }

}
