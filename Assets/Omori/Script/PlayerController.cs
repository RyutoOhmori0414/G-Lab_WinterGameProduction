using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CriWare;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("�v���C���[�̈ړ��X�s�[�h"), SerializeField]
    float _moveSpeed = 5f;
    [Tooltip("���˂�����"), SerializeField]
    GameObject _snowBall;
    [Tooltip("�e��"), SerializeField]
    Transform _muzzle;
    [Tooltip("�e�̍ő吔"), SerializeField]
    int _maxBulletCount = 5;
    [Tooltip("�����[�h�̎���"), SerializeField]
    float _reloadTime = 2;
    [Tooltip("HP�̍ő吔"), SerializeField]
    int _maxHP = 3;
    [SerializeField]
    Animator _anim;

    [Header("���͊֌W")]
    [Tooltip("�㉺����"), SerializeField]
    string _verticalName = "Vertical";
    [Tooltip("���E����"), SerializeField]
    string _horizontalName = "Horizontal";
    [Tooltip("�U��"), SerializeField]
    string _attackName = "Fire1";
    [Tooltip("�����[�h"), SerializeField]
    string _reloadName;

    Rigidbody _rb;
    PlayerUIController _pUIController;
    int _currentBulletCount;
    int _currentHP;
    bool _isLoad = false;
    bool _isCandyFlag;
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
        //���͊֌W��ϐ��ɑ��
        float v = Input.GetAxisRaw(_verticalName);
        Debug.Log(v);
        float h = Input.GetAxisRaw(_horizontalName);

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
        _anim.SetFloat("WalkFloat", dir.magnitude);

        if (Input.GetAxisRaw(_attackName) > 0 && _currentBulletCount > 0 && !_isLoad && !_isCandyFlag && _isTrigger)
        {
            GameObject obj = Instantiate(_snowBall);
            obj.transform.position = _muzzle.position;
            obj.transform.forward = Camera.main.transform.forward;
            _anim.SetTrigger("ThrowTrigger");
            _isTrigger = false;
            _currentBulletCount--;
            _pUIController.BulletUIUpdate(_currentBulletCount);
        }// ��ʂ𔭎˂���
        else if (Input.GetAxisRaw(_attackName) == 0)
        {
            _isTrigger = true;
        }

        if (Input.GetButtonDown(_reloadName)&& _currentBulletCount != _maxBulletCount)
        {
            StartCoroutine(BulletReload());
            CriAtomSource criAtomSource = new CriAtomSource();
            criAtomSource.Play();
        }// �����[�h
    }

    /// <summary>
    /// �����[�h�̏���
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
