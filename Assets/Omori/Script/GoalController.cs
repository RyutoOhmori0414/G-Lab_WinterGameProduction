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
        if (other.gameObject /* && 自軍のプレイヤーか？ && 旗を持っているか？*/)
        {
            _gameController.GameEnd(team);
        }
    }
}
