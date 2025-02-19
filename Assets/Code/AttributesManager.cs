using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int attack;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;
    

    public void TackeDamage(int amount)
    {

    health -= amount;
        
        DamagePopUpGenerator.Current.CreatePopUp(
            transform.position,amount.ToString(),Color.yellow);
        if (gameObject.CompareTag("Enemy"))
        {
          Slider slider = gameObject.transform
                .GetChild(1).transform
                .GetChild(0).transform.GetComponent<Slider>();
            slider.value = health;
          if(health <= 0){
                EnemyDie();
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<GameSession>().UpdateHealth(health);
            if (health <= 0)
            {
                Time.timeScale = 0;
            }
        }
    }
    void ActiveGem()
    {
        GameObject gem = gameObject.transform.GetChild(2).gameObject;
        gem.SetActive(true);
    }
    void DeActiveTpose()
    {
        GameObject enemytpose = gameObject.transform.GetChild(0).gameObject;
        enemytpose.SetActive(false);    
    }
    public void EnemyDie()
    {
        Debug.Log("ke thu die");
        Animator ani = gameObject.transform.GetChild(0).GetComponent<Animator>();
        ani.SetBool("isDead", true);
        GameObject canvas = gameObject.transform.GetChild(1).gameObject;
        canvas.SetActive(false);

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Invoke("DeActiveTpose", 2f);
        Invoke("ActiveGem", 2f);
        Destroy(gameObject,10f);
    }
    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            float totalDamage = attack;
            if(Random.Range(0f, 1f)< critChance)
                totalDamage += critDamage;
            atm.TackeDamage(attack);
        }
    }
    private void Start()
    {
        if(gameObject.transform.CompareTag("Player"))
            //FindFirstObjectByType<GameSession>().health = health;
            FindFirstObjectByType<GameSession>().MaxHealth(health);
        FindFirstObjectByType<GameSession>().UpdateHealth(health);
    }

}
