using UnityEngine;

public class EnemyRobot1 : Enemy
{
    protected override void EngagePlayer()
    {
        // プレイヤーの方向を向く（上下の方向は無視）
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0f; // 上下方向を無視
        transform.rotation = Quaternion.LookRotation(direction);

        // 射撃
        Fire();
    }
}
