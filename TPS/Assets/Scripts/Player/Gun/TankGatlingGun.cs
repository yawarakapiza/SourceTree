using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankGatlingGun : Player
{
    public GameObject BulletPrefab;
    public GameObject bulletSpawnPoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;
    public Vector3 mouseOffset = new Vector3(20, 20, 0);
    public float spreadAngle = 5f;
    public Transform go_barrel;
    public float barrelRotationSpeed;
    public ParticleSystem muzzelFlash;

    public int maxAmmo = 100; // 最大弾薬数
    private int currentAmmo; // 現在の弾薬数

    public Text ammoText; // 弾薬数を表示するUIText

    private float nextFireTime = 0f;
    private float currentRotationSpeed = 0f;

    void Start()
    {
        currentAmmo = maxAmmo; // 弾薬を最大に設定
        UpdateAmmoText(); // 弾薬数を表示
    }

    void OnEnable()
    {
        // マウスボタンが押されているか確認して、エフェクトを適切に制御
        CheckAndHandleFiringOnActivation();
    }

    void CheckAndHandleFiringOnActivation()
    {
        // オブジェクトがアクティブになるときにマウスボタンの状態をチェック
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            // 弾薬があればエフェクトを再生
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            // マウスボタンが押されていないか、弾薬がなければエフェクトを停止
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }

    protected override void Update()
    {
        // Gunオブジェクトの方向を更新する
        Vector3 mousePosition = Input.mousePosition + mouseOffset;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        worldMousePosition.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(worldMousePosition - transform.position);

        HandleBarrelRotation();
        HandleFiring();
    }

    void HandleBarrelRotation()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            go_barrel.transform.Rotate(0, 0, barrelRotationSpeed * Time.deltaTime);
        }
        else
        {
            go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);
        }
    }

    void HandleFiring()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            FireGun();
        }
        else if (!Input.GetMouseButton(0) && muzzelFlash.isPlaying)
        {
            muzzelFlash.Stop();
        }
    }

    void FireGun()
    {
        Vector3 spawnPoint = bulletSpawnPoint.transform.position;
        Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
        Quaternion bulletRotation = transform.rotation * spreadRotation;
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint, bulletRotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
            AudioManager.Instance.PlaySE(SESoundData.SE.Gun2);
        }

        // 弾薬を消費
        currentAmmo--;
        Debug.Log("Ammo left: " + currentAmmo);

        // 弾薬数を更新
        UpdateAmmoText();

        // マズルフラッシュを再生
        if (!muzzelFlash.isPlaying && currentAmmo > 0)
        {
            muzzelFlash.Play();
        }

        // 次の発射可能時間を更新
        nextFireTime = Time.time + 1f / fireRate;

        // 弾薬が切れたらエフェクトとアニメーションの停止
        if (currentAmmo <= 0)
        {
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
            currentRotationSpeed = 0; // バレルの回転速度を0に設定
        }
    }

    // 弾薬数を更新するメソッド
    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }

    // 弾薬を追加するメソッド
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoText();
    }
}
