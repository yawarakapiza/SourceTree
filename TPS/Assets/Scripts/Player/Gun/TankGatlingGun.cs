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

    public int maxAmmo = 100; // �ő�e��
    private int currentAmmo; // ���݂̒e��

    public Text ammoText; // �e�򐔂�\������UIText

    private float nextFireTime = 0f;
    private float currentRotationSpeed = 0f;

    void Start()
    {
        currentAmmo = maxAmmo; // �e����ő�ɐݒ�
        UpdateAmmoText(); // �e�򐔂�\��
    }

    void OnEnable()
    {
        // �}�E�X�{�^����������Ă��邩�m�F���āA�G�t�F�N�g��K�؂ɐ���
        CheckAndHandleFiringOnActivation();
    }

    void CheckAndHandleFiringOnActivation()
    {
        // �I�u�W�F�N�g���A�N�e�B�u�ɂȂ�Ƃ��Ƀ}�E�X�{�^���̏�Ԃ��`�F�b�N
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            // �e�򂪂���΃G�t�F�N�g���Đ�
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            // �}�E�X�{�^����������Ă��Ȃ����A�e�򂪂Ȃ���΃G�t�F�N�g���~
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }

    protected override void Update()
    {
        // Gun�I�u�W�F�N�g�̕������X�V����
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

        // �e�������
        currentAmmo--;
        Debug.Log("Ammo left: " + currentAmmo);

        // �e�򐔂��X�V
        UpdateAmmoText();

        // �}�Y���t���b�V�����Đ�
        if (!muzzelFlash.isPlaying && currentAmmo > 0)
        {
            muzzelFlash.Play();
        }

        // ���̔��ˉ\���Ԃ��X�V
        nextFireTime = Time.time + 1f / fireRate;

        // �e�򂪐؂ꂽ��G�t�F�N�g�ƃA�j���[�V�����̒�~
        if (currentAmmo <= 0)
        {
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
            currentRotationSpeed = 0; // �o�����̉�]���x��0�ɐݒ�
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
