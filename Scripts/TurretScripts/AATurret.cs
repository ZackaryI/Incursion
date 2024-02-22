using System.Collections.Generic;
using UnityEngine;

public class AATurret : AbstractTurret
{
    [SerializeField] Transform[] Gunbarrels = new Transform[4];
    [SerializeField] Transform gunBarrel;
    [SerializeField] Rigidbody bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bullerSpread = 5;
    int GunBarrelIndex = 0;

    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        if (GunBarrelIndex >= Gunbarrels.Length) { GunBarrelIndex = 0; }

        Rigidbody shotBullet = Instantiate(bullet, Gunbarrels[GunBarrelIndex].transform.position, gunBarrel.rotation);
        shotBullet.GetComponent<Transform>().Rotate(Random.Range(-bullerSpread, bullerSpread), Random.Range(-bullerSpread, bullerSpread), shotBullet.GetComponent<Transform>().rotation.z);
        shotBullet.GetComponent<Bullet>().SetDMG(Random.Range(turretValues.minDMG, turretValues.dmg));
        shotBullet.GetComponent<Bullet>().SetValues(turretValues);
        shotBullet.AddForce(shotBullet.GetComponent<Transform>().forward * bulletSpeed);
        soundSource.PlayOneShot(shootSound);
        GunBarrelIndex++;
    }
    public override void Rotate(Transform turret, Transform enemyPos, TurretObj turretConfigs)
    {
        float distance = Vector3.Distance(transform.position, enemyPos.position);

        //turret guns rotation
        Transform turretBar = turret.GetChild(0);
        // float turretBarAngle = lawOfCosines(Vector3.Distance(transform.position, enemyPos.position), turretConfigs.range, enemyPos.position.y);
        float turretBarAngle = -(Mathf.Atan2(enemyPos.position.y, distance) * Mathf.Rad2Deg) + 5;
        turretBar.localRotation = Quaternion.Euler(turretBarAngle, 0, 0);

        Vector3 Dist = transform.position - enemyPos.position;
        float angle = (Mathf.Atan2(Dist.x, Dist.z) * Mathf.Rad2Deg) + 180;
        turret.eulerAngles = new Vector3(-90, 0, angle);
        gunBarrel.localRotation = Quaternion.Euler(turretBarAngle, angle, 0);
    }
}


