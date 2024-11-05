using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float detectionRange = 15f;
    public int health = 100;
    public GameObject[] ammoDropPrefabs; // 弾薬ドロップのPrefabの配列
    public float[] dropProbabilities; // 各アイテムのドロップ確率の配列
    public GameObject explosionEffectPrefab; // 爆発エフェクトのPrefab

    protected GameObject player;
    private float nextFireTime = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            EngagePlayer();
        }
    }

    protected abstract void EngagePlayer();

    protected void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if (player != null && bulletSpawnPoint != null)
            {
                // プレイヤーと弾の発射ポイントの水平面上の方向を計算
                Vector3 direction = player.transform.position - bulletSpawnPoint.position;
                direction.y = 0; // 縦方向の成分を無視して水平面だけにする
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


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // エネミーの死亡処理など
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity); // 爆発エフェクトを再生
            AudioManager.Instance.PlaySE(SESoundData.SE.Exp01);
        }

        if (ammoDropPrefabs != null && ammoDropPrefabs.Length > 0 && dropProbabilities != null && dropProbabilities.Length == ammoDropPrefabs.Length)
        {
            // ドロップアイテムを選択
            GameObject selectedDrop = ChooseRandomDrop();
            if (selectedDrop != null)
            {
                Instantiate(selectedDrop, transform.position, Quaternion.identity); // 弾薬をドロップ
            }
        }
        Destroy(gameObject);
    }

    private GameObject ChooseRandomDrop()
    {
        float totalProbability = 0f;
        foreach (float probability in dropProbabilities)
        {
            totalProbability += probability;
        }

        float randomValue = Random.value * totalProbability;

        for (int i = 0; i < dropProbabilities.Length; i++)
        {
            if (randomValue <= dropProbabilities[i])
            {
                return ammoDropPrefabs[i];
            }
            randomValue -= dropProbabilities[i];
        }

        return null;
    }
}