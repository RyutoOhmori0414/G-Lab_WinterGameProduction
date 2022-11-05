using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーそれぞれについてるカメラ")]
    [SerializeField] Camera camera;
    [Header("プレイヤーの移動入力のInputManager内の名前")]
    [SerializeField] string _horizontal = "Horizontal";
    [SerializeField] string _vertical = "Vertical";
    [Header("移動速度")]
    [SerializeField] private float _moveSpeed = 3;
    [Header("速度関連")]
    [SerializeField] private float _moveSpeedDefault = 3;
    [SerializeField] private float _moveSpeedPowerUp = 5;
    Rigidbody _rb = default;
    [Header("タグ関連")]
    [SerializeField] string _speedUpTag = "SpeedUp";
    public float MoveSpeedDefault { get => _moveSpeedDefault; set => _moveSpeedDefault = value; }
    public float MoveSpeedPowerUp { get => _moveSpeedPowerUp; set => _moveSpeedPowerUp = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw(_horizontal);
        float v = Input.GetAxisRaw(_vertical);
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // カメラのローカル座標系を基準に dir を変換する
        dir = camera.transform.TransformDirection(dir);
        // カメラは斜め下に向いているので、Y 軸の値を 0 にして「XZ 平面上のベクトル」にする
        dir.y = 0;
        // 移動の入力がない時は回転させない。入力がある時はその方向にキャラクターを向ける。
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        ///移動速度増加アイテムに当たった時
        if (other.tag == _speedUpTag)
        {
            _moveSpeed = _moveSpeedPowerUp;
        }
    }
}
