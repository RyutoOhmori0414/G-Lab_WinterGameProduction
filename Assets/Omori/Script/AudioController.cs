using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    CriAtomSource _atomSource;

    private void Start()
    {
        _atomSource = FindObjectOfType<CriAtomSource>();
        _atomSource.Play("BGM");
    }
}
