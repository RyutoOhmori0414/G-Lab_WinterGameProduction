using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("�v���C���Ԃ̐�������"), SerializeField]
    float _timeLimit = default;
    [Tooltip("�Q�[�����n�܂�܂ł̗P�\����"), SerializeField]
    float _setUpTime = default;
    [Tooltip("����Transform"), SerializeField]
    Transform _flagTransform;
    [Tooltip("A�`�[���̐w�n��Transform"), SerializeField]
    Transform _teamATransform;
    [Tooltip("B�`�[���̐w�n��Transform"), SerializeField]
    Transform _teamBTransform;
    [Tooltip("Flag��prefab"), SerializeField]
    GameObject _flagPrefab;
    [Tooltip("Flag�̃��X�|�[���ʒu"), SerializeField]
    Transform _responeTramsform;

    MainUIController _mainUIController;
    float _timer = default;
    /// <summary>Timer�������Ă��邩�ǂ���</summary>
    bool _running = false;
    List<IPausable> _pausables = new();
    bool _gameStart = true;

    private void Start()
    {
        _running = true;
        // IPausable���������Ă���GamaObject��T���čŏ��Ƀ|�[�Y�������Ă���
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        _mainUIController = FindObjectOfType<MainUIController>();

        foreach(var n in gameObjects)
        {
            var temp = n.GetComponent<IPausable>();
            
            if (temp != null)
            {
                temp.Pause();
                _pausables.Add(temp);
            }
        }
    }

    private void Update()
    {
        if (_running)
        {
            Cursor.visible = false;
            _timer += Time.deltaTime;
            if (_timer < _setUpTime)
            {
                _mainUIController.CountTextUpdate(_setUpTime - _timer);
            }
            else if (_timer > _setUpTime && _gameStart)
            {
                // �ŏ��̑҂����Ԃ��I����Ă���s������
                // �J�E���g�_�E�����I�������|�[�Y����������
                foreach (var n in _pausables)
                {
                    n.Resume();
                }

                _mainUIController.CountEnd();
                _gameStart = false;
            } // �J�E���g�_�E�����I������ۂɈ�x�����s����
            else if (_timer > _timeLimit + _setUpTime)
            {
                // �^�C�����~�b�g���߂�����s������
                TimeOverGameEnd();
                _running = false;
                _mainUIController.SubCountEnd();
            }
            else
            {
                _mainUIController.SubCountTextUpdate(_timeLimit - (_timer - _setUpTime));
            } // �Q�[�����̏���
        }
        else
        {
            if (Input.anyKeyDown)
            {
                _mainUIController.ToTitle();
            }
        }

    }

    public void GameEnd(Team winTeam)
    {
        // �܂��|�[�Y��������ׂ��I�u�W�F�N�g���~�߂�
        foreach (var n in _pausables)
        {
            n.Pause();
        }
        _running = false;

        _mainUIController.EndGame(winTeam);
    }

    void TimeOverGameEnd()
    {
        // �܂��|�[�Y��������ׂ��I�u�W�F�N�g���~�߂�
        foreach(var n in _pausables)
        {
            n.Pause();
        }

        _mainUIController.DrawGame();
    }

    public void FlagRespone()
    {
        GameObject tempGO = Instantiate(_flagPrefab);
        tempGO.transform.position = _responeTramsform.position;
        _flagTransform = tempGO.transform;
    }

    public void GetFlag(Transform _playerTransform)
    {
        _flagTransform = _playerTransform;
    }

    public enum Team
    {
        ATeam,
        BTeam
    }
}
