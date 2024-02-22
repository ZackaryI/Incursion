using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] EnemyScriptableObject enemyAttributes;
    [SerializeField] Transform gunBarrel;
    AudioSource shootAudioSource;

    private int nodeIndex = 0;
    NodePathing node = null;
    ShopManager shopManager = null;
    [SerializeField] Transform currentNode = null;
    float dist;
    bool isDead = false;

    float coolDownTimer = 0;
    Vector3 targetDir;

    Upgrade upgrade;
    Health health;
    EnemyTracker enemyTracker;
    [SerializeField] GameObject Sheild;

    private void Start()
    {
        Slider healthBar = transform.GetComponentInChildren<Slider>();        
        Image healthBarBackGround = healthBar.transform.Find("Background").GetComponent<Image>();
        Image healthBarFill = healthBar.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();

        health = new(healthBar, healthBarBackGround, healthBarFill, enemyAttributes.health, enemyAttributes.SheildHealth);

        upgrade = GameObject.Find("GameMaster").GetComponent<Upgrade>();
        enemyTracker = upgrade.GetComponent<EnemyTracker>();
        enemyTracker.enemyList.Add(transform);

        shootAudioSource = gameObject.GetComponent<AudioSource>();
        shootAudioSource.clip = enemyAttributes.ShootAudio;

        if (enemyAttributes.kamikaze)
        {
            enemyAttributes.range = 0;
        }
    }
    public void EnemyConstructor(NodePathing node, ShopManager shopManager)
    {
        this.node = node;
        this.shopManager = shopManager;
        GetNextNode(nodeIndex);
    }

    private void FixedUpdate()
    {
        dist = Vector3.Distance(transform.position, currentNode.position);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward, out RaycastHit hit, enemyAttributes.range, ~enemyAttributes.layerMask))
        {

            if (!hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("Gen") && !hit.collider.CompareTag("Barrier"))
            {
                Move();
                return;
            }

            if (hit.collider.CompareTag("Gen"))
            {
                Shoot(hit.collider.gameObject, true);
            }
            else if (hit.collider.CompareTag("Barrier"))
            {
                Shoot(hit.collider.gameObject, false);
            }
        }
        else if (dist > .3f + enemyAttributes.airHeight)
        {
            Move();
        }
        else
        {
            GetNextNode(nodeIndex);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gen") && enemyAttributes.Air) 
        {
            Shoot(collision.gameObject, true);
            Destroy();
        }
    }
    private void GetNextNode(int index)
    {
        nodeIndex++;
      
        currentNode = node.GetNode(index);
    }
    private void Move() 
    {
        float moveSpeed = enemyAttributes.MoveSpeed + upgrade.EnemyMovmentSpeedPenalty;
        targetDir = currentNode.position - transform.position;
        targetDir.y = 0;

        if (targetDir != Vector3.zero)
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, new Vector3(currentNode.position.x, currentNode.position.y + enemyAttributes.airHeight,
        currentNode.position.z), (moveSpeed) * Time.deltaTime), Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), 1000 * Time.deltaTime));
        }
        else 
        {
            Debug.Log("OOPs FUCKY WUCKY!!");
        }
    }
    /// <summary>
    /// calls the health class to receive damage and update health bar.
    /// class will return true if health is less than or equal to zero.
    /// which take damage calls the destroy method.
    /// </summary>
    public void TakeDamage(float dmg)
    {
        if (health.TakeDMG(dmg, shopManager.GetComponent<Upgrade>().SheildDMGBounus, Sheild)) 
        {
            Destroy();
        }
    }
    /// <summary>
    /// is called when enemy is killed it first checks to see if it is alread dead to prevent being called multiple times
    /// it then provides funds to the player 
    /// spawns death animations and destroys itself.
    /// </summary>
    public void Destroy()
    {
        if (!isDead) 
        {
            enemyTracker.enemyList.Remove(transform);
            try
            {
                Instantiate(enemyAttributes.explosionAnimation, transform.position, transform.rotation).GetComponent<Explosion>().audioClip = enemyAttributes.explosionAudio;
                isDead = true;
                shopManager.Addmoney(enemyAttributes.value);
                shopManager.AddScore(enemyAttributes.value);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                coolDownTimer = 10000;
                Destroy(gameObject, .1f);
            }
            catch 
            {
                Destroy(gameObject);
            }
        }
    }
    void Shoot(GameObject obj, bool gen) 
    {

        if (coolDownTimer <= 0 && gen)
        {

            if (enemyAttributes.Air)
            {
                obj.GetComponent<temp>().TakeDamage(enemyAttributes.dmg);
                Destroy();
                return;
            }

            Rigidbody bullet;

            if (enemyAttributes.isWaveEmitter)
            {
                bullet = Instantiate(enemyAttributes.Bullet, gunBarrel.position, gunBarrel.rotation);
                bullet.gameObject.GetComponent<WaveEmitterProjectile>().SetDmg(enemyAttributes.dmg);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * enemyAttributes.BulletSpeed);
            }
            else 
            {
                bullet = Instantiate(enemyAttributes.Bullet, gunBarrel.position, gunBarrel.rotation.normalized);
                bullet.gameObject.GetComponent<EnemyBullet>().SetDmg(enemyAttributes.dmg);
                bullet.AddForce(gunBarrel.forward * enemyAttributes.BulletSpeed);
            }

            shootAudioSource.PlayOneShot(enemyAttributes.ShootAudio);
            coolDownTimer = enemyAttributes.shootSpeed;
        }
        else if (coolDownTimer <= 0 && !gen) 
        {
            Barrier barrier = obj.GetComponent<Barrier>();
            shootAudioSource.PlayOneShot(enemyAttributes.ShootAudio);
            barrier.TakeDamage(enemyAttributes.dmg);
            coolDownTimer = enemyAttributes.shootSpeed;
        }
        else
        {
            coolDownTimer -= Time.deltaTime;
        }
    }

    public bool GetEnemyEnergyDmg() { return enemyAttributes.EnergyDmgWeakness; }
    public bool GetEnemyPhysicalDmg() { return enemyAttributes.PhysicalDmgWeakness; }
    public bool GetEnemyExplosiveDmg() { return enemyAttributes.ExplosivDmgWeakness; }
    public float GetEnemySheildHealth() { return health.GetSheildHealth(); }
    public EnemyScriptableObject GetEnemyAttributes() { return enemyAttributes; }
}