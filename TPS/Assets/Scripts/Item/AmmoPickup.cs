using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int gatlingAmmoAmount = 50; // GatlingGun�̕�[����e��̗�
    public int cannonAmmoAmount = 30; // Cannon�̕�[����e��̗�
    public int barrierHealthRecoveryAmount = 3; // �o���A�̑ϋv�l�̉񕜗�

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ammo Pickup Triggered by: " + other.name); // �f�o�b�O���O��ǉ�
        if (other.CompareTag("Player"))
        {
            // Player�Ɋ֘A�t����ꂽGatlingGun�܂���Cannon��T��
            TankGatlingGun playerGatlingGun = other.GetComponentInChildren<TankGatlingGun>();
            TankCannon playerCannon = other.GetComponentInChildren<TankCannon>();
            Player player = other.GetComponent<Player>();

            // �Ή����镐�킪���邩�`�F�b�N���AammoAmount�̒e����[����
            if (playerGatlingGun != null)
            {
                playerGatlingGun.AddAmmo(gatlingAmmoAmount);
                Debug.Log("Gatling Gun Ammo picked up!"); // �f�o�b�O���O��ǉ�
            }

            if (playerCannon != null)
            {
                playerCannon.AddAmmo(cannonAmmoAmount);
                Debug.Log("Cannon Ammo picked up!"); // �f�o�b�O���O��ǉ�
            }

            // �v���C���[�̃o���A�ϋv�l���񕜂���
            if (player != null)
            {
                player.RecoverBarrierHealth(barrierHealthRecoveryAmount);
                Debug.Log("Barrier health recovered!"); // �f�o�b�O���O��ǉ�
            }

            Destroy(gameObject); // �e��A�C�e����j��
        }
    }
}
