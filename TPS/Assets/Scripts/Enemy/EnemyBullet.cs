using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float destroyDelay = 2f; // �����܂ł̑ҋ@���ԁi�b�j
    public int damage = 1; // �e�̃_���[�W��

    void Start()
    {
        // �w��b���Destroy���\�b�h���Ăяo���Ď������g�i����Bullet�I�u�W�F�N�g�j����������
        Destroy(gameObject, destroyDelay);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            // �e��Player�ɓ��������ꍇ�APlayer�Ƀ_���[�W��^����
            other.GetComponent<Player>().TakeDamage(damage);
            // �e�̏�������
            Destroy(gameObject);
        }
    }
}