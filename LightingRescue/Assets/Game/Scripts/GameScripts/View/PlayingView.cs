using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingView : EventView {
    public Text txt_Score;
    public Text txt_Target;
    public Text txt_level;
    public Slider sid_Timer;

    public Button btn_Stop;
    private bool isStop = false;

    protected override void Awake()
    {
        btn_Stop.onClick.AddListener(OnStopClick);
    }

    public void UpdateShow(int lv, int score, int target)
    {
        txt_Score.text = score.ToString();
        txt_level.text = lv.ToString();
        txt_Target.text = target.ToString();
    }

    public void ChangeScore(int Score)
    {
        txt_Score.text = Score.ToString();
    }

    public IEnumerator StartGame(float currentTime)
    {
        while (sid_Timer.value > 0.001f)
        {
            yield return new WaitForSeconds(1f);
            if(!isStop)
            {
                sid_Timer.value -= 1 / currentTime;
            }
        }
        dispatcher.Dispatch(GameEvents.ViewEvent.GAMEOVER);
        dispatcher.Dispatch(GameEvents.ViewEvent.STOPGAME, true);
    }

    public void OnStopClick()
    {
        isStop = !isStop;
        dispatcher.Dispatch(GameEvents.ViewEvent.STOPGAME, isStop);
    }
}
