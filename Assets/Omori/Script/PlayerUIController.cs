using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Tooltip("HP�̃C���[�W"), SerializeField]
    Image[] _hpImages;
    [Tooltip("��ʂ̃C���[�W"), SerializeField]
    Image[] _snowBallImages;
    [Tooltip("�Ə�"), SerializeField]
    Image _crossheir;
    [Tooltip("�����[�h���̃e�L�X�g"), SerializeField]
    Text _reloadText;

    public void HpUIUpdate(int currentHp)
    {
        for (int i = 0; i < _hpImages.Length; i++)
        {
            if (i < currentHp)
            {
                _hpImages[i].enabled = true;
            }
            else
            {
                _hpImages[i].enabled = false;
            }
        }
    }

    public void BulletUIUpdate(int currentBullet)
    {
        for (int i = 0; i < _snowBallImages.Length; i++)
        {
            if (i < currentBullet)
            {
                _snowBallImages[i].enabled = true;
            }
            else
            {
                _snowBallImages[i].enabled = false;
            }
        }
    }

    public void Reload()
    {
        _crossheir.enabled = !_crossheir.enabled;
        _reloadText.gameObject.SetActive(!_crossheir.enabled);
    }
}
