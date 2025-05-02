using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    public Text text06;
    public GameObject Activator; //collider
    public string dialogue = "Dialogue"; //Text

    public float timer = 2f; //How long it will show for



    
    void Start()
    {
        text06.GetComponent<Text>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            text06.GetComponent<Text>().enabled = true;
            text06.text = dialogue.ToString();
            StartCoroutine(DisableText());
        }
    }
    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(timer);
        text06.GetComponent<Text>().enabled = false;
        Destroy(Activator);
    }


}
