using UnityEngine;

public class DespawnAfterTimePassed : MonoBehaviour
{
    [SerializeField] float timeTIllDespawn = 5f;

    void Start()
    {
        Destroy(gameObject, timeTIllDespawn);
    }
}
