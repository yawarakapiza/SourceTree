using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // �T�u���[�^�[�̃��C���[�̃E�F�C�g��1�ɐݒ�
        animator.SetLayerWeight(1, 1.0f);
    }

    void Update()
    {
        // �w���R�v�^�[�̃��[�^�[���񂷃A�j���[�V��������ɍĐ�
        // �K�v�ɉ����ď�����ǉ����ăA�j���[�V�����̍Đ��𐧌�
        animator.SetBool("IsRotating", true);
    }
}
