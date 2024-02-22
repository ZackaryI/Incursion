using UnityEngine;
using System.Collections.Generic;

public class AutoTurret : AbstractTurret
{

    [SerializeField] Transform gunBarrel;
    [SerializeField] Rigidbody bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform muzzleFlashAnimiation;
    [SerializeField] Transform casingSpawnPos;
    [SerializeField] Rigidbody casingAnimation;
    [SerializeField] float casingSpeed;

    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        Rigidbody shotBullet = Instantiate(bullet, gunBarrel.GetChild(0).transform.position, gunBarrel.rotation.normalized);
        shotBullet.GetComponent<Bullet>().SetDMG(Random.Range(turretValues.minDMG, turretValues.dmg) + upgrade.PhysicalDMGBounus);
        shotBullet.GetComponent<Bullet>().SetValues(turretValues);
        shotBullet.AddForce(gunBarrel.GetChild(0).transform.forward * bulletSpeed);
        soundSource.PlayOneShot(shootSound);
        Instantiate(muzzleFlashAnimiation, gunBarrel.GetChild(0).transform.position, gunBarrel.rotation.normalized).parent = gunBarrel.GetChild(0).transform;
        Instantiate(casingAnimation, casingSpawnPos.position, gunBarrel.rotation.normalized).AddForce(casingSpawnPos.right * casingSpeed);
    }
    public override void Rotate(Transform turret, Transform enemyPos, TurretObj turretConfigs) 
    {
        Vector3 Dist = transform.position - enemyPos.position;
        float angle = (Mathf.Atan2(Dist.x, Dist.z) * Mathf.Rad2Deg) + 180;
        turret.eulerAngles = new Vector3(-90, 0, angle);
        gunBarrel.localRotation = Quaternion.Euler(0, angle, 0);
    }
}