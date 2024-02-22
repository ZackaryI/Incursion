using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    TextManager textManager;
    [SerializeField] int cash = 0;
    [SerializeField] int researchPoints = 0;
    [SerializeField] GameObject deployCancel;
    [SerializeField] GameObject deployOpen;
    [SerializeField] GameObject deployClose;

    [SerializeField] GameObject[] turrets;
    [SerializeField] int[] turretCost;

    [SerializeField] GameObject redWarningBackGroundEnergy;
    [SerializeField] GameObject redWarningBackGroundResearchPoints;
    [SerializeField] float lengthOfWarning = 2f;
    [SerializeField] TMP_Dropdown dropdown;
    bool on = false;
    float timeElapsed = 0;

    Upgrade upgrade;
    int score = 0;

    private void Start()
    {
        textManager = GetComponent<TextManager>();
        textManager.UpdateMoneyText(cash);
        upgrade = GetComponent<Upgrade>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Addmoney(100);
        }
    }
    public void Addmoney(int value)
    {
        if (upgrade.EnemyCurrencyBounus > 0) 
        {
            value = (int)((float)value * upgrade.EnemyCurrencyBounus) + value;
        }
        cash += value;

        if (cash < 0) { cash = 0; }

        textManager.UpdateMoneyText(cash);
    }
    public void AddResearchPoints(int value)
    {
        if (upgrade.ResearchBonus > 0)
        {
            value = (int)((float)value * upgrade.ResearchBonus) + value;
        }
        researchPoints += value;

        if (researchPoints < 0) { researchPoints = 0; }
        textManager.UpdateResearchText(researchPoints);
    }
    public void BuyTurret(int selectedTurret)
    {
        if (cash < turretCost[selectedTurret])
        {
            timeElapsed = 0;
            Placed();
            StartCoroutine(WarningNoMoney(redWarningBackGroundEnergy));
            return;
        }
        else
        {
            deployCancel.SetActive(true);
            deployClose.SetActive(false);
            deployOpen.SetActive(false);
            Instantiate(turrets[selectedTurret]);
            Addmoney(-turretCost[selectedTurret]);
        }
    }
    public bool BuyResearch(int cost) 
    {
        if (cost <= researchPoints)
        {
            AddResearchPoints(-cost);
            return true;
        }
        else 
        {
            timeElapsed = 0;
            StartCoroutine(WarningNoMoney(redWarningBackGroundResearchPoints));
            return false;
        }
    }
    public bool UpgradeTurret(int cost)
    {

        if (cost > cash)
        {
            timeElapsed = 0;
            StartCoroutine(WarningNoMoney(redWarningBackGroundEnergy));
            return false;
        }
        else
        {
            Addmoney(-cost);
            return true;
        }
    }
    IEnumerator WarningNoMoney(GameObject RedWarningBackGround)
    {
        if (on)
        {
            RedWarningBackGround.SetActive(false);
            on = false;
        }
        else
        {
            RedWarningBackGround.SetActive(true);
            on = true;
        }
        if (timeElapsed > lengthOfWarning)
        {
            RedWarningBackGround.SetActive(false);
            on = false;
            StopCoroutine(WarningNoMoney(RedWarningBackGround));
        }
        else
        {
            timeElapsed += .2f;
            yield return new WaitForSeconds(.2f);
            StartCoroutine(WarningNoMoney(RedWarningBackGround));
        }
    }
    public void Placed()
    {
        deployCancel.SetActive(false);
        deployClose.SetActive(false);
        deployOpen.SetActive(true);
    }
    public int GetCost(int index)
    {
        return turretCost[index];
    }
    public void AddScore(int score) 
    {
        this.score = score;

        string name = "Level" + SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(name, score);
    }
    public int GetScore() { return score; }
}
