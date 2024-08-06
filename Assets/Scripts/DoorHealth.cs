
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoorHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] ParticleSystem doorBlast;
    [SerializeField] GameObject door;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject lostTheGame;

    private int health;
    private int maxHealth = 100;
    private int damage;
    private int maxlives = 20;
    private int currentlives;
    // Start is called before the first frame update
    void Start()
    {
        currentlives = maxlives;
        health = maxHealth;
        healthSlider.value = health;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ReduceDoorHealth();
            Debug.Log("Enemy entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            currentlives--;
            UpdateLives();
            Debug.Log("Enemy destroyed");
        }
    }

    private void UpdateLives()
    {
        livesText.text = currentlives.ToString() + "/" + maxlives.ToString();
        if (currentlives <= 0)
        {
            lostTheGame.SetActive(true);
        }
    }

    private void ReduceDoorHealth()
    {
        if(health > 0)
        {
            damage = Random.Range(25, 29);
            health -= damage;
            healthSlider.value = health;
            if (health <= 0)
            {
                doorBlast.gameObject.SetActive(true);
                doorBlast.Play();
                StartCoroutine(DisableAfterDelay(1f));
            }
        }        
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        door.SetActive(false);
    }
}
