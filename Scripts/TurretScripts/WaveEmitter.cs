using System.Collections.Generic;
using UnityEngine;

public class WaveEmitter : AbstractTurret
{
    public float waveAngle = 10;
    [SerializeField] Transform gunBarrel;
    [SerializeField] Rigidbody bullet;
    [SerializeField] float bulletSpeed;

    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        Rigidbody shotBullet = Instantiate(bullet, gunBarrel.GetChild(0).transform.position, gunBarrel.rotation.normalized);
        shotBullet.GetComponent<WaveEmitterProjectile>().SetDMG(turretValues.minDMG, turretValues.dmg, upgrade.EnergyWeaponDMGBounus);
        shotBullet.GetComponent<WaveEmitterProjectile>().SetValues(turretValues);
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
