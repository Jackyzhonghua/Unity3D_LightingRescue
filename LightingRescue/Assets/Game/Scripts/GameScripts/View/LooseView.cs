using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LooseView : View {
    public Text Score;
    public Text Target;

    public Button btn_Menu;
    public Button btn_Restart;


    public void Show(int score, int target)
    {
        this.gameObject.SetActive(true);
        Score.text = score.ToString();
        Target.text = target.ToString();
    }
}
