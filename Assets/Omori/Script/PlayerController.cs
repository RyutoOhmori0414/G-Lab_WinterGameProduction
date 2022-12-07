using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CriWare;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("プレイヤーの移動スピード"), SerializeField]
    float _moveSpeed = 5f;
    [Tooltip("発射する雪玉"), SerializeField]
    GameObject _snowBall;
    [Tooltip("銃口"), SerializeField]
    Transform _muzzle;
    [Tooltip("弾の最大数"), SerializeField]
    int _maxBulletCount = 5;
    [Tooltip("リロードの秒数"), SerializeField]
    float _reloadTime = 2;
    [Tooltip("スタンしている秒数")]
    float _stanTime = 3f;
    [Tooltip("HPの最大数"), SerializeField]
    int _maxHP = 3;
    [Tooltip("プレイヤーのカメラ"),SerializeField]
    Camera _camera;
    [SerializeField]
    Animator _anim;
    [SerializeField]
    SEAudioController _audioController;

    [Header("入力関係")]
    [Tooltip("上下入力"), SerializeField]
    string _verticalName = "Vertical";
    [Tooltip("左右入力"), SerializeField]
    string _horizontalName = "Horizontal";
    [Tooltip("攻撃"), SerializeField]
    string _attackName = "Fire1";
    [Tooltip("リロード"), SerializeField]
    string _reloadName;

    Rigidbody _rb;
    PlayerUIController _pUIController;
    int _currentBulletCount;
    int _currentHP;
    /// <summary>現在のプレイヤーの状態</summary>
    PlayerState _state;
    /// <summary>現在のプレイヤーの状態</summary>
    public PlayerState _currentPlayerState { get { return _state; } }
    bool _isTrigger;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pUIController = GetComponent<PlayerUIController>();
        _currentBulletCount = _maxBulletCount;
        _currentHP = _maxHP;
    }

    private void Update()
    {
        //入力関係を変数に代入
        float v = Input.GetAxisRaw(_verticalName);
        float h = Input.GetAxisRaw(_horizontalName);

        // 上下入力を行く戻るに、左右をそのまま動くようにした
         Vector3 dirRaw = Vector3.forward * v + Vector3.right * h;
        // dirの向きの基準をプレイヤーのカメラにした 
        Vector3 dir = _camera.transform.TransformDirection(dirRaw);
        // カメラの縦のベクトルをプレイヤーの動きに反映させない
        dir.y = 0;
        // 入力がなければ回転しない
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }
        // 垂直方向の速度をそのままにする
        float y = _rb.velocity.y;

        _rb.velocity = dir.normalized * _moveSpeed + Vector3.up * y;
        _anim.SetFloat("WalkFloat", _rb.velocity.magnitude);

        if (Input.GetAxisRaw(_attackName) > 0 && 
            _currentBulletCount > 0 && 
            _state != PlayerState.Normal && 
            _isTrigger)
        {
            GameObject obj = Instantiate(_snowBall);
            obj.transform.position = _muzzle.position;
            obj.transform.forward = _camera.transform.forward;
            _anim.SetTrigger("ThrowTrigger");
            _isTrigger = false;
            _currentBulletCount--;
            _pUIController.BulletUIUpdate(_currentBulletCount);
            // SE
            _audioController.PlaySE(CueSheetName.CueSheet_se, "SE_Attack");
        }// 雪玉を発射する
        else if (Input.GetAxisRaw(_attackName) == 0)
        {
            _isTrigger = true;
        }

        if (Input.GetButtonDown(_reloadName)&& _currentBulletCount != _maxBulletCount)
        {
            StartCoroutine(BulletReload());
            CriAtomSource criAtomSource = new CriAtomSource();
            criAtomSource.Play();
            // SE
            _audioController.PlaySE(CueSheetName.CueSheet_se, "SE_ReLoad");
        }// リロード
    }

    /// <summary>
    /// リロードの処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletReload()
    {
        _currentBulletCount = _maxBulletCount;
        _pUIController.Reload();
        _state = PlayerState.isLoad;
        yield return new WaitForSeconds(_reloadTime);
        _pUIController.BulletUIUpdate(_currentBulletCount);
        _pUIController.Reload();
        _state = PlayerState.Normal;
    }

    IEnumerator Stan()
    {
        _state = PlayerState.Stan;
        yield return new WaitForSeconds(_);
    }

    /// <summary>
    /// 現在のプレイヤーの状態
    /// </summary>
    public enum PlayerState
    {
        Normal,
        isFlag,
        isLoad,
        SpeedUp,
        Stan
    }

    /// <summary>
    /// プレイヤーが死ぬときの処理
    /// </summary>
    public void PlayerDeath()
    {
        if (_state == PlayerState.isFlag)
        {

        } // リスポーンする処理を書く
        else
        {

        } // スタンする処理を書く
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _currentHP--;
            _pUIController.HpUIUpdate(_currentHP);
        } // 雪玉が当たったときに自分のHPを減らす処理未テスト
        else if (other.gameObject.CompareTag("Flag"))
        {
            _state = PlayerState.isFlag;
        } // フラグを取った際の処理
        else if (other.gameObject.CompareTag("SpeedUp"))
        {
            _state = PlayerState.SpeedUp;
        }
    }
}
