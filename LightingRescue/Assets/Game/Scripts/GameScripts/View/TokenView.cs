using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TokenView : ReuseableObject {
    public Sprite[] FruitSprites;
    public Sprite[] PowerSprite;

    private SpriteRenderer[] m_spriteRenderers;
    public SpriteRenderer[] SpriteRenderers
    {
        get
        {
            return m_spriteRenderers;
        }
    }

    private int m_fruitID = -1;
    public int FruitID { get { return m_fruitID; } }

    [HideInInspector]
    public Vector2 MapPosition;

    private Transform SelectedTranform;
    private Transform PowerTransform;
    private int m_power = 0;
    public int Power
    {
        get { return m_power; }
        set
        {
            m_power = value;
            if(value != 0)
            {
                SpriteRenderers[2].gameObject.SetActive(value >= 1 && value <= 3);
                m_spriteRenderers[2].sprite = PowerSprite[value - 1];
            }
  
        }
    }

    public bool Selected
    {
        set
        {
            if (value)
                SelectedTranform.gameObject.SetActive(true);
            else
                SelectedTranform.gameObject.SetActive(false);
        }
    }


    public void InitFruit(int fruitId, Vector2 pos)
    {
        SpriteRenderers[0].sprite = FruitSprites[fruitId];
        SpriteRenderers[1].sprite = FruitSprites[fruitId];
        m_fruitID = fruitId;
        MapPosition = pos;
        gameObject.name = "Fruit" + FruitID.ToString();
    }

    public override void BeforeGetObject()
    {
        if (m_spriteRenderers == null)
        {
            m_spriteRenderers = new SpriteRenderer[3];
            m_spriteRenderers[0] = GetComponent<SpriteRenderer>();
            m_spriteRenderers[1] = transform.Find("Select").GetComponent<SpriteRenderer>();
            m_spriteRenderers[2] = transform.Find("Power").GetComponent<SpriteRenderer>();
        }

        if (SelectedTranform == null)
        {
            SelectedTranform = transform.Find("Select");
        }

        Selected = false;
    }

    public override void BeforeHideObject()
    {
        m_power = 0;
        m_spriteRenderers[2].gameObject.SetActive(false);
    }


}
