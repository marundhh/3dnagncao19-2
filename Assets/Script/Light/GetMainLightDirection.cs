using UnityEngine;

[ExecuteInEditMode]
public class GetMainLightDirection : MonoBehaviour
{
    [SerializeField] private Material customSkybox;
    private void Update()
    {
        customSkybox.SetVector("_MainLightDirection", transform.forward);
    }
}