using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�ɓ���������
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[�ɂ��Ă���
            // �������́A���ꂽ�炢������Active��False�ɂ��Ă��̃L�������j�󂳂ꂽ�Ƃ��ɂ�����Position���f��

        }
    }
}
