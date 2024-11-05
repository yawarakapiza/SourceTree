using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyDelay = 2f; // �����܂ł̑ҋ@���ԁi�b�j
    public int damage = 10; // �e�̃_���[�W��

    void Start()
    {
        // �w��b���Destroy���\�b�h���Ăяo���Ď������g�i����Bullet�I�u�W�F�N�g�j����������
        Destroy(gameObject, destroyDelay);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            // �e���G�l�~�[�ɓ��������ꍇ�A�G�l�~�[�Ƀ_���[�W��^����
            other.GetComponent<Enemy>().TakeDamage(damage);
            // �e�̏�������
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            Debug.Log("hit");
            // �e���G�l�~�[�ɓ��������ꍇ�A�G�l�~�[�Ƀ_���[�W��^����
            other.GetComponent<EnemyBoss1>().TakeDamage(damage);
            // �e�̏�������
            Destroy(gameObject);
        }
    }
}
