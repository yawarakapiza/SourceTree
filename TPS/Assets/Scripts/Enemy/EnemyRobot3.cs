using UnityEngine;

public class EnemyRobot3 : Enemy
{
    public float moveSpeed = 5f;
    public float fireInterval = 2f; // �e�𔭎˂���Ԋu

    private float lastFireTime = 0f;

    protected override void EngagePlayer()
    {
        // �O���Ɍ������Đi��
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // ���̊Ԋu�Ńv���C���[�̕����ɒe�𔭎�
        if (Time.time >= lastFireTime + fireInterval)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
