using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTokenCommand : EventCommand {

    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        CreateTokenArgs e = this.evt.data as CreateTokenArgs;
        GameObject token = PoolManager.Instance.GetObject("Token");
        token.transform.position = e.WorldPosition;
        token.transform.SetParent(e.Parent);

        //save data
        GameModel.Tokens.Add(token);

        //change UI
        token.GetComponent<TokenView>().InitFruit(GameModel.CurrentFruits[Random.Range(0, GameModel.CurrentFruits.Length)] - 1, e.MapPosition);
    }
}
