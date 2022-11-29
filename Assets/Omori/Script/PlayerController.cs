using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    [Tooltip("リロードの時間"), SerializeField]
    float _reloadTime = 2;
    [Tooltip("HPの最大数"), SerializeField]
    int _maxHP = 3;
    [SerializeField]
    Animator _anim;

    Rigidbody _rb;
    PlayerUIController _pUIController;
    int _currentBulletCount;
    int _currentHP;
    bool _isLoad = false;
    bool _isCandyFlag;

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
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 上下入力を行く戻るに、左右をそのまま動くようにした
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // dirの向きの基準をプレイヤーのカメラにした 
        dir = Camera.main.transform.TransformDirection(dir); // ここはカメラを増やした際に要調整
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
        _anim.SetFloat("WalkFloat", dir.magnitude);

        if (Input.GetButtonDown("Fire1") && _currentBulletCount > 0 && !_isLoad && !_isCandyFlag)
        {
            GameObject obj = Instantiate(_snowBall);
            obj.transform.position = _muzzle.position;
            obj.transform.forward = Camera.main.transform.forward;
            _anim.SetTrigger("ThrowTrigger");
            _currentBulletCount--;
            _pUIController.BulletUIUpdate(_currentBulletCount);
        }// 雪玉を発射する

        if (Input.GetKeyDown("r") && _currentBulletCount != _maxBulletCount)
        {
            StartCoroutine(BulletReload());
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
        _isLoad = true;
        yield return new WaitForSeconds(_reloadTime);
        _pUIController.BulletUIUpdate(_currentBulletCount);
        _pUIController.Reload();
        _isLoad = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _currentHP--;
            _pUIController.HpUIUpdate(_currentHP);
        }
    }
}
