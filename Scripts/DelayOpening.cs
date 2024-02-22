using UnityEngine;

public class DelayOpening : MonoBehaviour
{
    [SerializeField] float delay = 5f;
    [SerializeField] GameObject obj;
    private void FixedUpdate()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else 
        {
            obj.SetActive(true);
            Destroy(this);
        }
    }
}
