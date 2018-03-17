using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGameCommand : EventCommand {

    public override void Execute()
    {
        dispatcher.Dispatch(GameEvents.ViewEvent.UPDATESTOPGAME, evt.data);
    }
}
