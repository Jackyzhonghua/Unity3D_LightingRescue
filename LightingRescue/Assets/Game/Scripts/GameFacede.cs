using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacede :  GameContextView {

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        context = new GameContext(this, true);
        context.Start();
    }
    

}
