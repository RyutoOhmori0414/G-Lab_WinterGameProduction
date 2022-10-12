using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // シングルトンでGameManagerを作る
    public static GameManager Instance = default;

    /// <summary>ゲームが開始されたときに動くAction</summary>
    public static Action OnGameStart;

    /// <summary>ゲームが終了したときに動くAction</summary>
    public static Action OnGameEnd;

    [Tooltip("プレイ時間の制限時間"), SerializeField]
    float _timeLimit = default;

    float _timer = default;

    /// <summary>ゲームシーンでゲームが動いているかどうか</summary>
    bool _OnGame = false;

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

    private void OnEnable()
    {
        // Actionに必要な関数を登録
        OnGameStart += GameStart;
        OnGameEnd += GameEnd;
    }

    private void Update()
    {
        if (_OnGame)
        {
            _timer += Time.deltaTime;

            if (_timer > _timeLimit)
            {
                // タイムオーバーした際に必要な処理を書く
            }
        }
    }

    /// <summary>
    /// ゲームが始まった際に行う処理
    /// </summary>
    void GameStart()
    {
        _OnGame = true;
    }

    /// <summary>
    /// ゲームが終了した際に行う処理
    /// </summary>
    void GameEnd()
    {
        _OnGame = false;
    }

    private void OnDisable()
    {
        // Actionの登録を消去
        OnGameStart -= GameStart;
        OnGameEnd -= GameEnd;
    }
}
