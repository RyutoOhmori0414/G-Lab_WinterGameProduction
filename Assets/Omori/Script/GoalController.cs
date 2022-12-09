using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    GameController _gameController;
    [Tooltip("どのチームのゴールか"), SerializeField]
    GameController.Team team;
    

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当たった自軍のプレイヤーが旗を持っていたら勝ち
        PlayerController temp = other.gameObject.GetComponent<PlayerController>();

        if (temp?.CurrentPlayerState == PlayerController.PlayerState.isFlag && 
            temp?.PlayerTeam == team)
        {
            _gameController.GameEnd(team);
        }
    }
}
