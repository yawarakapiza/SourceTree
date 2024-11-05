using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankGun : Player
{
    public GameObject BulletPrefab; // ��������e�I�u�W�F�N�g�̃v���n�u
    public GameObject bulletSpawnPoint; // �e�𐶐�����ʒu�̃I�u�W�F�N�g
    public float fireRate = 0.5f; // ���˃��[�g
    public float bulletSpeed = 10f; // �e�̑��x
    public Vector3 mouseOffset = new Vector3(20, 20, 0); // �}�E�X�ʒu����̃I�t�Z�b�g
    public float spreadAngle = 5f; // ���˂̂΂���p�x
    public ParticleSystem muzzleFlash; // �}�Y���t���b�V���̃p�[�e�B�N���V�X�e��
    public Text ammoText; // �e�򐔂�\������UIText

    private float nextFireTime = 0f; // ���ɔ��ˉ\�Ȏ���
    //private int currentAmmo = 100; // ���݂̒e��

    protected override void Update()
    {
        Vector3 mousePosition = Input.mousePosition + mouseOffset;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        worldMousePosition.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(worldMousePosition - transform.position);

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && !barrierActive)
        {
            Fire();
        }
        else if (!Input.GetMouseButton(0) && muzzleFlash.isPlaying)
        {
            muzzleFlash.Stop();
        }
    }

    void Fire()
    {
        Vector3 spawnPoint = bulletSpawnPoint.transform.position;
        Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
        Quaternion bulletRotation = transform.rotation * spreadRotation;
        GameObject bullet = Instantiate(BulletPrefab, spawnPoint, bulletRotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
            AudioManager.Instance.PlaySE(SESoundData.SE.Gun0);
        }

        if (!muzzleFlash.isPlaying)
        {
            muzzleFlash.Play();
        }

        nextFireTime = Time.time + 1f / fireRate;
        //currentAmmo--; // �e�������
        UpdateAmmoText(); // �e�򐔂��X�V
    }

    //�e�򐔂��X�V���郁�\�b�h
    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: ��";
        }
    }
}
