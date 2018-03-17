using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton{
    private static Singleton _instance;
    public static Singleton Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }    
}
