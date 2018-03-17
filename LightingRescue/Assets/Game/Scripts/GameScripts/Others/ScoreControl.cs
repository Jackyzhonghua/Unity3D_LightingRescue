using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : ReuseableObject {
    public Sprite[] ScoreSprites;

    private SpriteRenderer scoreRenderer;



    public void InitScore(int score)
    {
        if (score == -5)
        {
            scoreRenderer.sprite = ScoreSprites[0];
            Special = false;
        }
        else if(score == 10)
        {
            scoreRenderer.sprite = ScoreSprites[1];
            Special = false;
        }
        else if (score == 20)
        {
            scoreRenderer.sprite = ScoreSprites[2];
            Special = false;
        }
        else if (score == 100)
        {
            scoreRenderer.sprite = ScoreSprites[3];
            Special = true;
        }
        else if (score == 200)
        {
            scoreRenderer.sprite = ScoreSprites[4];
            Special = true;
        }
    }

    public bool Special
    {
        set
        {
            if(value)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.7f, 0.7f, 1);
            }
        }
    }


    public override void BeforeGetObject()
    {
        if (scoreRenderer == null)
            scoreRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        StartCoroutine(Hide());
        gameObject.SetActive(true);
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(1);
        PoolManager.Instance.HideObject(gameObject);
    }

    public override void BeforeHideObject()
    {
        Special = false;
    }
}
