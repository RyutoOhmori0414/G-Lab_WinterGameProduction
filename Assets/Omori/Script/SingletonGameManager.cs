using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGameManager : MonoBehaviour
{
    // �V���O���g����GameManager�����
    public static SingletonGameManager Instance = default;



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
}
