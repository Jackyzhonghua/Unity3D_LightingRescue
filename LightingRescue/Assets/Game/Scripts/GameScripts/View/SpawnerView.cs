using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : EventView
{
    public float[] FruitPosX = new float[] { -2.24f, -1.49f, -0.74f, 0, 0.74f, 1.49f, 2.24f};
    public float FruitPosY = 6.0f;
    private float gap = 0.8f;

    public void InitFruit()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                CreateTokenArgs e = new CreateTokenArgs() {
                    MapPosition = new Vector2(i,j),
                    WorldPosition = new Vector2(FruitPosX[i], FruitPosY + gap * j),
                    Parent = transform};
                dispatcher.Dispatch(GameEvents.ViewEvent.CREATETOKEN, e);
            }
        }
    }

    /// <summary>
    /// fill fruit
    /// </summary>
    /// <param name="XList"></param>
    /// <param name="yList"></param>
    public void FillFruits(List<int> xList, List<int> yList)
    {
        for(int i=0;i< xList.Count;++i)
        {
            int x = xList[i];
            int y = yList[i];

            CreateTokenArgs e = new CreateTokenArgs()
            {
                MapPosition = new Vector2(x, y),
                WorldPosition = new Vector2(FruitPosX[x], FruitPosY + gap * y - 5.5f),
                Parent = transform
            };
            dispatcher.Dispatch(GameEvents.ViewEvent.CREATETOKEN, e);
        }
    }

}
