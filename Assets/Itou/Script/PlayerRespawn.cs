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
    [Header("�����֘A")]
    [SerializeField] Vector3 _respawnPosition;
    [SerializeField] Quaternion _respawnQuaternion;
    [SerializeField] int _deathTimer = 5;
    [Header("�^�O�֘A")]
    [SerializeField] string _flagTag = "Flag";
    [Header("���̃X�N���v�g")]
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
        //�ŏ��̈ʒu�Ɗp�x���o����
        _respawnQuaternion = gameObject.transform.rotation;
        _respawnPosition = gameObject.transform.position;
    }

    /// <summary>
    /// �v���C���[�̗̑͂�0�ɂȂ������̏���
    /// 1.�������擾�����A�C�e����S�����̏ꏊ�ɖ߂��B
    /// 2.�������擾�����A�C�e���̋����X�e�[�^�X��S�����ɖ߂�
    /// 3.�v���C���[�̑�����~�߂�
    /// 4.��莞�Ԍ�Ƀ��X�|���n�_�ɂă��X�|��
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerDeath()
    {
        //�v���C���[�̑�����~�߂�
        _playerMove.enabled = false;
        _playerShoot.enabled = false;
        _playerReload.enabled = false;
        //���X�|�����Ԃ�҂��Ă��炢�낢�돈��������B
        yield return new WaitForSeconds(_deathTimer);
        //�t���O�擾���������X�|���n�_�Ń��X�|��
        if (_flag)
        {
            gameObject.transform.position = _respawnPosition;
            gameObject.transform.rotation = _respawnQuaternion;
        }
        //�A�C�e���擾�O�̃X�e�[�^�X�ɂ���
        //�܂��A�C�e�������̏ꏊ�ɖ߂��X�N���v�g�͏����ĂȂ�
        _playerHP.HpMax = _playerHP.HpMaxDefault;
        _playerMove.MoveSpeed = _playerMove.MoveSpeedDefault;
        //�����̗̑͂�UI���ďo��������
        for (int i = 0; i < _playerHP.HpMax; i++)
        {
            _playerHP.HpImage[i].gameObject.GetComponent<Image>().color = Color.white;
            _playerHP.Hp++;
        }
        _playerHP.HpImage[_playerHP.HpMax].gameObject.GetComponent<Image>().color = Color.clear;
        //�v���C���[�̑�����ċN������
        _playerMove.enabled = true;
        _playerShoot.enabled = true;
        _playerReload.enabled = true;
    }

    public void Pause()
    {
        //�v���C���[�̑�����~�߂�
        _playerMove.enabled = false;
        _playerShoot.enabled = false;
        _playerReload.enabled = false;
    }

    public void Resume()
    {
        //�v���C���[�̑�����ċN������
        _playerMove.enabled = true;
        _playerShoot.enabled = true;
        _playerReload.enabled = true;
    }
}
