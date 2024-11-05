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
                EngagePlayer(); // �v���C���[������
                Fire(); // �e������
            }
        }
    }

    // �v���C���[������
    void EngagePlayer()
    {
        // �v���C���[�Ƃ̕������v�Z
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // Y�������𖳎�

        // �v���C���[�Ɍ������ĉ�]���邪�A�p�x���������90�x�ɒ���
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(-90f, 0f, 0f);  //

        // ���݂̉�]����ڕW��]�փX���[�Y�ɕ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    // �e������
    void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if (bulletSpawnPoint != null && bulletPrefab != null)
            {
                Vector3 direction = player.transform.position - bulletSpawnPoint.position;
                direction.y = 0; // �����ʂ����ɂ���
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

    // �_���[�W���󂯂��Ƃ��̏���
    public void TakeDamage(int damage)
    {
        // �_���[�W����
    }

    // ���S���̏���
    void Die()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
