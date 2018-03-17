using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UpdateShowCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        int reachedLevel = GameModel.ReachedLevel;

        //todo
        if(SceneManager.GetActiveScene().name == "2.selectMap")
            dispatcher.Dispatch(GameEvents.ViewEvent.UPDATESHow, reachedLevel);

        if (SceneManager.GetActiveScene().name == "3.playing")
            dispatcher.Dispatch(GameEvents.ViewEvent.UPDATESHow, new UpdateShowArgs() { Level = GameModel.CurrentLevel, Target = GameModel.CurrentTarget, Score = GameModel.CurrentScore, LimitTime = GameModel.CurrentLimitTime});
    }

}
