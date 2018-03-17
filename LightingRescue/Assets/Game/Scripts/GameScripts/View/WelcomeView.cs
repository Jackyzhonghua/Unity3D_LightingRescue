using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : View {

    [HideInInspector]
    public Button btn_Music;
    [HideInInspector]
    public Image img_Music;
    [HideInInspector]
    public Button btn_Play;

    private Sprite[] m_musicStateSprites;

    protected override void Awake()
    {
        base.Awake();
        btn_Music = this.transform.Find("btn_Music").GetComponent<Button>();
        img_Music = this.transform.Find("btn_Music").GetComponent<Image>();
        btn_Play = this.transform.Find("btn_Play").GetComponent<Button>();

        m_musicStateSprites = new Sprite[2];
        m_musicStateSprites[0] = GameContextView.Instance.AssetsManager.LoadSprite("Home/Button Music On");
        m_musicStateSprites[1] = GameContextView.Instance.AssetsManager.LoadSprite("Home/Button Music Off");
    }

    public void SetMusicImageState(bool mute)
    {
        img_Music.sprite = m_musicStateSprites[mute?1:0];
    }
}
