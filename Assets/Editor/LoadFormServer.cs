using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class LoadFormSever : MonoBehaviour
{
    private string bundleUrl = "https://drive.google.com/uc?export=download&id=1qFCJoEiMPCnCIsAXIF4KXe2S05L1OXQH";


    void Start()
    {
        StartCoroutine(DownloadAndLoadBundle());
    }


    IEnumerator DownloadAndLoadBundle()
    {
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle: " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);


                if (bundle != null)
                {
                    string[] assetNames = bundle.GetAllAssetNames();
                    foreach (string assetName in assetNames)
                    {
                        Debug.Log("Asset found: " + assetName);
                    }


                    // Load an asset example (assuming prefab)
                    for (int i = 0; i < assetNames.Length; i++)
                    {
                        GameObject prefab = bundle.LoadAsset<GameObject>(assetNames[i]);
                        Instantiate(prefab);
                        Debug.Log("load duoc du lieu");
                    }


                    bundle.Unload(false);
                }
                else
                {
                    Debug.LogError("Failed to load AssetBundle");
                }
            }
        }
    }
}
