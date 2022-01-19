using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float move_speed = 0.00000001f; //追跡スピード
    GameObject PlayerObject; // playerオブジェクトを受け取る器
    public Transform Player; // プレイヤーの座標情報などを受け取る器
    // 2Dリジッドボディを受け取る変数
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        // ここで2Dリジッドボディを受け取る。
        rb2D = GetComponent<Rigidbody2D>();
        // playerのGameObjectを探して取得
        PlayerObject = GameObject.FindWithTag("Player");
        // playerのTransform情報を取得
        Player = PlayerObject.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        // -----------------
        // 追跡
        // -----------------
        Vector2 e_pos = transform.position;  // 自分(敵キャラクタ)の世界座標
        Vector2 p_pos = Player.position;  // プレイヤーの世界座標

        // プレイヤーの方向に動くベクトル(move_speedで速度を調整)
        Vector2 force = (p_pos - e_pos) * move_speed;
        // じわじわ追跡
        rb2D.AddForce(force, ForceMode2D.Force);    }
}
