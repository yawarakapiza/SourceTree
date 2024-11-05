using UnityEngine;

public class EnemyRobot2 : Enemy
{
    public float moveSpeed = 5f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;

    protected override void EngagePlayer()
    {
        // �v���C���[�I�u�W�F�N�g�����݂��邩�m�F
        if (player != null)
        {
            // �v���C���[�̕����Ɉړ�����
            // �v���C���[�̕����������i�㉺�̕����͖����j
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0f; // �㉺�����𖳎�
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            // �v���C���[�ɏՓ˂����ꍇ�̏���
            if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        // �����_���[�W��^����
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Bomb!");
                col.GetComponent<Player>().TakeDamage(explosionDamage);
            }
        }

        // �G�t�F�N�g��T�E���h���Đ��i�ȗ��j

        // �������g��j��
        Destroy(gameObject);
    }
}
