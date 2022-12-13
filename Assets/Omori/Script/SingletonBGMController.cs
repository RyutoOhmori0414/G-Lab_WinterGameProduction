using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class SingletonBGMController: MonoBehaviour
{
    static public SingletonBGMController instance;

    [SerializeField]
    CriAtomSource _BGMSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToGame()
    {
        _BGMSource.Stop();
        _BGMSource.cueName = "BGM_InGame";
        _BGMSource.Play();
    }

    public void ToTitle()
    {
        _BGMSource.Stop();
        _BGMSource.cueName = "BGM_Title";
        _BGMSource.Play();
    }
}
