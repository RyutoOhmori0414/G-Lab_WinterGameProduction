using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guruguru : MonoBehaviour
{
    [SerializeField] float _kaitenSpeed;
    void Update()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.up);// ���݂̎��M�̉�]�̏����擾����B
        Quaternion q = gameObject.transform.rotation;// �������āA���g�ɐݒ�
        gameObject.transform.rotation = q * rot;
    }
}
