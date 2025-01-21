using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Journal : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;

    public void RotateForward() 
    {
        index++;
        float angle = 180;
        StartCoroutine(Rotate(angle, true));
    
    }
    public void RotateBack()
    {
        float angle = 0;
        StartCoroutine(Rotate(angle, false));
        
    }
    IEnumerator Rotate(float angle, bool forawrd)
    {
        float value = 0f;
        while (true)
        {
            Quaternion targetRotaion = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotaion, value);
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotaion);

            if (angle1 < 0.1f)
            {
                if (forawrd == false)
                {
                    index--;
                }
                break;

            }
            yield return null;
        }
    }
}
