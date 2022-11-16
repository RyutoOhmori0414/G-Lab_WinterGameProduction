using UnityEngine;

public class PadPlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    Rigidbody _rb = default;
    [SerializeField] Camera camera;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //float camX = Input.GetAxisRaw("Mouse X");
        //float camY = Input.GetAxisRaw("Mouse Y");
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        //camera.transform.Rotate(0, camX, 0);
        // �J�����̃��[�J�����W�n����� dir ��ϊ�����
        dir = camera.transform.TransformDirection(dir);
        // �J�����͎΂߉��Ɍ����Ă���̂ŁAY ���̒l�� 0 �ɂ��āuXZ ���ʏ�̃x�N�g���v�ɂ���
        dir.y = 0;
        // �ړ��̓��͂��Ȃ����͉�]�����Ȃ��B���͂����鎞�͂��̕����ɃL�����N�^�[��������B
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;
    }
}