using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float dmg = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Gen"))
        {
            collision.gameObject.GetComponent<temp>().TakeDamage(dmg);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Barrier")) 
        {
            collision.gameObject.GetComponent<Barrier>().TakeDamage(dmg);
     
        }
    }

    public void SetDmg(float dmg) 
    {
        this.dmg = dmg;
    }
}
