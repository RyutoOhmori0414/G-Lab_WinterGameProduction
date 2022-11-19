using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerReload))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerGetFlag : MonoBehaviour
{
    [Header("プレイヤーからいい感じにレイを飛ばすためのもの")]
    [SerializeField] Image _crosshair;
    [SerializeField] float _shootRange = 5f;
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _layerMask;
    [Header("選択ボタンの名前")]
    [SerializeField] string _getFlagButtonName;
    [Header("Flag取得時に起きてほしいものたち")]
    [SerializeField] Image _candyIcon;
    [SerializeField] Light _light;
    [SerializeField] Text _flagText;
    [SerializeField] float _carrySpeed = 3f;

    [Header("")]
    public Collider HitCollider = default;    // Ray が当たったコライダー
    [SerializeField] private Collider _hitCollider = default;
    bool _isFlag;
    public bool GetFlag { get => _getFlag; set => _getFlag = value; }
    private bool _getFlag;
    [Header("")]
    PlayerMove _move;
    PlayerShoot _shoot;
    PlayerReload _reload;
    private void Start()
    {
        _move = GetComponent<PlayerMove>();
        _shoot = GetComponent<PlayerShoot>();
        _reload = GetComponent<PlayerReload>();
    }
    void Update()
    {
        RayToSetHitCollider();
        FlagText();
        GetFlagEnum();
    }
    void RayToSetHitCollider()
    {
        // カメラから照準に向かって Ray を飛ばし、何かに当たっているか調べる
        Ray ray = _camera.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;

        // Ray が何かに当たったか・当たっていないかで処理を分ける
        // Ray が当たったオブジェクトをhitColliderに与える
        if (Physics.Raycast(ray, out hit, _shootRange, _layerMask))
        {
            _hitCollider = hit.collider;
        }
    }
    void GetFlagClick()
    {
        if (_getFlag) { return; }
        if (Input.GetButtonDown(_getFlagButtonName) && _hitCollider.CompareTag("Flag"))
        {
            _hitCollider.gameObject.SetActive(false);
            _shoot.enabled = false;
            _reload.enabled = false;
            _move.MoveSpeed = _carrySpeed;
            _light.enabled = true;
            Debug.Log("フラグ取得したよ");

            _candyIcon.enabled = true;
            _getFlag = true;
            _flagText.enabled = false;
        }

    }
    void GetFlagEnum()
    {
        if (!_getFlag) { return; }
        _move.MoveSpeed = _carrySpeed;
        //  _flag.gameObject.transform.position = this.gameObject.transform.position;
    }
    void FlagText()
    {
        if (_getFlag) { return; }
        if (_hitCollider == null) { return; }
        if (_hitCollider.gameObject.CompareTag("Flag"))
        {
            Debug.Log("Flagにカーソル合わせてるよ！");
            _flagText.enabled = true;
            _isFlag = true;
        }
        else if (_isFlag)
        {
            _flagText.enabled = false;
            _isFlag = false;
        }
        if (_isFlag)
        {
            if (Input.GetButtonDown(_getFlagButtonName))
            {
                GetFlagClick();
            }
        }
    }
}
