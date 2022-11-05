using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("�v���C���[���ꂼ��ɂ��Ă�J����")]
    [SerializeField] Camera camera;
    [Header("�v���C���[�̈ړ����͂�InputManager���̖��O")]
    [SerializeField] string _horizontal = "Horizontal";
    [SerializeField] string _vertical = "Vertical";
    [Header("�ړ����x")]
    [SerializeField] private float _moveSpeed = 3;
    [Header("���x�֘A")]
    [SerializeField] private float _moveSpeedDefault = 3;
    [SerializeField] private float _moveSpeedPowerUp = 5;
    Rigidbody _rb = default;
    [Header("�^�O�֘A")]
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
        // �J�����̃��[�J�����W�n����� dir ��ϊ�����
        dir = camera.transform.TransformDirection(dir);
        // �J�����͎΂߉��Ɍ����Ă���̂ŁAY ���̒l�� 0 �ɂ��āuXZ ���ʏ�̃x�N�g���v�ɂ���
        dir.y = 0;
        // �ړ��̓��͂��Ȃ����͉�]�����Ȃ��B���͂����鎞�͂��̕����ɃL�����N�^�[��������B
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        ///�ړ����x�����A�C�e���ɓ���������
        if (other.tag == _speedUpTag)
        {
            _moveSpeed = _moveSpeedPowerUp;
        }
    }
}
