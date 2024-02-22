using UnityEngine;

public class DelaySpawn : MonoBehaviour
{
    [SerializeField] float delaySpawn = 0f;
    [SerializeField] GameObject Object;

    private void Update()
    {
        if (delaySpawn <= 0f)
        {
            Object.SetActive(true);
            Destroy(this);
        }
        else 
        {
            delaySpawn -= Time.deltaTime;
        }
    }

}
