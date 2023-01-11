using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Tooltip("カーソルのオンオフ"), SerializeField]
    bool _cursorVisible;

    void Start()
    {
        Cursor.visible = _cursorVisible;
    }
}
