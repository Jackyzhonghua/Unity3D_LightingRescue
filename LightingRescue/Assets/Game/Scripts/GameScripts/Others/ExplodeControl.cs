using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeControl : ReuseableObject {
    public override void BeforeGetObject()
    {
        StartCoroutine(Hide());
        gameObject.SetActive(true);
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.7f);
        PoolManager.Instance.HideObject(gameObject);
    }

    public override void BeforeHideObject()
    {
       
    }


}
