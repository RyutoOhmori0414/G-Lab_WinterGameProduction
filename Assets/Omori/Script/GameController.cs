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
    [Tooltip("Flagnのprefab"), SerializeField]
    GameObject _flagPrefab;

    float _timer = default;
    /// <summary>Timerが動いているかどうか</summary>
    bool _running = false;
    List<IPausable> _pausables = new();

    private void Start()
    {
        _running = true;
        // IPausableを実装しているGamaObjectを探して最初にポーズをかけている
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

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
        //if (_running)
        //{
        //    Cursor.visible = false;
        //    _timer += Time.deltaTime;

        //    if (_timer > _setUpTime)
        //    {
        //        // 最初の待ち時間が終わってから行う処理
        //        // カウントダウンが終わったらポーズを解除する
        //        foreach(var n in _pausables)
        //        {
        //            n.Resume();
        //        }
        //    }
        //    if (_timer > _timeLimit + _setUpTime)
        //    {
        //        // タイムリミットが過ぎたら行う処理
        //        TimeOverGameEnd();
        //        _running = false;
        //    }
        //}

    }

    public void GameEnd(Team winTeam)
    {
        // まずポーズをかけるべきオブジェクトを止める
        foreach (var n in _pausables)
        {
            n.Pause();
        }

        if (winTeam == Team.ATeam)
        {
            // Aチームwin
            // UIをpanelを出す
        }
        else
        {
            // Bチームwin
            // UIのパネルを出す
        }
    }

    void TimeOverGameEnd()
    {
        // まずポーズをかけるべきオブジェクトを止める
        foreach(var n in _pausables)
        {
            n.Pause();
        }

        // フラグがAチームの陣地に近かったら
        if (Vector3.Distance(_flagTransform.position, _teamATransform.position) < Vector3.Distance(_flagTransform.position, _teamBTransform.position))
        {
            // Aチームwin
            // UIをpanelを出す
        }
        else
        {
            // Bチームwin
            // UIのパネルを出す
        }
    }

    public void FlagRespone(Vector3 _sponePosition)
    {
        GameObject tempGO = Instantiate(_flagPrefab);
        tempGO.transform.position = _sponePosition;
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
