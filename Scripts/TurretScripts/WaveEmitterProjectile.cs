using UnityEngine;
using TMPro;

public class WaveEmitterProjectile : MonoBehaviour
{
    [SerializeField] float lifeSpan = 2;
    [SerializeField] float sizeIncreaseSpeed = .5f;
    [SerializeField] Canvas DamageIndicatorCanvas;
    [SerializeField] float DamageIndicatorForceUp = 10f;
    [SerializeField] float DamageIndicatorForceSide = 400;
    int dmg = 0;
    float damage = 0;
    int minDmg = 0;
    int bonusDMG = 0;
    bool physicalDmg = false;
    bool energyDmg = false;
    bool explosive = false;
    [SerializeField] bool istower = true;
    public void SetValues(TurretObj turretAttributes)
    {
        physicalDmg = turretAttributes.physicalDmg;
        energyDmg = turretAttributes.energyDmg;
        explosive = turretAttributes.explosive;
    }
    private void FixedUpdate()
    {
        lifeSpan -= Time.deltaTime;
        transform.localScale += new Vector3 (0, 0, Time.deltaTime * sizeIncreaseSpeed);
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        try 
        {
            if (other.gameObject.CompareTag("Enemy") && istower)
            {
                int curDmg = Random.Range(minDmg, dmg) + bonusDMG;
                EnemyController enemyAttrbutes = other.gameObject.GetComponent<EnemyController>();
                Canvas canvas = Instantiate(DamageIndicatorCanvas, transform.position, Quaternion.identity);

                if (energyDmg == enemyAttrbutes.GetEnemyEnergyDmg() &&
                 physicalDmg == enemyAttrbutes.GetEnemyPhysicalDmg() &&
                    explosive == enemyAttrbutes.GetEnemyExplosiveDmg())
                {
                    canvas.GetComponent<DamageIndacator>().SetUp(curDmg, DamageIndicatorForceUp, DamageIndicatorForceSide);
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(curDmg);
                }
                else
                {
                    if (other.gameObject.GetComponent<EnemyController>().GetEnemySheildHealth() > 0)
                    {
                        curDmg = Mathf.RoundToInt(curDmg / 2);
                    }
                    canvas.GetComponent<DamageIndacator>().SetUp(curDmg, DamageIndicatorForceUp, DamageIndicatorForceSide);
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(curDmg);
                }
            }
            else
            {
                if (other.gameObject.CompareTag("Gen"))
                {
                    other.gameObject.GetComponent<temp>().TakeDamage(damage);
                }
                else if (other.gameObject.CompareTag("Barrier"))
                {
                    other.gameObject.GetComponent<Barrier>().TakeDamage(damage);
                }
            }

        }
        catch 
        {
            Debug.Log("Object does not exist");
            Destroy(gameObject);
        }

    }

    public void SetDMG(int minDmg, int dmg, int bonusDMG)
    {
        this.dmg = dmg;
        this.minDmg = minDmg;
        this.bonusDMG = bonusDMG;
    }
    public void SetDmg(float dmg)
    {
        this.damage = dmg;
    }
    public bool GetPhysicalDmg() { return physicalDmg; }
    public bool GetEnergyDmg() { return energyDmg; }
    public bool GetExplosive() { return explosive; }
}
