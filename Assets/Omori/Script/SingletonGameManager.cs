using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGameManager : MonoBehaviour
{
    // シングルトンでGameManagerを作る
    public static SingletonGameManager Instance = default;



    private void Awake()
    {
        // インスタンスチェック
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
