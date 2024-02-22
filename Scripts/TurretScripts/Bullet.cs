using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeSpan = 2;
    [SerializeField] Canvas DamageIndicatorCanvas;
    [SerializeField] float DamageIndicatorForceUp = 10f;
    [SerializeField] float DamageIndicatorForceSide = 400;
    float dmg = 0;
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

        if (lifeSpan <= 0) 
        {
        Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDmage)
        {
            canDmage = false;
            EnemyController enemyAttrbutes = collision.gameObject.GetComponent<EnemyController>();
            Canvas canvas = Instantiate(DamageIndicatorCanvas, transform.position, Quaternion.identity);

            if (energyDmg == enemyAttrbutes.GetEnemyEnergyDmg() &&
             physicalDmg == enemyAttrbutes.GetEnemyPhysicalDmg()&&
                explosive == enemyAttrbutes.GetEnemyExplosiveDmg())
            {
                canvas.GetComponent<DamageIndacator>().SetUp(dmg, DamageIndicatorForceUp, DamageIndicatorForceSide);
                try
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
                }
                catch 
                {
                    Debug.Log("Null Exception Handled");
                }
            }
            else 
            {
                if (collision.gameObject.GetComponent<EnemyController>().GetEnemySheildHealth() > 0)
                {
                    dmg = Mathf.RoundToInt(dmg / 2);
                }
                canvas.GetComponent<DamageIndacator>().SetUp(dmg, DamageIndicatorForceUp, DamageIndicatorForceSide);
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
            }

            Destroy(gameObject);
        }
    }
    public void SetDMG(float dmg) 
    {
        this.dmg = dmg;
    }
    public bool GetPhysicalDmg() { return physicalDmg; }
    public bool GetEnergyDmg() { return energyDmg; }
    public bool GetExplosive() { return explosive; }
}
