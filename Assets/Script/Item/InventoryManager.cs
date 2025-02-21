using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<ItemIn> items = new List<ItemIn>();

    void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void AddItem(ItemIn item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemIn item)
    {
        items.Remove(item); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
