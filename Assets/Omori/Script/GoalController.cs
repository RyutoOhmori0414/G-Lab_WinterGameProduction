using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    GameController _gameController;
    [Tooltip("�ǂ̃`�[���̃S�[����"), SerializeField]
    GameController.Team team;
    

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name}��{this.gameObject.name}�ƐڐG���܂���");
        // �����������R�̃v���C���[�����������Ă����珟��
        PlayerController temp = other.gameObject.GetComponent<PlayerController>();

        if (temp?.CurrentPlayerState == PlayerController.PlayerState.isFlag && 
            temp?.PlayerTeam != team)
        {
            _gameController.GameEnd(temp.PlayerTeam);
            Debug.Log("���������đ���̃S�[���ɓ��B���܂����B");
        }
    }
}
