using UnityEngine;
public class NodePathing : MonoBehaviour
{
   [SerializeField] private Transform[] nodes;

    public Transform GetNode(int index) 
    {
        return nodes[index];
    }
}
