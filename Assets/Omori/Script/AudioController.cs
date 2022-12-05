using CriWare;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("SE")]
    [SerializeField] CriAtomSource _attack;
    [SerializeField] CriAtomSource _buffItem;
    [SerializeField] CriAtomSource _pushButton;
    [SerializeField] CriAtomSource _getCandy;
    [SerializeField] CriAtomSource _damage;
    [SerializeField] CriAtomSource _reload;
    [SerializeField] CriAtomSource _run;
    [SerializeField] CriAtomSource _stan;
    [Header("ME")]
    [SerializeField] CriAtomSource _win;
    [SerializeField] CriAtomSource _lose;

    public void Play(AudioName audioName)
    {

    }

    public enum AudioName
    {
        Attack,
        Buff,
        PushButton,
        GetCandy,
        Damage,
        Reload,
        Run,
        Stan,
        Win,
        lose
    }
}
