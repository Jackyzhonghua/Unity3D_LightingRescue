using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Saver {
    //reached Level
    static public int GetReachedLevel()
    {
        return PlayerPrefs.GetInt(Consts.ReachedLevel, 1);
    }

    static public void SetReachdLevel(int level, int maxlevel)
    {
        level = Mathf.Clamp(level, 1, maxlevel);
        PlayerPrefs.SetInt(Consts.ReachedLevel, level);
    }

    //music mute
    static public bool GetMute()
    {
        return PlayerPrefs.GetInt(Consts.Mute, 0) == 0 ? false : true;
    }

    static public void SetMute(bool mute)
    {
        PlayerPrefs.SetInt(Consts.Mute, mute ? 1 : 0);
    }
}
