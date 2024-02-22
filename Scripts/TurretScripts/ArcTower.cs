using UnityEngine;
using System.Collections.Generic;

public class ArcTower : AbstractTurret
{
    [SerializeField] ArcTowerProjectile projectile;
    [SerializeField] LayerMask layerMask;
    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        foreach (Transform enemyTransform in enemies) 
        {
            float dist = Vector3.Distance(enemyTransform.position, transform.position);

            if (dist < turretValues.range) 
            {
                if (enemyTransform.GetComponent<EnemyController>().GetEnemyAttributes().Air != true) 
                {
                    try
                    {
                        SpawnProjectile(turretValues, enemyTransform);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }   
        }
        soundSource.PlayOneShot(shootSound);
    }
    void SpawnProjectile(TurretObj turretValues, Transform enemypos) 
    {
        Physics.Raycast(transform.position, transform.TransformDirection(enemypos.position), out RaycastHit hit, turretValues.range, layerMask);

        ArcTowerProjectile shotBullet = Instantiate(projectile, transform.position + new Vector3(0, 2, 0), transform.rotation);
        shotBullet.SetDMG(turretValues.minDMG, turretValues.dmg, upgrade.EnergyWeaponDMGBounus);
        shotBullet.SetValues(turretValues);
        shotBullet.transform.localRotation = Quaternion.Euler(0, FindAngle(enemypos), 0);
        shotBullet.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, hit.distance);
    }
    float FindAngle(Transform enemypos) 
    {
        Vector3 distAngle = transform.position - enemypos.position;
        float angle = (Mathf.Atan2(distAngle.x, distAngle.z) * Mathf.Rad2Deg) + 180;
        return angle;
    }
}
