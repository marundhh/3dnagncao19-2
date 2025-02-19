using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool player_detection = false;
    public NPCConversation con;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player_detection = false; 
    }
    void Update()
    {
        if (player_detection && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("vao cung hoi thoai");
            ConversationManager.Instance.StartConversation(con);
        }
    }
}
