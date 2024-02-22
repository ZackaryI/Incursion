using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    ShopManager shopManager;

    //offensive
    int physicalDMGBounus = 0;
    int energyWeaponDMGBounus = 0;
    float fireRateBounus = 0;

    //Defensive
    int enemyMovmentSpeedPenalty = 0;
    int generatorHealthBonus = 0;
    bool turretsAbleToDetectStealth = false;
    int sheildDMGBonus = 0;
    GameObject objectToHide;

    //Economic
    int researchBonus = 0;
    int researchMoneyBonus = 0;
    int enemyCurrencyBounus = 0;
    float researchSpeedBonus = 0;
    int upgradeCostDiscountBonus = 0;

    //button ifo
    int cost;
    int valueAdded = 0;
    float valueAddedf;
    Image image;
    Button button;
    private void Start()
    {
        shopManager = GetComponent<ShopManager>();
    }
    //offensive
    public int PhysicalDMGBounus
    {
        get { return physicalDMGBounus; }
        set
        {
            physicalDMGBounus = value;
            valueAdded = value;
            Check(ref physicalDMGBounus);
        }
    }
    public float FireRateBounus
    {
        get { return fireRateBounus; }
        set
        {
            fireRateBounus = value;
            valueAddedf = value;
            CheckF(ref fireRateBounus);
        }
    }
    public int EnergyWeaponDMGBounus
    {
        get { return energyWeaponDMGBounus; }
        set { energyWeaponDMGBounus = value;
            valueAdded = value; 
            Check(ref energyWeaponDMGBounus); }
    }
    //Defensive
    public int EnemyMovmentSpeedPenalty
    {
        get { return enemyMovmentSpeedPenalty; }
        set { enemyMovmentSpeedPenalty = value; 
            valueAdded = value; 
            Check(ref enemyMovmentSpeedPenalty); }
    }
    public int GeneratorHealthBonus
    {
        get { return generatorHealthBonus; }
        set { generatorHealthBonus = value; 
            valueAdded = value; 
            Check(ref generatorHealthBonus); }
    }
    public bool TurretsAbleToDetectStealth
    {
        get { return turretsAbleToDetectStealth; }
        set
        {
            turretsAbleToDetectStealth = value;
            Checkb(ref turretsAbleToDetectStealth);
        }
    }
    public int SheildDMGBounus
    {
        get { return sheildDMGBonus; }
        set
        {
            sheildDMGBonus = value;
            Check(ref sheildDMGBonus);
        }
    }
    public void UnlockTurret() 
    {
        int tempNum = 0;
        Check(ref tempNum);
    }
    //economic
    public int ResearchBonus
    {
        get { return researchBonus; }
        set { researchBonus = value; 
            valueAdded = value; 
            Check(ref researchBonus); }
    }
    public int EnemyCurrencyBounus
    {
        get { return enemyCurrencyBounus; }
        set { enemyCurrencyBounus = value;
            valueAdded = value; 
            Check(ref enemyCurrencyBounus); }
    }
    public int ResearchMoneyBonus
    {
        get { return researchMoneyBonus; }
        set
        {
            researchMoneyBonus = value;
            valueAdded = value;
            Check(ref researchMoneyBonus);
        }
    }
    public int UpgradeCostReductionBonus
    {
        get { return upgradeCostDiscountBonus; }
        set
        {
            upgradeCostDiscountBonus = value;
            valueAdded = value;
            Check(ref upgradeCostDiscountBonus);
        }
    }
    public float ResearchSpeedBonus
    {
        get { return researchSpeedBonus; }
        set
        {
            researchSpeedBonus = value;
            valueAddedf = value;
            CheckF(ref researchSpeedBonus);
        }
    }
    //checks to see if button should be disabled
    public void SetObject (GameObject buttonObject)
    {
        image = buttonObject.GetComponent<Image>();
        button = buttonObject.GetComponent<Button>();
    }
    public void SetCost(int cost) 
    {
        this.cost = cost;
    }
    void Check(ref int bonus) 
    {
        if (shopManager.BuyResearch(cost))
        {
            image.color = Color.green;
            button.interactable = false;

            if (objectToHide != null) 
            {
                objectToHide.SetActive(false);

                objectToHide = null;
            }
        }
        else 
        {
            bonus -= valueAdded;
        }

        button = null;
        image = null;
        valueAdded = 0;
    }
    void CheckF(ref float bonus)
    {
        if (shopManager.BuyResearch(cost))
        {
            image.color = Color.green;
            button.interactable = false;
        }
        else
        {
            bonus -= valueAddedf;
        }

        button = null;
        image = null;
        valueAdded = 0;
    }
    void Checkb(ref bool bonus)
    {
        if (shopManager.BuyResearch(cost))
        {
            image.color = Color.green;
            button.interactable = false;
        }
        else
        {
            bonus = !bonus;
        }

        button = null;
        image = null;
        valueAdded = 0;
    }
    public void SetObjectToHide(GameObject objectToHide) 
    {
        this.objectToHide = objectToHide;
    }
}
