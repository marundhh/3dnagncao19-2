using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public static DamagePopUpGenerator Current;
    public GameObject prefab;

    private void Awake()
    {
        Current = this;
    }
     
    public void CreatePopUp(Vector3 position , string text , Color color)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshPro>();
        
        Destroy(popup, 1f);
        Destroy(temp, 1f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
