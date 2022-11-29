using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [Tooltip("�A�N�e�B�u�ɂ���Display�̐�"), SerializeField, Range(1, 8)]
    int _activeDisplayCount = 1;

    void Start()
    {
        for (int i = 0; i < _activeDisplayCount; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
