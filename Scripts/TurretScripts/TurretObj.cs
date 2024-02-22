using UnityEngine;

[CreateAssetMenu(menuName = "Turret",
    fileName = "TurretConfig", order = 0)]

public class TurretObj : ScriptableObject
{
    public MeshRenderer TurretHead;
    public MeshRenderer TurretBase;
    public int minDMG;
    public int dmg;
    public float range;
    public bool rotates = true;
    public float coolDown;
    public int NextLevelUpgradeCost;
    public int costToBuy = 15;

    public bool antiAir = false;
    public bool infaredDetecting = false;
    public bool physicalDmg = false;
    public bool energyDmg = false;
    public bool explosive = false;
    public bool researchTower = false;

    public AudioClip shootAudio;
    public RuntimeAnimatorController animation;
    public bool DisableAnimation = true;
}
