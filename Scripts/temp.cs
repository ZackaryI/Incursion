using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class temp : MonoBehaviour
{
    [SerializeField] float health = 20;
    float hardHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject endScreen;
    [SerializeField] TextMeshProUGUI scoreTextLose;
    [SerializeField] ShopManager shopManager;
    Upgrade upgrade;

    bool hasHealthUpgrade = false;

    private void Start()
    {
        upgrade = GameObject.Find("GameMaster").GetComponent<Upgrade>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            health += 100;
            TakeDamage(0);
        }
    }
    private void LateUpdate()
    {
        if (!hasHealthUpgrade && upgrade.GeneratorHealthBonus > 0)
        {
            health += upgrade.GeneratorHealthBonus;
            hardHealth += upgrade.GeneratorHealthBonus;
            TakeDamage(0);
            hasHealthUpgrade = true;
        }
    }
    private void Awake()
    {
        hardHealth = health;
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        healthBar.value = health / hardHealth;

        if (health + upgrade.GeneratorHealthBonus <= 0)
        {
            scoreTextLose.text = "Score: " + shopManager.GetScore();
            endScreen.SetActive(true);

            Time.timeScale = 0;
        }
    }
}
