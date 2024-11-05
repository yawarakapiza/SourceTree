using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankCannon : Player
{
    public GameObject BulletPrefab; // ��������e�I�u�W�F�N�g�̃v���n�u
    public GameObject bulletSpawnPoint; // �e�𐶐�����ʒu�̃I�u�W�F�N�g
    public float fireRate = 0.5f; // ���˃��[�g�i1�b������̔��ː��j
    public float bulletSpeed = 10f; // �e�̑��x
    public Vector3 mouseOffset = new Vector3(20, 20, 0); // �}�E�X�ʒu����̃I�t�Z�b�g
    public float spreadAngle = 5f; // ���˂̂΂���p�x
    public int maxAmmo = 10; // �ő�e��
    private int currentAmmo; // ���݂̒e��

    public Text ammoText; // �e�򐔂�\������UIText

    private float nextFireTime = 0f; // ���ɔ��ˉ\�Ȏ���

    void Start()
    {
        currentAmmo = maxAmmo; // �e����ő�ɐݒ�
        UpdateAmmoText(); // �e�򐔂�\��
    }

    protected override void Update()
    {
        // Gun�I�u�W�F�N�g�̕������X�V����
        Vector3 mousePosition = Input.mousePosition + mouseOffset;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        worldMousePosition.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(worldMousePosition - transform.position);

        // �}�E�X�̍��{�^����������Ă��āA���ɔ��ˉ\�Ȏ��ԂɂȂ��Ă��邩�A�����Ēe�򂪎c���Ă��邩���m�F
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

        currentAmmo--; // �e��������
        nextFireTime = Time.time + 1f / fireRate; // ���̔��ˉ\���Ԃ�ݒ�
        UpdateAmmoText(); // �e�򐔂��X�V

        // �e�򂪂Ȃ��Ȃ������̏���
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
        }
    }

    // �e�򐔂��X�V���郁�\�b�h
    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo;
        }
    }

    // �e���ǉ����郁�\�b�h
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoText();
    }
}
