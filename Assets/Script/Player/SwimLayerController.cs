using UnityEngine;
using UnityEngine.Rendering;

public class SwimLayerController : MonoBehaviour
{
    public Animator animator;
    private int swimLayerIndex;
    public Transform cameraTransform;

    public Volume underWaterEff;
    void Start()
    {
        swimLayerIndex = animator.GetLayerIndex("Swim");

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetLayerWeight(swimLayerIndex, 1);
            other.GetComponent<PlayerControll>().isSwimming = true;
            underWaterEff.enabled = true;
        }

        if (other.CompareTag("MainCamera"))
        {
            other.GetComponentInParent<PlayerControll>().isUnderWater = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetLayerWeight(swimLayerIndex, 0);
            other.GetComponent<PlayerControll>().isSwimming = false;
            underWaterEff.enabled = false;
        }

        if (other.CompareTag("MainCamera"))
        {
            other.GetComponentInParent<PlayerControll>().isUnderWater = false;
        }
    }
}
