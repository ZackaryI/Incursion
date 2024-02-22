using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float health = 10;

    public void TakeDamage(float dmg) 
    {
        health -= dmg;

        if (health <= 0) 
        {
            Instantiate(explosion,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
