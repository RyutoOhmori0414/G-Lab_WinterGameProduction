using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Tooltip("HPのイメージ"), SerializeField]
    Image[] _hpImages;
    [Tooltip("雪玉のイメージ"), SerializeField]
    Image[] _snowBallImages;
    [Tooltip("照準"), SerializeField]
    Image _crossheir;
    [Tooltip("リロード中のテキスト"), SerializeField]
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
