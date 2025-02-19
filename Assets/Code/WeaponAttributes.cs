using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    public AttributesManager atm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            atm = transform.root.transform.GetComponent<AttributesManager>();
            other.GetComponent<AttributesManager>().TackeDamage(atm.attack);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy chem Player");
            atm = transform.root.Find("Enemy").transform.GetComponent<AttributesManager>();
            other.GetComponent<AttributesManager>().TackeDamage(atm.attack);
        }
    }
}
