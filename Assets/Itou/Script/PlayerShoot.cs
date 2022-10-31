using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerReload))]
public class PlayerShoot : MonoBehaviour
{
    /// <summary> 残弾 </summary>
    public int RemainingBullets = 10;
    /// <summary>「弾」のプレハブ</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    /// <summary>弾/レーザーを発射する地点を設定する</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>レーザーの射程距離</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>レーザーが当たった時に加える力</summary>
    [SerializeField] float _shootPower = 10f;

    [SerializeField] Image _crosshair;

    [SerializeField] Camera _camera;
    [SerializeField] Text _remainingBulletsText;
    [Header("発射ボタンのインプットマネージャーの名前")]
    [SerializeField, Tooltip("発射ボタンの名前")] string _shot;
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