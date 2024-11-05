using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform mainCannonTransform;
    public Transform[] subCannonTransforms;
    public Transform mainCannonSpawnPoint;
    public Transform[] subCannonSpawnPoints;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float detectionRange = 15f;
    public int health = 100;
    public GameObject[] ammoDropPrefabs;
    public float[] dropProbabilities;
    public GameObject explosionEffectPrefab;

    protected GameObject player;
    private float nextFireTime = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            EngagePlayer();
            AimTurretsAtPlayer();
        }
    }

    protected abstract void EngagePlayer();

    private void AimTurretsAtPlayer()
    {
        Vector3 direction = (player.transform.position - mainCannonTransform.position).normalized;
        direction.y = 90f; // è„â∫ï˚å¸ÇÃäpìxÇñ≥éã
        mainCannonTransform.rotation = Quaternion.LookRotation(direction);

        foreach (var cannonTransform in subCannonTransforms)
        {
            direction = (player.transform.position - cannonTransform.position).normalized;
            direction.y = 90f; // è„â∫ï˚å¸ÇÃäpìxÇñ≥éã
            cannonTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

    protected void FireMainCannon()
    {
        Fire(mainCannonSpawnPoint);
    }

    protected void FireSubCannons()
    {
        foreach (var spawnPoint in subCannonSpawnPoints)
        {
            Fire(spawnPoint);
        }
    }

    private void Fire(Transform spawnPoint)
    {
        if (Time.time >= nextFireTime)
        {
            // íeÇÃî≠éÀï˚å¸ÇåvéZÇ∑ÇÈ
            Vector3 direction = (player.transform.position - spawnPoint.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(direction);

            // íeÇê∂ê¨ÇµÇƒî≠éÀÇ∑ÇÈ
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, bulletRotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
            }

            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySE(SESoundData.SE.Exp01);
        }

        if (ammoDropPrefabs != null && ammoDropPrefabs.Length > 0 && dropProbabilities != null && dropProbabilities.Length == ammoDropPrefabs.Length)
        {
            GameObject selectedDrop = ChooseRandomDrop();
            if (selectedDrop != null)
            {
                Instantiate(selectedDrop, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    private GameObject ChooseRandomDrop()
    {
        float totalProbability = 0f;
        foreach (float probability in dropProbabilities)
        {
            totalProbability += probability;
        }

        float randomValue = Random.value * totalProbability;

        for (int i = 0; i < dropProbabilities.Length; i++)
        {
            if (randomValue <= dropProbabilities[i])
            {
                return ammoDropPrefabs[i];
            }
            randomValue -= dropProbabilities[i];
        }

        return null;
    }
}