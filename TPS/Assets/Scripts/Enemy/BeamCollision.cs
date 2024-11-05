using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    public float lifetime = 2f; // エフェクトの寿命（秒）
    public int damage = 1; // ビームのダメージ量
    private Collider beamCollider;

    private void Start()
    {
        // エフェクトの寿命が終了したら自動で削除する
        Destroy(gameObject, lifetime);
        beamCollider = GetComponent<Collider>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        // ビームが当たった場合に跳ね返らないように、他のコリジョンを無視する
        if (beamCollider != null && other.GetComponent<Collider>() != null)
        {
            Physics.IgnoreCollision(beamCollider, other.GetComponent<Collider>(), true);
        }
    }
}
