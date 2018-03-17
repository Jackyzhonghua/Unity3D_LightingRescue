using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel {
    private List<MapInfo> m_mapList;
    public List<MapInfo> MapList { set { m_mapList = value; } }


    //the dictionary for Tokens' position
    public List<GameObject> Tokens = new List<GameObject>();
    //Selected token
    //private List<GameObject> SelectedFruits = new List<GameObject>();

    private MapInfo m_curMapInfo;

    private int m_curScore;
    private int m_curLevel;

    public int CurrentTarget { get { return m_curMapInfo.TargetScore; } }
    public float CurrentLimitTime { get { return m_curMapInfo.LimitTime; } }
    public int CurrentLevel { get { return m_curLevel; } }
    public int CurrentScore {  get { if (m_curScore < 0) m_curScore = 0; return m_curScore; } set { m_curScore = value; } }
    public int[] CurrentFruits { get { return m_curMapInfo.FruitIndexs; } }

    public int ReachedLevel
    {
        get { return Saver.GetReachedLevel(); }
        set
        {

            Saver.SetReachdLevel(value, m_mapList.Count);
        }
    }


    //public void AddSelectedFruit(GameObject fruit)
    //{
    //    SelectedFruits.Add(fruit);
    //}

    public bool PlayLevel(int level)
    {
        if (level > m_mapList.Count)
            return false;
        m_curMapInfo = m_mapList[level - 1];
        m_curLevel = level;
        m_curScore = 0;
        Tokens.Clear();
        return true;
    }

}
