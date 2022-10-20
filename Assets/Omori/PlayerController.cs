using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("�v���C���[�̈ړ��X�s�[�h"), SerializeField]
    float _moveSpeed = 5;
    [Tooltip("���˂�����"), SerializeField]
    GameObject _snowBall;
    [Tooltip("�e��"), SerializeField]
    Transform _muzzle;
    Rigidbody _rb;

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //���͊֌W��ϐ��ɑ��
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // �㉺���͂��s���߂�ɁA���E�����̂܂ܓ����悤�ɂ���
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // dir�̌����̊���v���C���[�̃J�����ɂ��� 
        dir = Camera.main.transform.TransformDirection(dir); // �����̓J�����𑝂₵���ۂɗv����
        // �J�����̏c�̃x�N�g�����v���C���[�̓����ɔ��f�����Ȃ�
        dir.y = 0;
        // ���͂��Ȃ���Ή�]���Ȃ�
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }
        // ���������̑��x�����̂܂܂ɂ���
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
