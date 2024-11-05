using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float detectionRange = 15f;
    public int health = 100;
    public GameObject[] ammoDropPrefabs; // �e��h���b�v��Prefab�̔z��
    public float[] dropProbabilities; // �e�A�C�e���̃h���b�v�m���̔z��
    public GameObject explosionEffectPrefab; // �����G�t�F�N�g��Prefab

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
                // �v���C���[�ƒe�̔��˃|�C���g�̐����ʏ�̕������v�Z
                Vector3 direction = player.transform.position - bulletSpawnPoint.position;
                direction.y = 0; // �c�����̐����𖳎����Đ����ʂ����ɂ���
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
        // �G�l�~�[�̎��S�����Ȃ�
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity); // �����G�t�F�N�g���Đ�
            AudioManager.Instance.PlaySE(SESoundData.SE.Exp01);
        }

        if (ammoDropPrefabs != null && ammoDropPrefabs.Length > 0 && dropProbabilities != null && dropProbabilities.Length == ammoDropPrefabs.Length)
        {
            // �h���b�v�A�C�e����I��
            GameObject selectedDrop = ChooseRandomDrop();
            if (selectedDrop != null)
            {
                Instantiate(selectedDrop, transform.position, Quaternion.identity); // �e����h���b�v
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