using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class ItouTestMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _moveSpeedPowerUp = 5;
    Rigidbody _rb = default;
    [SerializeField] int _deathTimer = 5;
    [SerializeField] List<string> _itemTag = new();
    [SerializeField] int _hp;
    [SerializeField] int _hpMax = 3;
    [SerializeField] int _hpMaxPowerUp = 4;
    [SerializeField] Vector3 _respawnPosition;
    [SerializeField] Quaternion _respawnQuaternion;
    [SerializeField]List<Image> _hpImage = new();
    bool alive = true;
    bool _flag = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _respawnQuaternion = gameObject.transform.rotation;
        _respawnPosition = gameObject.transform.position;
    }
    void Update()
    {
        if (alive)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 dir = Vector3.forward * v + Vector3.right * h;
            // �J�����̃��[�J�����W�n����� dir ��ϊ�����
            dir = Camera.main.transform.TransformDirection(dir);
            // �J�����͎΂߉��Ɍ����Ă���̂ŁAY ���̒l�� 0 �ɂ��āuXZ ���ʏ�̃x�N�g���v�ɂ���
            dir.y = 0;
            // �ړ��̓��͂��Ȃ����͉�]�����Ȃ��B���͂����鎞�͂��̕����ɃL�����N�^�[��������B
            if (dir != Vector3.zero) this.transform.forward = dir;
            _rb.velocity = dir.normalized * _moveSpeed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ///�e�ɓ����������̗͌��炷
        if (other.tag == _itemTag[0])
        {
            _hpImage[_hp].gameObject.SetActive(false);
            _hp--;
            if (_hp <= 0)
            {
                StartCoroutine(StopPlayerMove());
            }
        }
        if (other.tag == _itemTag[1])
        {
            _moveSpeed = _moveSpeedPowerUp;
        }
        if (other.tag == _itemTag[2])
        {
            _hpMax = _hpMaxPowerUp;
            _hp++;
        }
    }
    /// <summary>
    /// �v���C���[�̗̑͂�0�ɂȂ������񎩕��̍s�����~�߂�
    /// </summary>
    /// <returns></returns>
    IEnumerator StopPlayerMove()
    {
        alive = false;
        Debug.Log("����ł�");
        yield return new WaitForSeconds(_deathTimer);
        Debug.Log("�����Ă�");
        if (_flag)
        {
            gameObject.transform.position = _respawnPosition;
            gameObject.transform.rotation = _respawnQuaternion;
        }
        for (int i = 0; i < _hpImage.Count; i++)
        {
            _hpImage[_hp].gameObject.SetActive(true);
        }
        _hp = _hpMax;
        alive = true;
    }
}
