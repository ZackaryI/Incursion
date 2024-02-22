using UnityEngine;

public class PlacePlaceable : MonoBehaviour
{

    Transform mouse;
    bool placed = false;
    [SerializeField] bool ableToBePlaced = false;
    bool blockedLandMine = false;
    bool BlockedBarreir = false;
    ShopManager shopManager;
    void Start()
    {
        shopManager = GameObject.Find("GameMaster").GetComponent<ShopManager>();
        mouse = GameObject.Find("Mouse").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!placed)
        {
            transform.position = mouse.position;
        }

        if (ableToBePlaced && Input.GetMouseButtonDown(0) && !blockedLandMine && !BlockedBarreir)
        {
            Placed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            ableToBePlaced = true;
            transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
        }
        if (other.gameObject.CompareTag("Landmine"))
        {
            blockedLandMine = true;
        }
        if (other.gameObject.CompareTag("Barrier"))
        {
            BlockedBarreir = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            ableToBePlaced = false;
        }
        if (other.gameObject.CompareTag("Landmine"))
        {
            blockedLandMine = false;
        }
        if (other.gameObject.CompareTag("Barrier"))
        {
            BlockedBarreir = false;
        }
    }

    void Placed()
    {
        placed = true;
        if (gameObject.GetComponent<LandMine>())
        {
            gameObject.GetComponent<LandMine>().enabled = true;
        }
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.layer = 11;
        shopManager.Placed();
        Destroy(this);
    }
    public void CancelPlacment()
    {
        if (GetComponent<LandMine>())
        {
            shopManager.Addmoney(shopManager.GetCost(6));
        }
        else 
        {
            shopManager.Addmoney(shopManager.GetCost(7));
        }
        Destroy(gameObject);
    }
}
