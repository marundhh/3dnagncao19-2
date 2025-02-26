using UnityEngine;


public class save : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float x = PlayerPrefs.GetFloat("x", 0.0f);//doi so thu 2 la neu khong co
        float y = PlayerPrefs.GetFloat("y", 0.0f);
        float z = PlayerPrefs.GetFloat("z", 0.0f);
        Vector3 vector = new Vector3(x, y, z);
        player.transform.position = vector;
        Debug.Log("da load");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 vector = player.transform.position;
            PlayerPrefs.SetFloat("x", vector.x);
            PlayerPrefs.SetFloat("y", vector.y);
            PlayerPrefs.SetFloat("z", vector.z);
            //PlayerPrefs.SetInt("so",5);
            //PlayerPrefs.SetString("chuoi","teo");

            PlayerPrefs.Save();//khong bat buoc nhung nen co
            Debug.Log("da luu");
        }
    }
}
