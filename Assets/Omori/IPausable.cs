using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�E���g�_�E���Ȃǂœ������~�߂Ă����Ƃ��ɍs���������������Ă�������
/// </summary>
public interface IPausable
{
    /// <summary>
    /// Pause �������~�߂鏈���������Ă�������
    /// </summary>
    public void Pause();
    /// <summary>
    /// Pause���������鏈��
    /// </summary>
    public void Awake();
}
