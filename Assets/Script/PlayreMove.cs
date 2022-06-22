using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayreMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 7.5f;
    Vector3 dir;
    Rigidbody _rb = default;
    Animator _anim = default;
    bool _canMove = true;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();

        }
    }

    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
        }
    }

    void Move()
    {
        // 入力を受け付ける
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 入力された方向を「カメラを基準とした XZ 平面上のベクトル」に変換する
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // キャラクターを「入力された方向」に向ける
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        // Y 軸方向の速度を保ちながら、速度ベクトルを求めてセットする
        Vector3 velocity = dir.normalized * _moveSpeed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }

}
