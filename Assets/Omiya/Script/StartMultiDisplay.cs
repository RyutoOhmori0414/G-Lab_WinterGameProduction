using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMultiDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Display.displays.Length);
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
