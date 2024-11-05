using UnityEngine;

public class EnemyRobot1 : Enemy
{
    protected override void EngagePlayer()
    {
        // �v���C���[�̕����������i�㉺�̕����͖����j
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0f; // �㉺�����𖳎�
        transform.rotation = Quaternion.LookRotation(direction);

        // �ˌ�
        Fire();
    }
}
