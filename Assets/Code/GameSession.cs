using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public int health;
    public int gem;
    public TextMeshProUGUI txtGem;
    public Slider slider;
    private void Awake()
    {
       int numbersession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numbersession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    public void MaxHealth(int health)
    {
        slider.maxValue = health;
    }
    public void UpdateHealth(int health)
    {
        this.health = health;
        slider.value = health;
    }
    public void UpdateGem(int x)
    {
        this.gem += x;
        txtGem.text = "Gem: " + this.gem;
    }
}
