using UnityEngine;
using DialogueEditor;

public class NPCSystem : MonoBehaviour
{
    bool player_InConvert = false;

    public NPCConversation convert;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player_InConvert && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Vào hội thoại NPC 1");
            ConversationManager.Instance.StartConversation(convert);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_InConvert = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_InConvert = false;
    }
}
