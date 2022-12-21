using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSnowBallController : MonoBehaviour
{
    [Tooltip("ボールのスピード"), SerializeField]
    float _ballSpeed = 10f;
    [Tooltip("ボールの射出角度"), SerializeField]
    float _ballAngle = 15f;
    Rigidbody _rb;
    void Start()
    {
        transform.Rotate(new Vector3(-_ballAngle, 0f, 0));
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = this.transform.forward * _ballSpeed;
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
