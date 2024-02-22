using UnityEngine;

public class ArcTowerProjectile : MonoBehaviour
{
    [SerializeField] float lifeSpan = 2;
    [SerializeField] float sizeIncreaseSpeed = .5f;
    [SerializeField] Canvas DamageIndicatorCanvas;
    [SerializeField] float DamageIndicatorForceUp = 10f;
    [SerializeField] float DamageIndicatorForceSide = 400;

    int dmg = 0;
    int minDmg = 0;
    int modifier = 0;
    bool physicalDmg = false;
    bool energyDmg = false;
    bool explosive = false;
    bool canDmage = true;
    public void SetValues(TurretObj turretAttributes)
    {
        physicalDmg = turretAttributes.physicalDmg;
        energyDmg = turretAttributes.energyDmg;
        explosive = turretAttributes.explosive;
    }
    private void FixedUpdate()
    {
        lifeSpan -= Time.deltaTime;
        transform.localScale += new Vector3(Time.deltaTime * sizeIncreaseSpeed, 0, Time.deltaTime * sizeIncreaseSpeed);
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && canDmage)
        {
            canDmage = false;
            int curDmg = Random.Range(minDmg, dmg) + modifier;
            EnemyController enemyAttrbutes = other.gameObject.GetComponent<EnemyController>();
            Canvas canvas = Instantiate(DamageIndicatorCanvas, other.transform.position, Quaternion.identity);

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

            Destroy(gameObject);
        }
    }

    public void SetDMG(int minDmg, int dmg, int modifier)
    {
        this.dmg = dmg;
        this.minDmg = minDmg;
        this.modifier = modifier;
    }
}
