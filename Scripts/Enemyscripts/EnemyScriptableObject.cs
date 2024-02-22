using UnityEngine;

[CreateAssetMenu(menuName = "Enemy",
    fileName = "EnemyConfig", order = 0)]

public class EnemyScriptableObject : ScriptableObject
{
    public bool isWaveEmitter = false;

    public float MoveSpeed = 5f;
    public float health = 10f;
    public float SheildHealth = 100;
    public int value = 10;

    public float range = 4;
    public float shootSpeed = 2;
    public Rigidbody Bullet;
    public float BulletSpeed;
    public int dmg = 1;
    public bool kamikaze = false;

    public bool PhysicalDmgWeakness = false;
    public bool EnergyDmgWeakness = false;
    public bool ExplosivDmgWeakness = false;
    public bool Air = false;
    public float airHeight = 0;
    public bool invisible = false;

    public GameObject explosionAnimation;
    public AudioClip explosionAudio;
    public AudioClip ShootAudio;

    public LayerMask layerMask;
}
