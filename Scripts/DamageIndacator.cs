using TMPro;
using UnityEngine;

public class DamageIndacator : MonoBehaviour
{
    [SerializeField] float lifeSpan = 1.5f;
    private void Update()
    {
        Destroy(gameObject, lifeSpan);
    }
    public void SetUp(float dmg, float damageIndicatorForceUp, float damageIndicatorForceSide) 
    {
        GetComponent<Rigidbody>().AddForce(Vector2.up * damageIndicatorForceUp);
        GetComponent<Rigidbody>().AddForce(Vector2.right * Random.Range(-damageIndicatorForceSide, damageIndicatorForceSide));
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dmg.ToString();
    }
}
