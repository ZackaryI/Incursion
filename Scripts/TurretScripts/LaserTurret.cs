using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : AbstractTurret
{
    [SerializeField] Transform gunBarrel;
    [SerializeField] Rigidbody bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject turretAnimation;
    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        Rigidbody shotBullet = Instantiate(bullet, gunBarrel.GetChild(0).transform.position, gunBarrel.rotation.normalized) as Rigidbody;
        Transform animation = Instantiate(turretAnimation, gunBarrel.position, gunBarrel.rotation.normalized).transform;
        animation.transform.parent = transform;
        shotBullet.GetComponent<Bullet>().SetDMG(Random.Range(turretValues.minDMG, turretValues.dmg) + upgrade.PhysicalDMGBounus);
        shotBullet.GetComponent<Bullet>().SetValues(turretValues);
        shotBullet.AddForce(gunBarrel.GetChild(0).transform.forward * bulletSpeed);
        soundSource.PlayOneShot(shootSound);
    }
    public override void Rotate(Transform turret, Transform enemyPos, TurretObj turretConfigs)
    {
        Vector3 Dist = transform.position - enemyPos.position;
        float angle = (Mathf.Atan2(Dist.x, Dist.z) * Mathf.Rad2Deg) + 180;
        turret.eulerAngles = new Vector3(-90, 0, angle);
        gunBarrel.localRotation = Quaternion.Euler(0, angle, 0);
    }
}
