using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerReload))]
public class PlayerShoot : MonoBehaviour
{
    /// <summary> �c�e </summary>
    public int RemainingBullets { get => _remainingBullets; set => _remainingBullets = value; }
    [SerializeField] int _remainingBullets = 5;
    /// <summary>�u�e�v�̃v���n�u</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    /// <summary>�e/���[�U�[�𔭎˂���n�_��ݒ肷��</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>���[�U�[�̎˒�����</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>���[�U�[�������������ɉ������</summary>
    [SerializeField] float _shootPower = 10f;
    [SerializeField] Camera _camera;
    //[SerializeField] Text _remainingBulletsText;
    [SerializeField] List<Image> _bulletImage = new();
    [Header("���˃{�^���̃C���v�b�g�}�l�[�W���[�̖��O")]
    [SerializeField, Tooltip("���˃{�^���̖��O")] string _shot;
    PlayerReload _playerReload;
    public List<Image> BulletImage { get => _bulletImage; set => _bulletImage = value; }
    private void Start()
    {
        _playerReload = GetComponent<PlayerReload>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (RemainingBullets > 1)
            {
                Shot();
            }
            else if (RemainingBullets == 1)
            {
                Shot();
                StartCoroutine(_playerReload.ReloadAction());
            }
        }
    }
    void Shot()
    {
        _remainingBullets--;
        _bulletImage[RemainingBullets].gameObject.GetComponent<Image>().color = Color.gray;//�e��UI�������
        var go = Instantiate(_bulletPrefab);
        go.transform.position = _camera.transform.position;
        go.transform.LookAt(_muzzle);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * _shootPower, ForceMode.Impulse);
    }
}