using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankCannon : Player
{
    public GameObject BulletPrefab; // 生成する弾オブジェクトのプレハブ
    public GameObject bulletSpawnPoint; // 弾を生成する位置のオブジェクト
    public float fireRate = 0.5f; // 発射レート（1秒あたりの発射数）
    public float bulletSpeed = 10f; // 弾の速度
    public Vector3 mouseOffset = new Vector3(20, 20, 0); // マウス位置からのオフセット
    public float spreadAngle = 5f; // 発射のばらつき角度
    public int maxAmmo = 10; // 最大弾薬数
    private int currentAmmo; // 現在の弾薬数

    public Text ammoText; // 弾薬数を表示するUIText

    private float nextFireTime = 0f; // 次に発射可能な時間

    void Start()
    {
        currentAmmo = maxAmmo; // 弾薬を最大に設定
        UpdateAmmoText(); // 弾薬数を表示
    }

    protected override void Update()
    {
        // Gunオブジェクトの方向を更新する
        Vector3 mousePosition = Input.mousePosition + mouseOffset;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        worldMousePosition.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(worldMousePosition - transform.position);

        // マウスの左ボタンが押されていて、次に発射可能な時間になっているか、そして弾薬が残っているかを確認
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            FireCannon();
        }
    }

    void FireCannon()
    {
        Vector3 spawnPoint = bulletSpawnPoint.transform.position;
        Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
        Quaternion bulletRotation = transform.rotation * spreadRotation;
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint, bulletRotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
            AudioManager.Instance.PlaySE(SESoundData.SE.Gun1);
        }

        currentAmmo--; // 弾薬を一つ消費
        nextFireTime = Time.time + 1f / fireRate; // 次の発射可能時間を設定
        UpdateAmmoText(); // 弾薬数を更新

        // 弾薬がなくなった時の処理
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
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
