using System.Threading.Tasks;
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
    private int magSize = 15;

    [SerializeField]
    private int ammoInMag = 15;

    [SerializeField]
    private int currentAmmo = 40;

    [SerializeField]
    private float fireRate = 0.5f;

    [SerializeField]
    private float reloadTime = 1.5f;

    [SerializeField]
    private AudioClip shootingSound; // Assuming you have a script for handling sound

    [SerializeField]
    private AudioClip reloadSound; // Assuming you have a script for handling sound

    private float lastShotTime;

    public async void Reload()
    {
        if (currentAmmo > 0)
        {
            if (reloadSound != null)
            {
                AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            }
            await Task.Delay((int)(reloadTime * 1000));
            ammoInMag = 0;
            int ammoToReload = magSize - ammoInMag;
            if (currentAmmo >= ammoToReload)
            {
                ammoInMag = magSize;
                currentAmmo -= ammoToReload;
            }
            else
            {
                ammoInMag += currentAmmo;
                currentAmmo = 0;
            }
        }
    }

    public void Shoot()
    {
        if (lastShotTime + fireRate < Time.time && ammoInMag > 0)
        {
            ammoInMag--;
            if (ammoInMag <= 0)
            {
                Reload();
            }
            // Play the shooting sound here if you have one

            if (shootingSound != null)
            {
                AudioSource.PlayClipAtPoint(shootingSound, transform.position);
            }
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
