using UnityEngine;

public class EnemyRobot2 : Enemy
{
    public float moveSpeed = 5f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;

    protected override void EngagePlayer()
    {
        // プレイヤーオブジェクトが存在するか確認
        if (player != null)
        {
            // プレイヤーの方向に移動する
            // プレイヤーの方向を向く（上下の方向は無視）
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0f; // 上下方向を無視
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            // プレイヤーに衝突した場合の処理
            if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        // 爆発ダメージを与える
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Bomb!");
                col.GetComponent<Player>().TakeDamage(explosionDamage);
            }
        }

        // エフェクトやサウンドを再生（省略）

        // 自分自身を破壊
        Destroy(gameObject);
    }
}
