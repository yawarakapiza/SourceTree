using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float rollSpeed = 5f; // 転がる速度

    private Rigidbody rb; // Rigidbody コンポーネントの参照

    void Start()
    {
        // Rigidbody コンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // キー入力を取得して転がるベクトルを計算
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // 転がる方向に速度を加える
        rb.AddForce(moveDirection * rollSpeed);
    }
}
