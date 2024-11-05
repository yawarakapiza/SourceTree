using System.Collections; // IEnumerator を使うために必要
using UnityEngine;

public class EnemyBoss1 : MonoBehaviour
{
    public int health = 100;
    private GameObject player;
    public Transform turret; // 主砲のTransform
    public GameObject explosionEffectPrefab; // 爆発エフェクトのPrefab
    public GameObject beamEffectPrefab; // ビームエフェクトのPrefab
    public Transform shotPoint; // ビームを発射するポイント
    public float fireInterval = 0.5f; // ビームを発射する間隔
    public float sweepAngle = 45f; // ビームの薙ぎ払い角度
    public float attackDuration = 2f; // 攻撃の継続時間（秒）
    public float attackCycleTime = 10f; // 攻撃サイクルの時間（秒）

    public float sweepStartAngle = -45f; // インスペクターで設定できる開始角度
    public float sweepEndAngle = 45f; // インスペクターで設定できる終了角度

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
        // プレイヤーの方向を向かない
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
            turret.rotation = Quaternion.Euler(-90f, sweepStartAngle, 0f); // 最初の角度にリセット
        }
    }

    private IEnumerator SweepBeam(float startAngle, float endAngle)
    {
        float elapsedTime = 0f;
        while (elapsedTime < attackDuration)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / attackDuration);
            turret.rotation = Quaternion.Euler(-90f, currentAngle, 0f); // 縦方向は無視

            // 一定間隔でビームを発射する
            if (Time.time >= nextFireTime)
            {
                FireBeam();
                nextFireTime = Time.time + fireInterval;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 攻撃終了時の最終角度に設定
        turret.rotation = Quaternion.Euler(-90f, endAngle, 0f);
    }

    void FireBeam()
    {
        if (beamEffectPrefab != null && shotPoint != null)
        {
            GameObject beam = Instantiate(beamEffectPrefab, shotPoint.position, Quaternion.identity);

            // ビームの向きを設定
            Quaternion targetRotation = Quaternion.Euler(0f, turret.eulerAngles.y, 0f);
            beam.transform.rotation = targetRotation;

            beam.transform.SetParent(shotPoint);
            beam.transform.localScale = Vector3.one;

            // ビームエフェクトのスクリプトで寿命を設定
            BeamCollision beamEffect = beam.GetComponent<BeamCollision>();
            if (beamEffect != null)
            {
                beamEffect.lifetime = fireInterval; // ビームの寿命を発射間隔に合わせる
            }
        }
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            StartAttack();
            yield return new WaitForSeconds(attackDuration); // 攻撃の継続時間を待つ
            yield return new WaitForSeconds(attackCycleTime); // 攻撃サイクルの間隔を待つ
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
