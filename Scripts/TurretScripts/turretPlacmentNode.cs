using UnityEngine;
using TMPro;

public class turretPlacmentNode : MonoBehaviour
{
    [SerializeField] GameObject highLight;
    [SerializeField] bool occupied = false;
    FowllowMouse mouse = null;
    [SerializeField] TurretController turret;
    [SerializeField] ShopManager shop;
    [SerializeField] TextMeshProUGUI upgradetext;
    [SerializeField] TextMeshProUGUI sellText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject upgradeButton;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            highLight.SetActive(true);
            mouse = collision.gameObject.GetComponent<FowllowMouse>();

            mouse.OnNode(true, transform, occupied);
        }
        if (collision.gameObject.CompareTag("Turret"))
        {
            occupied = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            highLight.SetActive(false);
            mouse.OnNode(false, null, false);
            mouse = null;
        }
    }
    public void UpgradeTurret()
    {  
        if (turret.GetUpgradeCost() <= 0)
        {
            return;
        }

        Upgrade upgrade = shop.GetComponent<Upgrade>();
        if (shop.UpgradeTurret(turret.GetUpgradeCost() - upgrade.UpgradeCostReductionBonus))
        {
            turret.UpgradTurret();
        }
    }
    public void SetTurret(TurretController turret)
    {
        if (occupied == false)
        {
            this.turret = turret;
            occupied = true;
        }
        else 
        {
            Debug.Log("The node tried taking data it should not have please nottify me.");
        }
    }
    public void Sell() 
    {
        try
        {
            occupied = false;
            int cost = turret.GetCostToBuy();
            turret.SoldTurret();
            shop.Addmoney(cost);
            turret = null;
        }
        catch 
        {
            occupied = false;
            turret = null;
        }
    }
    public TurretController GetTurret() 
    {
        return turret;
    }
    public void DisplayValues()
    {
        if(mouse.GetupgradeMunuOn() && turret == null) 
        {
            return;
        }
        try 
        {
            if (turret.GetUpgradeCost() <= 0)
            {
                upgradeButton.SetActive(false);
            }
            else
            {
                upgradeButton.SetActive(true);
            }
        }
        catch 
        {
            Debug.Log("Turret does not exist");
            return;
        }

        Upgrade upgradeBonus = shop.GetComponent<Upgrade>();
        sellText.text = "Sell $" + turret.GetCostToBuy();
        upgradetext.text = "Upgrade $" + (turret.GetUpgradeCost() - upgradeBonus.UpgradeCostReductionBonus);
        levelText.text = "LVL " + (turret.GetTurretLvl() + 1) + "/5";
    }
    public void RangeIdacator(bool value) 
    {
        if (turret != null) { turret.SetRangeIndactor(value); }
    }
    public bool ReturnOccupied() 
    {
        return occupied;
    }
}
