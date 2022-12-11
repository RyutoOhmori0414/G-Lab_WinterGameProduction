using CriWare;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEAudioController : MonoBehaviour
{
    CriAtomSource _seSource;

    private void Start()
    {
        _seSource = GetComponent<CriAtomSource>();
    }

    public void PlaySE(CueSheetName audioName, string cueName)
    {
        _seSource.cueSheet = audioName.ToString();
        _seSource.cueName = cueName;
        _seSource.loop = audioName == CueSheetName.CueSheet_se_loop ? true : false;
        _seSource.Play();
    }
}
public enum CueSheetName
{
    CueSheet_se,
    CueSheet_se_loop,
    CueSheet_me,
    CueSheet_bgm
}
