using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int gatlingAmmoAmount = 50; // GatlingGunの補充する弾薬の量
    public int cannonAmmoAmount = 30; // Cannonの補充する弾薬の量
    public int barrierHealthRecoveryAmount = 3; // バリアの耐久値の回復量

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ammo Pickup Triggered by: " + other.name); // デバッグログを追加
        if (other.CompareTag("Player"))
        {
            // Playerに関連付けられたGatlingGunまたはCannonを探す
            TankGatlingGun playerGatlingGun = other.GetComponentInChildren<TankGatlingGun>();
            TankCannon playerCannon = other.GetComponentInChildren<TankCannon>();
            Player player = other.GetComponent<Player>();

            // 対応する武器があるかチェックし、ammoAmountの弾薬を補充する
            if (playerGatlingGun != null)
            {
                playerGatlingGun.AddAmmo(gatlingAmmoAmount);
                Debug.Log("Gatling Gun Ammo picked up!"); // デバッグログを追加
            }

            if (playerCannon != null)
            {
                playerCannon.AddAmmo(cannonAmmoAmount);
                Debug.Log("Cannon Ammo picked up!"); // デバッグログを追加
            }

            // プレイヤーのバリア耐久値を回復する
            if (player != null)
            {
                player.RecoverBarrierHealth(barrierHealthRecoveryAmount);
                Debug.Log("Barrier health recovered!"); // デバッグログを追加
            }

            Destroy(gameObject); // 弾薬アイテムを破壊
        }
    }
}
