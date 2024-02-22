using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LandMine : MonoBehaviour
{
    [SerializeField] float ExplosionRange = 2.5f;
    [SerializeField] int dmg = 100;
    [SerializeField] GameObject explosionAnimation;
    [SerializeField] Canvas DamageIndicatorCanvas;
    [SerializeField] float DamageIndicatorForceUp = 10f;
    [SerializeField] float DamageIndicatorForceSide = 400;
    bool explode = false;
    [SerializeField] bool armed = false;

    EnemyTracker enemyTracker;

    private void OnEnable()
    {
        armed = true;
        enemyTracker = GameObject.Find("GameMaster").GetComponent<EnemyTracker>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && armed)
        {
            if (explode == false)
            {
                explode = true;
                Boom();
      
            }
        }
    }
    void Boom()
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform enemy in enemyTracker.enemyList)
        {
            float range = Vector3.Distance(transform.position, enemy.position);

            if (range <= ExplosionRange)
            {
               list.Add(enemy);
            }
        }
        foreach (Transform enemy in list) { DamageEnemy(enemy); }

        Destroy(gameObject);
    }
    void DamageEnemy(Transform enemy) 
    {
        enemy.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
        Canvas canvas = Instantiate(DamageIndicatorCanvas, transform.position, transform.rotation);
        canvas.gameObject.GetComponent<Rigidbody>().AddForce(Vector2.up * DamageIndicatorForceUp);
        canvas.gameObject.GetComponent<Rigidbody>().AddForce(Vector2.right * Random.Range(-DamageIndicatorForceSide, DamageIndicatorForceSide));
        canvas.transform.LookAt(Camera.main.transform);
        canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dmg.ToString();
    }
}
