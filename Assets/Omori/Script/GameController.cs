using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>�Q�[�����J�n���ꂽ�Ƃ��ɓ���Action</summary>
    public static Action OnGameStart;

    /// <summary>�Q�[�����I�������Ƃ��ɓ���Action</summary>
    public static Action OnGameEnd;

    [Tooltip("�v���C���Ԃ̐�������"), SerializeField]
    float _timeLimit = default;
    [Tooltip("�Q�[�����n�܂�܂ł̗P�\����"), SerializeField]
    float _setUpTime = default;

    float _timer = default;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _setUpTime)
        {
            // �ŏ��̑҂����Ԃ��I����Ă���s������
            
        }
        if (_timer > _timeLimit)
        {
            // �^�C�����~�b�g���߂�����s������
        }

    }
}
