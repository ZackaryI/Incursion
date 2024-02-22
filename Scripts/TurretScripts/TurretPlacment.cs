using UnityEngine;
public class TurretPlacment : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    BoxCollider col;
    Rigidbody rb;
    FowllowMouse mouse = null;
    turretPlacmentNode node;

    private void Start()
    {
        mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<FowllowMouse>();
        mouse.SetBoolPlacingTurret(true);
        col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    void LateUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (mouse.GetOnNode()) 
        {
            transform.position = mouse.GetNodePos().position;
        }
        else if (Physics.Raycast(ray, out hit, 1000, 3)) 
        {
            transform.position = hit.point;
        }
        if (Input.GetMouseButton(0) && mouse.GetOnNode())
        {
            Placed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<turretPlacmentNode>())
        {
            node = other.gameObject.GetComponent<turretPlacmentNode>();
        }
    }
    void Placed() 
    {
        if (!node.ReturnOccupied()) 
        {
            mouse.SetBoolPlacingTurret(false);
            col.isTrigger = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            transform.GetComponent<TurretController>().enabled = true;
            transform.GetComponent<TurretController>().IsPlaced();
            Destroy(this);
        }
    }
}
