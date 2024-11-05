using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    public float lifetime = 2f; // �G�t�F�N�g�̎����i�b�j
    public int damage = 1; // �r�[���̃_���[�W��
    private Collider beamCollider;

    private void Start()
    {
        // �G�t�F�N�g�̎������I�������玩���ō폜����
        Destroy(gameObject, lifetime);
        beamCollider = GetComponent<Collider>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        // �r�[�������������ꍇ�ɒ��˕Ԃ�Ȃ��悤�ɁA���̃R���W�����𖳎�����
        if (beamCollider != null && other.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(beamCollider, other.GetComponent<Collider>(), true);
        }
    }
}
