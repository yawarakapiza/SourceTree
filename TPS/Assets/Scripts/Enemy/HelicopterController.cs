using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // サブローターのレイヤーのウェイトを1に設定
        animator.SetLayerWeight(1, 1.0f);
    }

    void Update()
    {
        // ヘリコプターのローターを回すアニメーションを常に再生
        // 必要に応じて条件を追加してアニメーションの再生を制御
        animator.SetBool("IsRotating", true);
    }
}
