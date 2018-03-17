using UnityEngine;
using System;

public interface ISomeService {
    string URL { get; set; }
    void AddListerner(Action<string> linster);
}
