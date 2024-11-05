using UnityEngine;

public class BossLittleCannon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float detectionRange = 15f;
    public float rotationSpeed = 5f;
    public GameObject explosionEffectPrefab;

    private GameObject player;
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                EngagePlayer(); // プレイヤーを向く
                Fire(); // 弾を撃つ
            }
        }
    }

    // プレイヤーを向く
    void EngagePlayer()
    {
        // プレイヤーとの方向を計算
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // Y軸方向を無視

        // プレイヤーに向かって回転するが、角度を上向きに90度に調整
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(-90f, 0f, 0f);  //

        // 現在の回転から目標回転へスムーズに補間
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    // 弾を撃つ
    void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if (bulletSpawnPoint != null && bulletPrefab != null)
            {
                Vector3 direction = player.transform.position - bulletSpawnPoint.position;
                direction.y = 0; // 水平面だけにする
                direction.Normalize();

                Quaternion bulletRotation = Quaternion.LookRotation(direction);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
                }

                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // ダメージを受けたときの処理
    public void TakeDamage(int damage)
    {
        // ダメージ処理
    }

    // 死亡時の処理
    void Die()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
