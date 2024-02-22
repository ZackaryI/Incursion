using System.Collections.Generic;
using UnityEngine;

public class ResearchTower : AbstractTurret
{
    ShopManager shopManager;
    private void Start()
    {
        shopManager = GameObject.Find("GameMaster").GetComponent<ShopManager>();
    }
    public override void Shoot(Transform turret, List<Transform> enemies, TurretObj turretValues, AudioClip shootSound, AudioSource soundSource)
    {
        shopManager.Addmoney(upgrade.ResearchMoneyBonus);
        shopManager.AddResearchPoints(turretValues.dmg);
    }
}
