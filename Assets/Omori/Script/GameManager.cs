using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �V���O���g����GameManager�����
    public static GameManager Instance = default;

    /// <summary>�Q�[�����J�n���ꂽ�Ƃ��ɓ���Action</summary>
    public static Action OnGameStart;

    /// <summary>�Q�[�����I�������Ƃ��ɓ���Action</summary>
    public static Action OnGameEnd;

    [Tooltip("�v���C���Ԃ̐�������"), SerializeField]
    float _timeLimit = default;

    float _timer = default;

    /// <summary>�Q�[���V�[���ŃQ�[���������Ă��邩�ǂ���</summary>
    bool _OnGame = false;

    private void Awake()
    {
        // �C���X�^���X�`�F�b�N
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        // Action�ɕK�v�Ȋ֐���o�^
        OnGameStart += GameStart;
        OnGameEnd += GameEnd;
    }

    private void Update()
    {
        if (_OnGame)
        {
            _timer += Time.deltaTime;

            if (_timer > _timeLimit)
            {
                // �^�C���I�[�o�[�����ۂɕK�v�ȏ���������
            }
        }
    }

    /// <summary>
    /// �Q�[�����n�܂����ۂɍs������
    /// </summary>
    void GameStart()
    {
        _OnGame = true;
    }

    /// <summary>
    /// �Q�[�����I�������ۂɍs������
    /// </summary>
    void GameEnd()
    {
        _OnGame = false;
    }

    private void OnDisable()
    {
        // Action�̓o�^������
        OnGameStart -= GameStart;
        OnGameEnd -= GameEnd;
    }
}
