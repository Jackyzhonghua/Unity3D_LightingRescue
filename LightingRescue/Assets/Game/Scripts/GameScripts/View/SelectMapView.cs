using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class SelectMapView : View {
    //[HideInInspector]
    public MapView[] MapViewArray;

    public Button Btn_Back;

    public void Init()
    {
        MapViewArray = GameObject.FindObjectsOfType<MapView>();
    }
    
}
