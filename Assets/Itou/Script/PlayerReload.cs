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
    public int ReloadCount = 10;
    PlayerShoot _playerShoot;
    [Header("UI")]
    [SerializeField] Text _remainingBulletsText;
    [SerializeField] Text _reloadText;

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
            StartCoroutine(ReloadAction());
        }
    }
    public IEnumerator ReloadAction()
    {
        _playerShoot.enabled = false;
        _reloadText.enabled = true;
        yield return new WaitForSeconds(_reloadTime);
        _playerShoot.RemainingBullets = ReloadCount;
        _remainingBulletsText.text = $"{_playerShoot.RemainingBullets} / {ReloadCount}";
        _reloadText.enabled = false;
        _playerShoot.enabled = true;
    }
}
