using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Tooltip("�J�[�\���̃I���I�t"), SerializeField]
    bool _cursorVisible;

    void Start()
    {
        Cursor.visible = _cursorVisible;
    }
}
