using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerReload : MonoBehaviour
{
    [Header("リロードボタンのインプットマネージャーの名前")]
    [SerializeField, Tooltip("リロードボタンの名前")] string Reload;
    [Header("リロード時間")]
    [SerializeField] float _reloadTime = 3f;
    [Header("戻してほしい弾数")]
    public int ReloadCount = 5;
    PlayerShoot _playerShoot;
    [Header("UI")]
    [SerializeField] Text _reloadText;
    [SerializeField] Image _crosshair;

    private void Start()
    {
        _playerShoot = GetComponent<PlayerShoot>();
        _reloadText.enabled = false;
    }
    private void Update()
    {
        //if (Input.GetButtonDown(Reload))
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_playerShoot.RemainingBullets != ReloadCount)
            {
                StartCoroutine(ReloadAction());
            }
        }
    }
    public IEnumerator ReloadAction()
    {
        _playerShoot.enabled = false;
        _reloadText.enabled = true;
        _crosshair.enabled = false;
        yield return new WaitForSeconds(_reloadTime);
        _playerShoot.RemainingBullets = ReloadCount;
        _reloadText.enabled = false;
        _playerShoot.enabled = true;
        _crosshair.enabled = true;
        for (int i = 0; i < ReloadCount; i++)
        {
            _playerShoot.BulletImage[i].color = Color.white;
        }
    }
}
