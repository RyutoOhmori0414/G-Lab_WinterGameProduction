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
    Collider hitCollider = default;    // Ray �����������R���C�_�[
    bool _isFlag;
    void Update()
    {
        // �J��������Ə��Ɍ������� Ray ���΂��A�����ɓ������Ă��邩���ׂ�
        Ray ray = _camera.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;

        // Ray �������ɓ����������E�������Ă��Ȃ����ŏ����𕪂���        
        if (Physics.Raycast(ray, out hit, _shootRange))
        {
            hitCollider = hit.collider;    // Ray �����������I�u�W�F�N�g
            if (hitCollider.tag == "Flag")
            {
                Debug.Log("Flag�ɃJ�[�\�����킹�Ă��I");
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
