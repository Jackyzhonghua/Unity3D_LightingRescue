using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchView : View {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private GameObject prevFruit;
    private int prevId;
    private bool needMusic = false;

    public bool Stop = false;

    private List<GameObject> SelectedFruits = new List<GameObject>();
    private List<GameObject> Lines = new List<GameObject>();

	void Update () 
	{
        if(Stop)
        {
            return;
        }

		if(Input.GetMouseButtonDown(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(point);
            if (col != null && col.tag == "Fruit")
            {
                col.GetComponent<TokenView>().Selected = true;
                prevFruit = col.gameObject;
                prevId = prevFruit.GetComponent<TokenView>().FruitID;
                prevFruit.GetComponent<TokenView>().Selected = true;
                SelectedFruits.Add(prevFruit);
                needMusic = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(point);
            if (col != null && col.tag == "Fruit")
            {
                GameObject tempFruit = col.gameObject;
                //if is on a new fruit, is this neibor
                if (tempFruit != prevFruit && CheckDIstance(tempFruit, prevFruit))
                {
                    int tempID = tempFruit.GetComponent<TokenView>().FruitID;
                    if (tempID == prevId)
                    {
                        if (needMusic)
                        {
                            GameFacede.Instance.PlayAudio("Selected");
                            needMusic = false;
                        }
                        //on a selected fruit
                        if (SelectedFruits.Contains(tempFruit))
                        {
                            //fruit count is more one, and on the last second fruit
                            if(SelectedFruits.Count > 1 && tempFruit == SelectedFruits[SelectedFruits.Count - 2])
                            {
                                //cancle the selection of the last fruit
                                GameObject fruit = SelectedFruits[SelectedFruits.Count - 1];
                                fruit.GetComponent<TokenView>().Selected = false;
                                SelectedFruits.Remove(fruit);
                                //set the new last
                                prevFruit = SelectedFruits[SelectedFruits.Count - 1];
                                PoolManager.Instance.HideObject(Lines[Lines.Count - 1]);
                                Lines.RemoveAt(Lines.Count - 1);
                            }
                   
                        }
                        //in a new fruit
                        else
                        {
                            CreatLine(tempFruit, prevFruit);
                            prevFruit = tempFruit;
                            //prevId = tempFruit.GetComponent<FruitView>().FruitID;
                            prevFruit.GetComponent<TokenView>().Selected = true;
                            SelectedFruits.Add(tempFruit);
                            needMusic = true;
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedFruits.Count > 1)
            {
                //send a event to delete fruit
                dispatcher.Dispatch(GameEvents.CommandEvnt.DESTROY_TOKEN, SelectedFruits);
            }
            else
            {
                if (SelectedFruits.Count > 0)
                {
                    prevFruit.GetComponent<TokenView>().Selected = false;
                }
            }
            SelectedFruits.Clear();
            prevFruit = null;

            PoolManager.Instance.HideAllObject("Line");
            Lines.Clear();
        }
            
    }

    private bool CheckDIstance(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 tempPos = tempFruit.GetComponent<TokenView>().MapPosition;
        Vector2 prevPos = prevFruit.GetComponent<TokenView>().MapPosition;
        return Mathf.Abs(tempPos.x - prevPos.x) <= 1 && Mathf.Abs(tempPos.y - prevPos.y) <= 1;
    }

    private void CreatLine(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 linePos = new Vector2((tempFruit.transform.position.x + prevFruit.transform.position.x) / 2, (tempFruit.transform.position.y + prevFruit.transform.position.y) / 2);
        Quaternion rotation = Quaternion.Euler(0, 0, GetRotationZ(tempFruit, prevFruit));
        GameObject go = PoolManager.Instance.GetObject("Line");
        go.transform.position = linePos;
        go.transform.rotation = rotation;
        Lines.Add(go);
    }

    private float GetRotationZ(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 fruit1PosMap = tempFruit.GetComponent<TokenView>().MapPosition;
        Vector2 fruit2PosMap = prevFruit.GetComponent<TokenView>().MapPosition;

        if (fruit1PosMap.x == fruit2PosMap.x)
            return 0;
        if (fruit1PosMap.y == fruit2PosMap.y)
            return 90f;

        float temp = (fruit1PosMap.x - fruit2PosMap.x) * (fruit1PosMap.y - fruit2PosMap.y);

        if (temp < 0)
            return 45f;
        else
            return -45f;
    }
}
