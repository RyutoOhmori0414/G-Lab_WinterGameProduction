using CriWare;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("SE")]
    [SerializeField] CriAtomSource _seSource;
    [Header("ME")]
    [SerializeField] CriAtomSource _win;
    [SerializeField] CriAtomSource _lose;

    public void PlaySE(CueSheetName audioName, string cueName)
    {
        _seSource.cueSheet = audioName.ToString();
        _seSource.cueName = cueName;
        _seSource.Play();
    }

    public enum CueSheetName
    {
        CueSheet_se,
        CueSheet_me,
        CueSheet_bgm
    }
}
