using UnityEngine;

public class EnemyRobot4 : Enemy
{
    public float approachSpeed = 3f; // プレイヤーに近づく速度
    public float maintainDistance = 5f; // プレイヤーとの維持する距離
    public float swayDistance = 1f; // 揺れる距離
    public float swaySpeed = 2f; // 揺れる速度
    public float fireInterval = 2f; // 弾を発射する間隔

    private float lastFireTime = 0f;
    private Vector3 initialPosition;

    protected override void EngagePlayer()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // プレイヤーの方向を向く
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * approachSpeed);

        // プレイヤーに近づく
        if (distanceToPlayer > maintainDistance)
        {
            transform.Translate(directionToPlayer * approachSpeed * Time.deltaTime, Space.World);
            initialPosition = transform.position; // 初期位置を記憶
        }
        else
        {
            // 一定距離を維持している間、左右に揺れる
            float swayAmount = Mathf.Sin(Time.time * swaySpeed) * swayDistance;

            // 左右に揺れる
            Vector3 swayPosition = initialPosition + transform.right * swayAmount;
            transform.position = new Vector3(swayPosition.x, transform.position.y, swayPosition.z);
        }

        // 一定の間隔で射撃する
        if (Time.time >= lastFireTime + fireInterval && distanceToPlayer <= maintainDistance)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
