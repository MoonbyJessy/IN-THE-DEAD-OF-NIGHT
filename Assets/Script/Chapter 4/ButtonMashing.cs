using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMashing : MonoBehaviour
{
    public float mashDelay = 0.5f;
    public GameObject text;

    float mash;
    bool pressed;
    bool started;
    int currentMashCount;
    int targetMashCount;
    bool finished;
    bool hasFailedBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        StartChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.GetMouseButton(0))
        {
            started = true;
            text.SetActive(true);
            text.GetComponent<Text>().text = "SPACE";
        }
        if (started && !finished)
        { 
            
            mash -= Time.deltaTime;


            if (Input.GetKeyDown(KeyCode.Space) && !pressed)
            {
                pressed = true;
                currentMashCount++;
                mash = mashDelay;

                if (currentMashCount >= targetMashCount)
                {
                    finished = true;
                    text.GetComponent<Text>().text = "SUCCESS";
                    
                    if (hasFailedBefore)
                    {
                        Invoke("LoadSkippedScene", 1.0f);
                    }
                    else
                    {
                        Invoke("LoadNextScene", 1.0f);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                pressed = false;
            }
            if (mash <= 0 && !finished)
            {
                finished = true;
                hasFailedBefore = true;
                text.GetComponent<Text>().text = "FAILED";
                Invoke("ShowFailedMessage", 1.0f);
                Invoke("StartChallenge", 2.5f);
            }
        
        }
    }
    void StartChallenge()
    {
        mash = mashDelay;
        currentMashCount = 0;
        targetMashCount = Random.Range(6, 11);
        finished = false;
        started = false;
        pressed = false;

        if (text != null)
        {
            text.SetActive(false);
        }
    }
    void ShowFailedMessage()
    {
        if (text != null)
        {
            text.GetComponent<Text>().text = "What happened! Did i die?";
        }
    }
    
    void LoadSkippedScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
