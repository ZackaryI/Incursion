using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    [SerializeField] AbstractTurret turrentType;
    [SerializeField] TurretObj[] turretConfigs;
    [SerializeField] GameObject rangeIndicator;
    int turretConfigIndex;
    float coolDownTimer;
    MeshRenderer turretRenderer;
    //List<Transform> enemyList = new();
    turretPlacmentNode node;
    AudioSource turretAudioSource;

    [SerializeField] Transform turretHead;
    [SerializeField] float turretHeadSpawnHeight = .75f;
    [SerializeField] Transform turretBase;
    Animator animator;
    ShopManager shopManager;
    Upgrade upgrade;
    float range;
    EnemyTracker enemyTracker;
    List<Transform> targets = new ();
    private void OnEnable()
    {
        shopManager = GameObject.Find("GameMaster").GetComponent<ShopManager>();
        enemyTracker = shopManager.GetComponent<EnemyTracker>();
        SetRangeIndactor(false);
        shopManager.Placed();
        upgrade = shopManager.GetComponent<Upgrade>();
    }
    void Start()
    {
        turretConfigIndex = 0;
        coolDownTimer = 0;
        turrentType.Constructor(turretConfigs[turretConfigIndex].dmg, range);
        range = turretConfigs[turretConfigIndex].range;

        if (gameObject.GetComponent<Animator>()) 
        {
            animator = gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = turretConfigs[turretConfigIndex].animation;
        }

        turretRenderer = turretHead.GetComponent<MeshRenderer>();
        turretAudioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else if(!turretConfigs[turretConfigIndex].DisableAnimation)
        {
            animator.SetBool("PlayAnimation", false);
        }


        if (enemyTracker.enemyList.Count > 0 && range > 0)
        {
            if (targets.Count == 0)
            {
                FindNewTarget();
                return;
            }
            if (targets[0] == null) 
            {
                targets.RemoveAt(0);
                return;
            }
            if (Vector3.Distance(targets[0].position, transform.position) > range)
            {
                targets.RemoveAt(0);
                return;
            }
            EnemyController enemyController = targets[0].GetComponent<EnemyController>();
            if (enemyController.GetEnemyAttributes().Air == turretConfigs[turretConfigIndex].antiAir && enemyController.GetEnemyAttributes().invisible == upgrade.TurretsAbleToDetectStealth)
            {
                turrentType.Rotate(turretHead, targets[0], turretConfigs[turretConfigIndex]);

                if (coolDownTimer > 0)
                {
                    return;
                }

                if (!turretConfigs[turretConfigIndex].DisableAnimation)
                {
                    PlayAnimation(true);
                }

                turrentType.Shoot(turretHead, enemyTracker.enemyList, turretConfigs[turretConfigIndex], turretConfigs[turretConfigIndex].shootAudio, turretAudioSource);
                coolDownTimer = turretConfigs[turretConfigIndex].coolDown - upgrade.FireRateBounus;
            }
            else 
            {
                targets.RemoveAt(0);
            }

        }
        else 
        {//research tower code
            if (coolDownTimer <= 0 && turretConfigs[turretConfigIndex].range == 0)
            {
                turrentType.Shoot(turretHead, null, turretConfigs[turretConfigIndex], turretConfigs[turretConfigIndex].shootAudio, turretAudioSource);
                coolDownTimer = turretConfigs[turretConfigIndex].coolDown - upgrade.ResearchSpeedBonus;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("TurretNodes"))
        {
            node = collider.gameObject.GetComponent<turretPlacmentNode>();
        }
    }
    void FindNewTarget() 
    {
        for (int i = 0; i < enemyTracker.enemyList.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, enemyTracker.enemyList[i].position);

            if (dist < range && dist > 0)
            {
                targets.Add(enemyTracker.enemyList[i]);
            }
        }
    }
    public void UpgradTurret() 
    {
        turretConfigIndex++;
        range = turretConfigs[turretConfigIndex].range;
        turrentType.Constructor(turretConfigs[turretConfigIndex].dmg, range);
        Destroy(turretRenderer.gameObject);

        turretRenderer = Instantiate(turretConfigs[turretConfigIndex].TurretHead, new Vector3(transform.position.x,
            transform.position.y + turretHeadSpawnHeight, transform.position.z), turretRenderer.gameObject.transform.rotation);

        turretRenderer.gameObject.transform.parent = transform;
        turretAudioSource.clip = turretConfigs[turretConfigIndex].shootAudio;
        turretHead = turretRenderer.GetComponent<Transform>().transform;

        Destroy(turretBase.gameObject);
        turretBase = Instantiate(turretConfigs[turretConfigIndex].TurretBase, transform.position, Quaternion.Euler(-90, 0, 0)).GetComponent<Transform>();
        turretBase.gameObject.transform.parent = transform;
    }
    public void SoldTurret()   { Destroy(gameObject);}

    public void IsPlaced(){node.SetTurret(this);}
    public void CancelPlacment()
    {
        shopManager.Addmoney(turretConfigs[turretConfigIndex].costToBuy);
        Destroy(gameObject);
    }

    public int GetUpgradeCost()  { return turretConfigs[turretConfigIndex].NextLevelUpgradeCost - upgrade.UpgradeCostReductionBonus; }
    public int GetCostToBuy()  {return turretConfigs[turretConfigIndex].costToBuy;}
    public int GetTurretLvl() {return turretConfigIndex;}
    void PlayAnimation(bool state)  { animator.SetBool("PlayAnimation", state);}
    public void SetRangeIndactor(bool value) { rangeIndicator.SetActive(value);}
}
