using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerHP))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerReload))]
public class PlayerRespawn : MonoBehaviour, IPausable
{
    [Header("復活関連")]
    [SerializeField] Vector3 _respawnPosition;
    [SerializeField] Quaternion _respawnQuaternion;
    [SerializeField] int _deathTimer = 5;
    [Header("タグ関連")]
    [SerializeField] string _flagTag = "Flag";
    [Header("他のスクリプト")]
    [SerializeField] PlayerHP _playerHP;
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerShoot _playerShoot;
    [SerializeField] PlayerReload _playerReload;

    bool _flag = false;
    bool alive = true;
    private void Start()
    {
        _playerHP = GetComponent<PlayerHP>();
        _playerMove = GetComponent<PlayerMove>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerReload = GetComponent<PlayerReload>();
        //最初の位置と角度を覚える
        _respawnQuaternion = gameObject.transform.rotation;
        _respawnPosition = gameObject.transform.position;
    }

    /// <summary>
    /// プレイヤーの体力が0になった時の処理
    /// 1.自分が取得したアイテムを全部元の場所に戻す。
    /// 2.自分が取得したアイテムの強化ステータスを全部元に戻す
    /// 3.プレイヤーの操作を止める
    /// 4.一定時間後にリスポン地点にてリスポン
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerDeath()
    {
        //プレイヤーの操作を止める
        _playerMove.enabled = false;
        _playerShoot.enabled = false;
        _playerReload.enabled = false;
        //リスポン時間を待ってからいろいろ処理させる。
        yield return new WaitForSeconds(_deathTimer);
        //フラグ取得時だけリスポン地点でリスポン
        if (_flag)
        {
            gameObject.transform.position = _respawnPosition;
            gameObject.transform.rotation = _respawnQuaternion;
        }
        //アイテム取得前のステータスにする
        //まだアイテムを元の場所に戻すスクリプトは書いてない
        _playerHP.HpMax = _playerHP.HpMaxDefault;
        _playerMove.MoveSpeed = _playerMove.MoveSpeedDefault;
        //自分の体力のUIを再出現させる
        for (int i = 0; i < _playerHP.HpMax; i++)
        {
            _playerHP.HpImage[i].gameObject.GetComponent<Image>().color = Color.white;
            _playerHP.Hp++;
        }
        _playerHP.HpImage[_playerHP.HpMax].gameObject.GetComponent<Image>().color = Color.clear;
        //プレイヤーの操作を再起させる
        _playerMove.enabled = true;
        _playerShoot.enabled = true;
        _playerReload.enabled = true;
    }

    public void Pause()
    {
        //プレイヤーの操作を止める
        _playerMove.enabled = false;
        _playerShoot.enabled = false;
        _playerReload.enabled = false;
    }

    public void Resume()
    {
        //プレイヤーの操作を再起させる
        _playerMove.enabled = true;
        _playerShoot.enabled = true;
        _playerReload.enabled = true;
    }
}
