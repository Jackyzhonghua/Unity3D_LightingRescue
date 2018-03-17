using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour,  IEndDragHandler {

    private ScrollRect m_scrollRect;
    private int m_totalPage = 27 / 9;
    private int m_page = 1;
    public int Page
    {
        get { return m_page; }
        set
        {
            value = Mathf.Clamp(value, 1, m_totalPage);
            m_page = value;
            target = (float)(m_page - 1) / (float)(m_totalPage - 1);
            start = true;
        }
    }

    private float target;
    private bool start;



    void Awake()
    {
        m_scrollRect = transform.GetComponent<ScrollRect>();
    }


    void Update()
    {
        if (start)
        {
            m_scrollRect.horizontalNormalizedPosition = Mathf.Lerp(m_scrollRect.horizontalNormalizedPosition, target, Time.deltaTime * 10);
            if (Mathf.Abs(m_scrollRect.horizontalNormalizedPosition - target) < 0.001f)
                start = false;
        }


    }

    public void UpdatePageNumber()
    {
        if(start == false)
        {
            float temp = (Mathf.Max(0, m_scrollRect.horizontalNormalizedPosition) * (m_totalPage - 1) + 1);
            m_page = temp % 1 > 0.5 ? (int)temp + 1 : (int)temp;
        }
 
    }

    public void ChangePage(int amount)
    {
        Page += amount;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float gap = (float)1 / (float)(m_totalPage - 1);
        float h = m_scrollRect.horizontalNormalizedPosition + gap / 2;
        Page = (int)(h / gap) + 1;
    }

}
