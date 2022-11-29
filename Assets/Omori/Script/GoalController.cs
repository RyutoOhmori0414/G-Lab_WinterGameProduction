using System.Collections;
using System.Collections.Generic;
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
        // �����������R�̃v���C���[�����������Ă����珟��
        if (other.gameObject /* && ���R�̃v���C���[���H && ���������Ă��邩�H*/)
        {
            _gameController.GameEnd(team);
        }
    }
}
