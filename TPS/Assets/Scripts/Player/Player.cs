using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public int health = 1;
    public float respawnTime = 3f;
    public Transform respawnPoint;
    public int maxRespawnCount = 3;
    private int respawnCount = 0;
    protected Rigidbody rb;
    private bool isDead = false;

    public GameObject barrierEffect;
    private GameObject activeBarrier;
    public bool barrierActive = false;
    public int barrierHealth = 3;
    public int maxBarrierHealth = 3;
    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    public Slider barrierHealthSlider; // バリア耐久値のスライダー
    public Vector3 barrierOffset = new Vector3(0f, 0f, 1f); // バリアのオフセット位置

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        // スライダーの初期値設定
        if (barrierHealthSlider != null)
        {
            barrierHealthSlider.maxValue = maxBarrierHealth;
            barrierHealthSlider.value = barrierHealth;
        }
    }

    protected virtual void Update()
    {
        if (isDead)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        rb.velocity = moveDirection * moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ActivateBarrier();
        }
        else
        {
            DeactivateBarrier();
        }
    }

    public void RecoverBarrierHealth(int amount)
    {
        barrierHealth = Mathf.Min(barrierHealth + amount, maxBarrierHealth);
        if (barrierHealth > 0 && activeBarrier == null)
        {
            ActivateBarrier();
        }

        // スライダーを更新
        UpdateBarrierHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        if (activeBarrier != null)
        {
            barrierHealth -= damage;
            if (barrierHealth <= 0)
            {
                barrierHealth = 0;
                DeactivateBarrier();
            }
        }
        else
        {
            health -= damage;
            if (health <= 0 && !isDead)
            {
                Die();
            }
        }

        // スライダーを更新
        UpdateBarrierHealthSlider();
    }

    protected virtual void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        respawnCount++;

        if (respawnCount <= maxRespawnCount)
        {
            RespawnManager.Instance.StartRespawnCoroutine(this, respawnTime);
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        health = 1;
        isDead = false;
        gameObject.SetActive(true);
        barrierHealth = maxBarrierHealth;
        StartCoroutine(InvincibilityCoroutine());

        // スライダーを更新
        UpdateBarrierHealthSlider();
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void ActivateBarrier()
    {
        if (activeBarrier == null && barrierEffect != null && barrierHealth > 0)
        {
            barrierActive = true;
            // プレイヤーの位置にオフセットを追加してバリアを生成
            Vector3 barrierPosition = transform.position + barrierOffset;
            activeBarrier = Instantiate(barrierEffect, barrierPosition, Quaternion.identity, transform);
        }
    }

    private void DeactivateBarrier()
    {
        if (activeBarrier != null)
        {
            barrierActive = false;
            Destroy(activeBarrier);
            activeBarrier = null;
        }
    }

    private void UpdateBarrierHealthSlider()
    {
        if (barrierHealthSlider != null)
        {
            barrierHealthSlider.value = barrierHealth;
        }
    }
}
