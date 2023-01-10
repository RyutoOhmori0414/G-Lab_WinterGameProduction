using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("プレイ時間の制限時間"), SerializeField]
    float _timeLimit = default;
    [Tooltip("ゲームが始まるまでの猶予時間"), SerializeField]
    float _setUpTime = default;
    [Tooltip("旗のTransform"), SerializeField]
    Transform _flagTransform;
    [Tooltip("Aチームの陣地のTransform"), SerializeField]
    Transform _teamATransform;
    [Tooltip("Bチームの陣地のTransform"), SerializeField]
    Transform _teamBTransform;
    [Tooltip("Flagのprefab"), SerializeField]
    GameObject _flagPrefab;
    [Tooltip("Flagのリスポーン位置"), SerializeField]
    Transform _responeTramsform;

    MainUIController _mainUIController;
    float _timer = default;
    /// <summary>Timerが動いているかどうか</summary>
    bool _running = false;
    List<IPausable> _pausables = new();
    bool _gameStart = true;

    private void Start()
    {
        _running = true;
        // IPausableを実装しているGamaObjectを探して最初にポーズをかけている
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        _mainUIController = FindObjectOfType<MainUIController>();

        foreach(var n in gameObjects)
        {
            var temp = n.GetComponent<IPausable>();
            
            if (temp != null)
            {
                temp.Pause();
                _pausables.Add(temp);
            }
        }
    }

    private void Update()
    {
        if (_running)
        {
            Cursor.visible = false;
            _timer += Time.deltaTime;
            if (_timer < _setUpTime)
            {
                _mainUIController.CountTextUpdate(_setUpTime - _timer);
            }
            else if (_timer > _setUpTime && _gameStart)
            {
                // 最初の待ち時間が終わってから行う処理
                // カウントダウンが終わったらポーズを解除する
                foreach (var n in _pausables)
                {
                    n.Resume();
                }

                _mainUIController.CountEnd();
                _gameStart = false;
            } // カウントダウンが終わった際に一度だけ行われる
            else if (_timer > _timeLimit + _setUpTime)
            {
                // タイムリミットが過ぎたら行う処理
                TimeOverGameEnd();
                _running = false;
                _mainUIController.SubCountEnd();
            }
            else
            {
                _mainUIController.SubCountTextUpdate(_timeLimit - (_timer - _setUpTime));
            } // ゲーム中の処理
        }
        else
        {
            if (Input.anyKeyDown)
            {
                _mainUIController.ToTitle();
            }
        }

    }

    public void GameEnd(Team winTeam)
    {
        // まずポーズをかけるべきオブジェクトを止める
        foreach (var n in _pausables)
        {
            n.Pause();
        }
        _running = false;

        _mainUIController.EndGame(winTeam);
    }

    void TimeOverGameEnd()
    {
        // まずポーズをかけるべきオブジェクトを止める
        foreach(var n in _pausables)
        {
            n.Pause();
        }

        _mainUIController.DrawGame();
    }

    public void FlagRespone()
    {
        GameObject tempGO = Instantiate(_flagPrefab);
        tempGO.transform.position = _responeTramsform.position;
        _flagTransform = tempGO.transform;
    }

    public void GetFlag(Transform _playerTransform)
    {
        _flagTransform = _playerTransform;
    }

    public enum Team
    {
        ATeam,
        BTeam
    }
}
