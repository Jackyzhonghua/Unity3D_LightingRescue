using strange.extensions.context.api;
using System;
using System.Collections;
using UnityEngine;

public class GameService : ISomeService
{
    public string URL { get; set; }

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject ContextView { get; set; }

    public void AddListerner(Action<string> linster)
    {
        TextAsset ta = Resources.Load<TextAsset>(URL);
        if (linster != null)
            linster(ta.text);

        //ContextView.GetComponent<GameFacede>().StartCoroutine(GetData(linster));
    }

    private IEnumerator GetData(Action<string> linster)
    {
        WWW www = new WWW(URL);
        yield return www;
        if(www.error == null)
        {
            if(linster != null)
                linster(www.text);
        }
    }
}
