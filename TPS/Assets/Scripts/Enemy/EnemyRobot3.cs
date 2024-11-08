using UnityEngine;

public class EnemyRobot3 : Enemy
{
    public float moveSpeed = 5f;
    public float fireInterval = 2f; // 弾を発射する間隔

    private float lastFireTime = 0f;

    protected override void EngagePlayer()
    {
        // 前方に向かって進む
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // 一定の間隔でプレイヤーの方向に弾を発射
        if (Time.time >= lastFireTime + fireInterval)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
