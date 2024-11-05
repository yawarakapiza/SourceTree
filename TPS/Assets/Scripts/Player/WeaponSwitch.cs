using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] guns; // Gun�I�u�W�F�N�g�̔z��
    private int currentGunIndex = 0; // ���݃A�N�e�B�u��Gun�I�u�W�F�N�g�̃C���f�b�N�X

    public float switchCooldown = 1.0f; // ����؂�ւ��̃N�[���_�E�����ԁi�b�j
    private float lastSwitchTime; // �Ō�ɕ����؂�ւ�������

    public Image[] weaponIcons; // ����A�C�R����Image�R���|�[�l���g�̔z��

    void Start()
    {
        // ������Ԃōŏ��̏e�ȊO���A�N�e�B�u�ɂ���
        UpdateWeaponActiveState();
        UpdateWeaponIcons();
    }

    void Update()
    {
        // �E�N���b�N�������ꂽ�������o���A�N�[���_�E�����I�����Ă��邩�m�F����
        if (Input.GetMouseButtonDown(1) && Time.time >= lastSwitchTime + switchCooldown)
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // ���݂̏e���A�N�e�B�u�ɂ���
        guns[currentGunIndex].SetActive(false);

        // ���̏e�̃C���f�b�N�X���v�Z����
        currentGunIndex = (currentGunIndex + 1) % guns.Length; // �z��̒����Ŋ������]�����邱�Ƃŏz������

        // �V�����e���A�N�e�B�u�ɂ���
        guns[currentGunIndex].SetActive(true);

        // �Ō�ɕ����؂�ւ������Ԃ��X�V����
        lastSwitchTime = Time.time;

        // ����A�C�R���̋����\�����X�V����
        UpdateWeaponIcons();
    }

    // ���ׂĂ̏e���A�N�e�B�u�ɂ��A���ݑI������Ă���e�������A�N�e�B�u�ɂ���
    void UpdateWeaponActiveState()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        guns[currentGunIndex].SetActive(true);
    }

    // ����A�C�R���̋����\�����X�V����
    void UpdateWeaponIcons()
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            if (i == currentGunIndex)
            {
                // ���ݑI������Ă��镐��̃A�C�R����ʏ�̐F�ɐݒ�
                weaponIcons[i].color = new Color(weaponIcons[i].color.r, weaponIcons[i].color.g, weaponIcons[i].color.b, 1f);
            }
            else
            {
                // �I������Ă��Ȃ�����̃A�C�R���𔖈Â�����
                weaponIcons[i].color = new Color(weaponIcons[i].color.r, weaponIcons[i].color.g, weaponIcons[i].color.b, 0.5f); // �����x��0.5�ɐݒ�
            }
        }
    }
}
