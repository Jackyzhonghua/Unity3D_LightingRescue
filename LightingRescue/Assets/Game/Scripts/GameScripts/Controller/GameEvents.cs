using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents {
    public enum CommandEvnt
    {
        REQUEST_MAPINFO = 0,
        UPDATE_SHow = 1,
        START_GAME = 2,
        GAME_OVER = 3,
        CREATE_TOKEN = 4,
        DESTROY_TOKEN = 5,
        STOP_GAME = 6,
    }

    public enum ViewEvent
    {
        UPDATESHow = 0,
        GAMEOVER = 1,
        WINGAME = 2,
        LOOSEGAME = 3,
        CREATETOKEN = 4,
        CHANGESCORE = 5,
        FILLTOKENS = 6,
        STOPGAME = 7,
        UPDATESTOPGAME = 8,
    }
}
