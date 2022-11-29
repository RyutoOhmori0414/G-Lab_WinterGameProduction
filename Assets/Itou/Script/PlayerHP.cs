using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerRespawn))]
public class PlayerHP : MonoBehaviour
{
    [Header("�̗͊֘A")]
    [SerializeField] private int _hp = 3;
    [SerializeField] private int _hpMax = 3;
    [SerializeField] private int _hpMaxDefault = 3;
    [SerializeField] private int _hpMaxPowerUp = 4;
    [SerializeField] private List<Image> _hpImage = new();
    [SerializeField] private string _hPUpTag = "HPUp";//trygetcomponent���Ă���Ă�����
    [SerializeField] string _bulletTag = "Bullet";
    [SerializeField] PlayerRespawn _playerRespawn;

    public int Hp { get => _hp; set => _hp = value; }
    public int HpMax { get => _hpMax; set => _hpMax = value; }
    public int HpMaxDefault { get => _hpMaxDefault; set => _hpMaxDefault = value; }
    public int HpMaxPowerUp { get => _hpMaxPowerUp; set => _hpMaxPowerUp = value; }
    public List<Image> HpImage { get => _hpImage; set => _hpImage = value; }
    public string HPUpTag { get => _hPUpTag; set => _hPUpTag = value; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aa");
        ///�e�ɓ���������
        if (other.tag == _bulletTag)
        {
            Debug.Log("bb");

            //�̗͂�UI���D�F�ɂ���
            _hpImage[_hp - 1].gameObject.GetComponent<Image>().color = Color.gray;
            //�̗͂����炷
            _hp--;
            //�̗͂Ȃ��Ȃ����珈��������
            if (_hp <= 0)
            {
                StartCoroutine(_playerRespawn.PlayerDeath());
            }
        }
        ///�̗͑����A�C�e���ɓ���������
        if (other.tag == _hPUpTag)
        {
            if (_hpMax != _hpMaxPowerUp)
            {
                _hpMax = _hpMaxPowerUp;
                _hpImage[_hpMax - 1].gameObject.GetComponent<Image>().color = Color.gray;//�̗͈���������Ȃ��Ƃ��ɂ������܂��쓮���Ȃ�
            }   
            if (_hp < _hpMax)
            {
                _hp++;
                _hpImage[_hp - 1].gameObject.GetComponent<Image>().color = Color.white;//�̗͈���������Ȃ��Ƃ��ɂ������܂��쓮���Ȃ�
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerRespawn = GetComponent<PlayerRespawn>();
        _hp = _hpMax;
    }
}
