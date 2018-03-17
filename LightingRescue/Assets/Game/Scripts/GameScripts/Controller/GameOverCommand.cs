using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCommand : EventCommand {
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        bool isWin = GameModel.CurrentScore >= GameModel.CurrentTarget;

        GameOverShowArgs e = new GameOverShowArgs() { CurrentScore = GameModel.CurrentScore, Target = GameModel.CurrentTarget };

        if(isWin)
        {
            if(GameModel.CurrentLevel == GameModel.ReachedLevel)
                GameModel.ReachedLevel++;
            dispatcher.Dispatch(GameEvents.ViewEvent.WINGAME, e);
        }
        else
        {
            dispatcher.Dispatch(GameEvents.ViewEvent.LOOSEGAME, e);
        }
    }
}
