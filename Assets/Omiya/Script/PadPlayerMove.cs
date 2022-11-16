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
        // カメラのローカル座標系を基準に dir を変換する
        dir = camera.transform.TransformDirection(dir);
        // カメラは斜め下に向いているので、Y 軸の値を 0 にして「XZ 平面上のベクトル」にする
        dir.y = 0;
        // 移動の入力がない時は回転させない。入力がある時はその方向にキャラクターを向ける。
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed;
    }
}