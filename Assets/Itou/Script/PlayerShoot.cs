using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerReload))]
public class PlayerShoot : MonoBehaviour
{
    /// <summary> �c�e </summary>
    public int RemainingBullets = 10;
    /// <summary>�u�e�v�̃v���n�u</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    /// <summary>�e/���[�U�[�𔭎˂���n�_��ݒ肷��</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>���[�U�[�̎˒�����</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>���[�U�[�������������ɉ������</summary>
    [SerializeField] float _shootPower = 10f;

    [SerializeField] Image _crosshair;

    [SerializeField] Camera _camera;
    [SerializeField] Text _remainingBulletsText;
    [Header("���˃{�^���̃C���v�b�g�}�l�[�W���[�̖��O")]
    [SerializeField, Tooltip("���˃{�^���̖��O")] string _shot;
    PlayerReload _playerReload;
    private void Start()
    {
        _playerReload = GetComponent<PlayerReload>();
        _remainingBulletsText.text = $"{RemainingBullets} / {_playerReload.ReloadCount}";
    }
    void Update()
    {
        if (Input.GetButtonDown(_shot))
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
        RemainingBullets--;
        var go = Instantiate(_bulletPrefab);
        go.transform.position = _camera.transform.position;
        go.transform.LookAt(_muzzle);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * _shootPower, ForceMode.Impulse);
        _remainingBulletsText.text = $"{RemainingBullets} / {_playerReload.ReloadCount}";
    }
}