using UnityEngine;
using UnityEngine.UI;

public class Health
{
    Slider healthBar;
    Image healthBarBackGround;
    Image healthBarFill;
    float health;
    float hardHealth;
    float sheildHealth;
    float sheildHardHealth;

    /// <summary>
    /// sets the defualt values and references 
    /// checkts to see if sheild health is more than zero if so sets the halth bar color to blue
    /// </summary>
    public Health(Slider healthBar, Image healthBackGround, Image healthBarFill, float health, float sheildHealth = 0) 
    {
        this.healthBar = healthBar;
        this.healthBarBackGround = healthBackGround;
        this.healthBarFill = healthBarFill;

        this.health = health;
        hardHealth = health;
        sheildHardHealth = sheildHealth;

        this.sheildHealth = sheildHealth;

        if (sheildHealth > 0) 
        {
            SetHealthBarValues();
        }
    }
    /// <summary>
    /// handles the damage that is dealt to the enemy if sheild is less than zero it 
    /// changes the color of heath bar.
    /// if health is less than zero or equal to zero it returns true for the enemy to be destroyed
    /// </summary>
    public bool TakeDMG(float dmg, float sheildDMGBonus, GameObject sheild) 
    {
        if (sheildHealth > 0)
        {
            sheildHealth -= dmg + sheildDMGBonus;
            healthBar.value = sheildHealth / sheildHardHealth;

            if (sheildHealth <= 0)
            {
                healthBarBackGround.color = Color.black;
                healthBarFill.color = Color.red;

                if (sheild != null)
                {
                    sheild.SetActive(false);
                }

                healthBar.value = health / hardHealth;
            }
        }
        else
        {
            health -= dmg;
            healthBar.value = health / hardHealth;

            if (health < 0)
            {
               return true;
            }
        }

        return false;
    }

    void SetHealthBarValues() 
    {
        if (sheildHealth > 0)
        {
            healthBarBackGround.color = Color.red;
            healthBarFill.color = Color.blue;
        }
    }

    public float GetSheildHealth() {return sheildHealth; }
}
