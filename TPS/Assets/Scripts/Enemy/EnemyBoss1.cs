using System.Collections; // IEnumerator ���g�����߂ɕK�v
using UnityEngine;

public class EnemyBoss1 : MonoBehaviour
{
    public int health = 100;
    private GameObject player;
    public Transform turret; // ��C��Transform
    public GameObject explosionEffectPrefab; // �����G�t�F�N�g��Prefab
    public GameObject beamEffectPrefab; // �r�[���G�t�F�N�g��Prefab
    public Transform shotPoint; // �r�[���𔭎˂���|�C���g
    public float fireInterval = 0.5f; // �r�[���𔭎˂���Ԋu
    public float sweepAngle = 45f; // �r�[���̓ガ�����p�x
    public float attackDuration = 2f; // �U���̌p�����ԁi�b�j
    public float attackCycleTime = 10f; // �U���T�C�N���̎��ԁi�b�j

    public float sweepStartAngle = -45f; // �C���X�y�N�^�[�Őݒ�ł���J�n�p�x
    public float sweepEndAngle = 45f; // �C���X�y�N�^�[�Őݒ�ł���I���p�x

    private float nextFireTime = 0f;
    private bool isAttacking = false;
    private float attackStartTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AttackCycle());
    }

    private void Update()
    {
        if (isAttacking)
        {
            PerformAttack();
        }
    }

    void EngagePlayer()
    {
        // �v���C���[�̕����������Ȃ�
    }

    void StartAttack()
    {
        isAttacking = true;
        attackStartTime = Time.time;
        StartCoroutine(SweepBeam(sweepStartAngle, sweepEndAngle));
    }

    private void PerformAttack()
    {
        if (Time.time - attackStartTime >= attackDuration)
        {
            isAttacking = false;
            StopCoroutine("SweepBeam");
            turret.rotation = Quaternion.Euler(-90f, sweepStartAngle, 0f); // �ŏ��̊p�x�Ƀ��Z�b�g
        }
    }

    private IEnumerator SweepBeam(float startAngle, float endAngle)
    {
        float elapsedTime = 0f;
        while (elapsedTime < attackDuration)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / attackDuration);
            turret.rotation = Quaternion.Euler(-90f, currentAngle, 0f); // �c�����͖���

            // ���Ԋu�Ńr�[���𔭎˂���
            if (Time.time >= nextFireTime)
            {
                FireBeam();
                nextFireTime = Time.time + fireInterval;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �U���I�����̍ŏI�p�x�ɐݒ�
        turret.rotation = Quaternion.Euler(-90f, endAngle, 0f);
    }

    void FireBeam()
    {
        if (beamEffectPrefab != null && shotPoint != null)
        {
            GameObject beam = Instantiate(beamEffectPrefab, shotPoint.position, Quaternion.identity);

            // �r�[���̌�����ݒ�
            Quaternion targetRotation = Quaternion.Euler(0f, turret.eulerAngles.y, 0f);
            beam.transform.rotation = targetRotation;

            beam.transform.SetParent(shotPoint);
            beam.transform.localScale = Vector3.one;

            // �r�[���G�t�F�N�g�̃X�N���v�g�Ŏ�����ݒ�
            BeamCollision beamEffect = beam.GetComponent<BeamCollision>();
            if (beamEffect != null)
            {
                beamEffect.lifetime = fireInterval; // �r�[���̎����𔭎ˊԊu�ɍ��킹��
            }
        }
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            StartAttack();
            yield return new WaitForSeconds(attackDuration); // �U���̌p�����Ԃ�҂�
            yield return new WaitForSeconds(attackCycleTime); // �U���T�C�N���̊Ԋu��҂�
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

    private void Die()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
