using UnityEngine;
using System.Collections;

public class FowllowMouse : MonoBehaviour
{
    [SerializeField] ShopManager shopManager;
    [SerializeField] GameObject upgradeMenu;

    bool upgradeMunuOn = false;

    [SerializeField] LayerMask layerMask;
    Ray rayMouseMovment;
    RaycastHit hitMouseMovment;
    bool onNode = false;
    bool occupied = false;
    [SerializeField] bool placingTurret = false;

    bool tempOnNode = false;
    bool tempOccupied = false;
    Transform tempNodePos;

    Transform nodePos;
    void Update()
    {
        rayMouseMovment = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayMouseMovment, out hitMouseMovment, 1000, layerMask))
        {
            transform.position = hitMouseMovment.point;
        }
        if (Input.GetMouseButtonDown(0) && !placingTurret || Input.GetMouseButtonDown(1) && !placingTurret)
        {
            if (!upgradeMunuOn && tempOccupied && tempOnNode)
            {
                nodePos.GetComponent<turretPlacmentNode>().DisplayValues();
                nodePos.GetComponent<turretPlacmentNode>().RangeIdacator(true);
                upgradeMunuOn = true;
                upgradeMenu.transform.position = Input.mousePosition;
                upgradeMenu.SetActive(true);
            }
            else if (upgradeMunuOn)
            {
                StartCoroutine(CloseMenu());
            }
        }
    }
    public void OnNode(bool onNode, Transform nodePos, bool occupied)
    {
        this.onNode = onNode;
        this.nodePos = nodePos;
        this.occupied = occupied;

        if (!upgradeMunuOn)
        {
            tempOnNode = onNode;
            tempOccupied = occupied;
            tempNodePos = nodePos;
        }
    }
    public bool GetOnNode()
    {
        return onNode;
    }
    public bool GetOccupied()
    {
        return occupied;
    }
    public bool GetupgradeMunuOn()
    {
        return upgradeMunuOn;
    }
    public Transform GetNodePos()
    {
        return nodePos;
    }
    private IEnumerator CloseMenu()
    {
        yield return new WaitForSeconds(.15f);
        upgradeMunuOn = false;
        if (tempNodePos != null) { tempNodePos.GetComponent<turretPlacmentNode>().RangeIdacator(false); }
        ClearTempValue();
        StopAllCoroutines();
    }
    public void Upgrade()
    {
        tempNodePos.gameObject.GetComponent<turretPlacmentNode>().UpgradeTurret();
        if (tempNodePos != null) { tempNodePos.GetComponent<turretPlacmentNode>().RangeIdacator(false); }
        ClearTempValue();
    }
    public void Sell()
    {
        tempNodePos.gameObject.GetComponent<turretPlacmentNode>().Sell();
        ClearTempValue();
    }
    public void CancelPlacment()
    {
        if (Physics.Raycast(rayMouseMovment, out hitMouseMovment, 1000))
        {
            if (hitMouseMovment.collider.gameObject.CompareTag("Turret"))
            {
                TurretController turret = hitMouseMovment.collider.gameObject.GetComponent<TurretController>();
                turret.enabled = true;
                turret.CancelPlacment();
            }
            else if (hitMouseMovment.collider.gameObject.CompareTag("Landmine") || hitMouseMovment.collider.gameObject.CompareTag("Barrier")) 
            {
                PlacePlaceable placeable = hitMouseMovment.collider.gameObject.GetComponent<PlacePlaceable>();
                placeable.enabled = true;
                placeable.CancelPlacment();
            }
        }
        placingTurret = false;
    }
    public void SetBoolPlacingTurret(bool value)
    {
        placingTurret = value;
    }
    void ClearTempValue()
    {
        upgradeMenu.SetActive(false);
        tempOnNode = onNode;
        tempOccupied = occupied;
        tempNodePos = nodePos;
    }
}
