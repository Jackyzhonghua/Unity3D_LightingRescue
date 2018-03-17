using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class MapView : MonoBehaviour {
    public Sprite[] States;
    

    public int Level = -1;
    private bool m_locked = true;
    public bool Locked
    {
        get { return m_locked; }
        set
        {
            int index = value ? 0 : 1;
            GetComponent<Image>().sprite = States[index];
            if(!value)
            {
                GetComponentInChildren<Text>().text = Level.ToString();
            }

            m_locked = value;
        }
    }

    [HideInInspector]
    public Action<int> OnClick;

    public void OnPointClick()
    {
        if(!Locked && OnClick != null)
        {
            OnClick(this.Level);
        }
    } 

 

    private void OnDestroy()
    {
        while(OnClick != null)
            OnClick -= OnClick;
    }
}
