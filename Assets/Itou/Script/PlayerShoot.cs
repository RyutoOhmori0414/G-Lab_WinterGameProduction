using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerReload))]
public class PlayerShoot : MonoBehaviour
{
    /// <summary> 残弾 </summary>
    public int RemainingBullets { get => _remainingBullets; set => _remainingBullets = value; }
    [SerializeField] int _remainingBullets = 5;
    /// <summary>「弾」のプレハブ</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    /// <summary>弾/レーザーを発射する地点を設定する</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>レーザーの射程距離</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>レーザーが当たった時に加える力</summary>
    [SerializeField] float _shootPower = 10f;
    [SerializeField] Camera _camera;
    //[SerializeField] Text _remainingBulletsText;
    [SerializeField] List<Image> _bulletImage = new();
    [Header("発射ボタンのインプットマネージャーの名前")]
    [SerializeField, Tooltip("発射ボタンの名前")] string _shot;
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
        _bulletImage[RemainingBullets].gameObject.GetComponent<Image>().color = Color.gray;//弾のUIが一個減る
        var go = Instantiate(_bulletPrefab);
        go.transform.position = _camera.transform.position;
        go.transform.LookAt(_muzzle);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * _shootPower, ForceMode.Impulse);
    }
}