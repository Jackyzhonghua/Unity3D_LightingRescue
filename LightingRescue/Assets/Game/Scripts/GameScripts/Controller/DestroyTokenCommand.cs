using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTokenCommand : EventCommand {

    [Inject]
    public GameModel GameModel { get; set; }
    
    public override void Execute()
    {
        var tokens =  evt.data as List<GameObject>;
        //score
        int score = GetScore(tokens.Count);
        //tokens count
        int tokenCout = tokens.Count;
        Vector2[] tokensPoses = new Vector2[tokens.Count];
        for (int i = 0; i < tokens.Count; ++i)
        {
            tokensPoses[i] = tokens[i].GetComponent<TokenView>().MapPosition;
        }
        //check powerToken
        for (int i = 0; i < tokens.Count; ++i)
        {
            TokenView tokenView = tokens[i].GetComponent<TokenView>();
            //is this a power token
            switch (tokenView.Power)
            {
                case 1:
                    Power1Effect(tokenView.MapPosition, tokens);
                    break;
                case 2:
                    Power2Effect(tokenView.MapPosition, tokens);
                    break;
                case 3:
                    Power3Effect(tokenView.MapPosition, tokens);
                    break;
                default:
                    break;
            }
        }

        FillTokensArgs e = new FillTokensArgs();

        for (int i = 0; i< tokens.Count;++i)
        {
            TokenView tokenView = tokens[i].GetComponent<TokenView>();

            GameObject scoreGo = PoolManager.Instance.GetObject("Score");
            scoreGo.transform.position = tokens[i].transform.position;
            scoreGo.GetComponent<ScoreControl>().InitScore(score);
            GameModel.CurrentScore += score;

            Vector2 pos = tokenView.MapPosition;
            ResetMap(pos, tokens[i]);

            //explode
            GameObject effectGO = PoolManager.Instance.GetObject("ExplodeEffect");
            effectGO.transform.position = tokens[i].transform.position;

            int y = 0;
            foreach(var item in GameModel.Tokens)
            {
                if (item.GetComponent<TokenView>().MapPosition.x == pos.x)
                {
                    ++y;
                }   
            }
            e.xList.Add((int)pos.x);
            e.yList.Add(y);
        }
        if(score == 20)
        {
            GameFacede.Instance.PlayAudio("goodmusic");
        }
        else if( score == 10)
        {
            GameFacede.Instance.PlayAudio("clearmusic");
        }
        else if( score == -5)
        {
            GameFacede.Instance.PlayAudio("down");
        }
        //Fill new token
        dispatcher.Dispatch(GameEvents.ViewEvent.FILLTOKENS, e);
        //Update UI Score
        dispatcher.Dispatch(GameEvents.ViewEvent.CHANGESCORE, GameModel.CurrentScore);

        //bonus
        int power = GetBouns(tokenCout);
        while (true)
        {
            int r = Random.Range(0, tokenCout);
            Vector2 pos = tokensPoses[r];
            TokenView tv = null;
            for (int i = 0; i < GameModel.Tokens.Count; i++)
            {
                TokenView tmpTv = GameModel.Tokens[i].GetComponent<TokenView>();
                if (tmpTv.MapPosition.x == pos.x && tmpTv.MapPosition.y == pos.y && tmpTv.Power == 0)
                {
                    tmpTv.Power = power;
                    tv = tmpTv;
                    break;
                }
            }
            if (tv != null)
                break;
        }

    }

    /// <summary>
    /// Set map info in a colunm
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="token"></param>
    private void ResetMap(Vector2 pos, GameObject token)
    {
        if (GameModel.Tokens.Contains(token))
            GameModel.Tokens.Remove(token);
        else
            return;

        foreach(var t in GameModel.Tokens)
        {
            TokenView tv = t.GetComponent<TokenView>();
            if(tv.MapPosition.x == pos.x && tv.MapPosition.y >= pos.y)
            {
                tv.MapPosition.y -= 1;
            }
        }
        PoolManager.Instance.HideObject(token);
    }

    private int GetScore(int tokenCount)
    {
        int score = 0;
        if (tokenCount < 3)
        {
            score = -5;
        }
        else if (tokenCount < 6)
        {
            if(tokenCount > 3)
            {
                GameObject scoreGo = PoolManager.Instance.GetObject("Score");
                scoreGo.transform.position = new Vector2();
                scoreGo.GetComponent<ScoreControl>().InitScore(100);
                GameModel.CurrentScore += 100;
            }
            score = 10;
        }
        else
        {
            GameObject scoreGo = PoolManager.Instance.GetObject("Score");
            scoreGo.transform.position = new Vector2();
            scoreGo.GetComponent<ScoreControl>().InitScore(200);
            GameModel.CurrentScore += 200;
            score = 20;
        }
        return score;
    }

    private int GetBouns(int tokenCount)
    {
        int power = 0;
        if (tokenCount >= 6 && tokenCount <= 8)
            power = 1;
        else if (tokenCount > 8 && tokenCount <= 10)
            power = 2;
        else if (tokenCount > 10)
            power = 3;

        return power;
    }

    //destroy the rounded tokens
    private void Power1Effect(Vector2 pivot, List<GameObject> tokens)
    {
        foreach (var item in GameModel.Tokens)
        {
            Vector2 pos = item.GetComponent<TokenView>().MapPosition;
            if(Mathf.Abs(pos.x - pivot.x) <= 1 && Mathf.Abs(pos.y - pivot.y) <= 1)
            {
                if (!tokens.Contains(item))
                    tokens.Add(item);
            }
        }
    }

    //destroy the a colunm or a row of Tokens
    private void Power2Effect(Vector2 pivot, List<GameObject> tokens)
    {
        foreach (var item in GameModel.Tokens)
        {
            Vector2 pos = item.GetComponent<TokenView>().MapPosition;
            int isRow = Random.Range(0, 2);
            if(isRow == 1)
            {
                if (pos.x == pivot.x)
                {
                    if (!tokens.Contains(item))
                        tokens.Add(item);
                }
            }
            else
            {
                if (pos.y == pivot.y)
                {
                    if (!tokens.Contains(item))
                        tokens.Add(item);
                }
            }
        }
    }

    //destroy the a colunm and a row of Tokens
    private void Power3Effect(Vector2 pivot, List<GameObject> Tokens)
    {
        foreach (var item in GameModel.Tokens)
        {
            Vector2 pos = item.GetComponent<TokenView>().MapPosition;
            if (pos.x == pivot.x || pos.y == pivot.y)
            {
                if (!Tokens.Contains(item))
                    Tokens.Add(item);
            }

        }
    }
}
