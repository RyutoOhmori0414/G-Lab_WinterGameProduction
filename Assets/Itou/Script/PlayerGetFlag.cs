using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGetFlag : MonoBehaviour
{
    [SerializeField] Image _crosshair;
    [SerializeField] float _shootRange = 5f;
    [SerializeField] Text _flagText;
    [SerializeField] Camera _camera;
    Collider hitCollider = default;    // Ray が当たったコライダー
    bool _isFlag;
    void Update()
    {
        // カメラから照準に向かって Ray を飛ばし、何かに当たっているか調べる
        Ray ray = _camera.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;

        // Ray が何かに当たったか・当たっていないかで処理を分ける        
        if (Physics.Raycast(ray, out hit, _shootRange))
        {
            hitCollider = hit.collider;    // Ray が当たったオブジェクト
            if (hitCollider.tag == "Flag")
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
        }
    }
}
