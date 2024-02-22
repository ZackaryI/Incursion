using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractTurret : MonoBehaviour
{
    protected float dmg;
    protected float range;
    protected Upgrade upgrade;
    public void Constructor(float dmg, float range)
    {
        this.dmg = dmg;
        this.range = range;
        upgrade = GameObject.Find("GameMaster").GetComponent<Upgrade>();
    }
    abstract public void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource);

    public virtual void Rotate(Transform turret, Transform enemyPos, TurretObj turretConfigs) 
    {
    }
}
