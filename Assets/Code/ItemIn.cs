using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ItemIn",menuName = "Inventory/ItemIn")]

public class ItemIn : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite image;
    public ItemType itemType;
    
    // Start is called before the first frame update
    public enum ItemType
    { 
        Consumables, Weapons
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
