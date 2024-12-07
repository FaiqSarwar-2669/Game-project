using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float healt;
    public float maxHealth;
    public int lives = 1;

    private void Awake()
    {
        healt = maxHealth;
    }

    void Update()
    {
        checkHealth();
        //kill();
    }

    private void kill()
    {
        if (healt == 0)
        {
            StartCoroutine(WaitBeforeSceneChange());
        }
    }

    private void checkHealth()
    {
        if (healt <= 0)
        {
            lives--;
            if (lives > 0)
            {
                healt = maxHealth;
            }
            else
            {
                SceneManager.LoadSceneAsync(1);
                //StartCoroutine(WaitBeforeSceneChange());
            }
        }
    }

    private IEnumerator WaitBeforeSceneChange()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(1);
    }
}
