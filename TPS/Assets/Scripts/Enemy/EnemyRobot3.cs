using UnityEngine;

public class EnemyRobot3 : Enemy
{
    public float moveSpeed = 5f;
    public float fireInterval = 2f; // ’e‚ð”­ŽË‚·‚éŠÔŠu

    private float lastFireTime = 0f;

    protected override void EngagePlayer()
    {
        // ‘O•û‚ÉŒü‚©‚Á‚Äi‚Þ
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // ˆê’è‚ÌŠÔŠu‚ÅƒvƒŒƒCƒ„[‚Ì•ûŒü‚É’e‚ð”­ŽË
        if (Time.time >= lastFireTime + fireInterval)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
}
