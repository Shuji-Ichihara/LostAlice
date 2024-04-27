using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _playerJunp = 10f;

    private Rigidbody _rb;

    [SerializeField]
    private Animator _anim;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // プレイヤーの移動処理
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * verticalInput + right * horizontalInput) * _playerSpeed * Time.deltaTime;

        transform.Translate(movement);

        // アニメーション処理
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {     
            _anim.SetBool("Walk",true);
        }
        else
        {
            _anim.SetBool("Walk", false);
        }

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _anim.SetTrigger("Junp");
            _rb.AddForce(Vector3.up * _playerJunp, ForceMode.Impulse);
        }
       
    }
}
