using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            Debug.Log("thu thap diem");
            FindFirstObjectByType<GameSession>().UpdateGem(1);
            other.transform.gameObject.SetActive(false);
            Destroy(other);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
