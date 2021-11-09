using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    
    void OnTriggerEnter(Collider collider)
    { 
        if (collider.tag == "Enemy")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            StartCoroutine("RestartGame");
        }
    }

    IEnumerator RestartGame()
    {
        PhaseController.PhaseStartCondition();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene");
    }
}
