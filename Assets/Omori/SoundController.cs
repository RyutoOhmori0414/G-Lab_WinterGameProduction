using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
public class SoundController : MonoBehaviour
{
    CriAtomSource _atom;

    private void Start()
    {
        _atom = GetComponent<CriAtomSource>();
    }

    [SerializeField]
    string[] _cueNames;
}
