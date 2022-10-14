using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>ゲームが開始されたときに動くAction</summary>
    public static Action OnGameStart;

    /// <summary>ゲームが終了したときに動くAction</summary>
    public static Action OnGameEnd;

    [Tooltip("プレイ時間の制限時間"), SerializeField]
    float _timeLimit = default;
    [Tooltip("ゲームが始まるまでの猶予時間"), SerializeField]
    float _setUpTime = default;

    float _timer = default;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _setUpTime)
        {
            // 最初の待ち時間が終わってから行う処理
            
        }
        if (_timer > _timeLimit)
        {
            // タイムリミットが過ぎたら行う処理
        }

    }
}
