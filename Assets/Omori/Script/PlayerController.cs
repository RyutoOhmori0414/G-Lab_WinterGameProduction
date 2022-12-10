using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CriWare;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IPausable
{
    [Tooltip("プレイヤーの移動スピード"), SerializeField]
    float _moveSpeed = 5f;
    [Tooltip("スピードアップ後の移動スピード"), SerializeField]
    float _powerUpSpeed = 7f;
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
    [Tooltip("プレイヤーのリスポーン位置"), SerializeField]
    Transform _responeTransform;
    [Tooltip("プレイヤーの陣営"), SerializeField]
    GameController.Team _playerTeam;
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
    GameController _gameController;
    int _currentBulletCount;
    int _currentHP;
    /// <summary>現在のプレイヤーの状態</summary>
    PlayerState _state = PlayerState.Pause;
    PlayerState _lastState = PlayerState.Normal;
    bool _isTrigger;

    public GameController.Team PlayerTeam { get => _playerTeam; }
    /// <summary>現在のプレイヤーの状態</summary>
    public PlayerState CurrentPlayerState { get { return _state; } }
   

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pUIController = GetComponent<PlayerUIController>();
        _gameController = FindObjectOfType<GameController>();
        _currentBulletCount = _maxBulletCount;
        _currentHP = _maxHP;
    }

    private void Update()
    {
        if (_state != PlayerState.Stan)
        {
            //入力関係を変数に代入
            float v = Input.GetAxisRaw(_verticalName);
            float h = Input.GetAxisRaw(_horizontalName);
            
            // 上下入力を行く戻るに、左右をそのまま動くようにした
            Vector3 dirRaw = Vector3.forward * v + Vector3.right * h;
            // dirの向きの基準をプレイヤーのカメラにした 
            Vector3 dir = transform.TransformDirection(dirRaw);
            // カメラの縦のベクトルをプレイヤーの動きに反映させない
            dir.y = 0;

            if (_state == PlayerState.SpeedUp)
            {
                _rb.velocity = dir.normalized * _powerUpSpeed + Vector3.up * _rb.velocity.y;
            }
            else
            {
                _rb.velocity = dir.normalized * _moveSpeed + Vector3.up * _rb.velocity.y;
            }

            //this.transform.rotation = new Quaternion(transform.rotation.x, _camera.transform.rotation.y ,transform.rotation.z, transform.rotation.w);
            transform.eulerAngles = new Vector3(transform.rotation.x, _camera.transform.rotation.y * 180 + 360, transform.rotation.z);
            //transform.Rotate(transform.rotation.x, _camera.transform.rotation.y, transform.rotation.z);

            _anim.SetFloat("WalkFloat", _rb.velocity.magnitude);

            if (Input.GetAxisRaw(_attackName) > 0 &&
                _currentBulletCount > 0 &&
                _state != PlayerState.isFlag &&
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
            else if (Input.GetAxisRaw(_attackName) <= 0.1f)
            {
                _isTrigger = true;
            }

            if (Input.GetButtonDown(_reloadName) && _currentBulletCount != _maxBulletCount)
            {
                StartCoroutine(BulletReload());
                CriAtomSource criAtomSource = new CriAtomSource();
                criAtomSource.Play();
                // SE
                _audioController.PlaySE(CueSheetName.CueSheet_se, "SE_ReLoad");
            }// リロード
        }

        Debug.Log($"{this.gameObject.name}の状態は{_state.ToString()}");
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
        _audioController.PlaySE(CueSheetName.CueSheet_se_loop, "SE_Stan");
        yield return new WaitForSeconds(_stanTime);
        _state = PlayerState.Normal;
        yield break;
    }

    /// <summary>
    /// 現在のプレイヤーの状態
    /// </summary>
    public enum PlayerState
    {
        Pause,
        Normal,
        isFlag,
        isLoad,
        SpeedUp,
        Stan
    }

    public void GameStart()
    {
        _state = PlayerState.Normal;
    }

    /// <summary>
    /// プレイヤーが死ぬときの処理
    /// </summary>
    public void PlayerDeath()
    {
        if (_state == PlayerState.isFlag)
        {
            // リスポーン処理
            this.transform.position = _responeTransform.position;
            _state = PlayerState.Normal;
            // ステータスのリセット
            _currentBulletCount = _maxBulletCount;
            _currentHP = _maxHP;
            _pUIController.BulletUIUpdate(_currentBulletCount);
            _pUIController.HpUIUpdate(_currentHP);
            // flagをその位置に出現させる処理　ゲームマネージャーを呼ぶ
            _gameController.FlagRespone(this.transform.position);
        } // リスポーンする処理を書く
        else
        {
            StartCoroutine(Stan());
        } // スタンする処理を書く
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _currentHP--;
            _pUIController.HpUIUpdate(_currentHP);
            if (_currentHP >= 0)
            {
                PlayerDeath();
            }
        } // 雪玉が当たったときに自分のHPを減らす処理未テスト
        else if (other.gameObject.CompareTag("Flag"))
        {
            _state = PlayerState.isFlag;
            _gameController.GetFlag(this.transform);
            Destroy(other.gameObject);
            Debug.Log("旗を取りました");
        } // フラグを取った際の処理
        else if (other.gameObject.CompareTag("SpeedUp") && _state != PlayerState.isFlag)
        {
            _state = PlayerState.SpeedUp;
        }
    }

    public void Pause()
    {
        _lastState = CurrentPlayerState;
        _state = PlayerState.Pause;
    }

    public void Resume()
    {
        _state = _lastState;
    }
}
