using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetsManager
{
    AudioClip LoadAudioClip(string name);
    GameObject LoadEffect(string name);
    Sprite LoadSprite(string name);

    void ReleaseAll();
}


