using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSnowBallController : MonoBehaviour
{
    [Tooltip("ボールのスピード"), SerializeField]
    float _ballSpeed = 10f;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = this.transform.forward * _ballSpeed;
        Destroy(this.gameObject, 3f);
    }

    
}
