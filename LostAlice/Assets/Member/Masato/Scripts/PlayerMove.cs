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

    //x軸方向の入力を保存
    private float _input_x;
    //z軸方向の入力を保存
    private float _input_z;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // x軸方向、z軸方向の入力を取得
        //Horizontal、水平、横方向のイメージ
        _input_x = Input.GetAxis("Horizontal");
        //Vertical、垂直、縦方向のイメージ
        _input_z = Input.GetAxis("Vertical");

        //移動の向きなど座標関連はVector3で扱う
        Vector3 velocity = new Vector3(_input_x, 0, _input_z);
        //ベクトルの向きを取得
        Vector3 direction = velocity.normalized;

        //移動距離を計算
        float distance = _playerSpeed * Time.deltaTime;
        //移動先を計算
        Vector3 destination = transform.position + direction * distance;

        //移動先に向けて回転
        transform.LookAt(destination);
        //移動先の座標を設定
        transform.position = destination;

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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("aaa");
            Destroy(this.gameObject);
        }
    }
}
