using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartRespawnCoroutine(Player player, float respawnTime)
    {
        StartCoroutine(Respawn(player, respawnTime));
    }

    private IEnumerator Respawn(Player player, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        player.Respawn();
    }
}
