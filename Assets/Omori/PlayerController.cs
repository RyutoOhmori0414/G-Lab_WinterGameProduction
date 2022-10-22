using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("プレイヤーの移動スピード"), SerializeField]
    float _moveSpeed = 5;
    [Tooltip("発射する雪玉"), SerializeField]
    GameObject _snowBall;
    [Tooltip("銃口"), SerializeField]
    Transform _muzzle;
    Rigidbody _rb;

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
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

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject obj = Instantiate(_snowBall);
            obj.transform.position = _muzzle.position;
            obj.transform.forward = Camera.main.transform.forward;
        }
    }
}
