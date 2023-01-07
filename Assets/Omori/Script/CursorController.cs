using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Tooltip("カーソルのオンオフ")]

    void Start()
    {
        Cursor.visible = false;
    }
}
