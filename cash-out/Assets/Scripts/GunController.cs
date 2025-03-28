using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private bool bulletSpreadEnabled = true;

    [SerializeField]
    private Vector3 bulletSpread = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField]
    private float fireRate = 0.5f;

    private float lastShotTime;

    public void Shoot()
    {
        if (lastShotTime + fireRate < Time.time)
        {
            lastShotTime = Time.time;
            GameObject bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.position,
                bulletSpawnPoint.rotation
            );
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(bulletSpawnPoint.forward * 20, ForceMode.Impulse);
            if (bulletSpreadEnabled)
            {
                bulletRb.AddForce(
                    new Vector3(
                        Random.Range(-bulletSpread.x, bulletSpread.x),
                        Random.Range(-bulletSpread.y, bulletSpread.y),
                        Random.Range(-bulletSpread.z, bulletSpread.z)
                    ),
                    ForceMode.Impulse
                );
            }
        }
    }
}
