using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float rollSpeed = 5f; // �]���鑬�x

    private Rigidbody rb; // Rigidbody �R���|�[�l���g�̎Q��

    void Start()
    {
        // Rigidbody �R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �L�[���͂��擾���ē]����x�N�g�����v�Z
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // �]��������ɑ��x��������
        rb.AddForce(moveDirection * rollSpeed);
    }
}
