using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Line")]
public class NarrationLine : MonoBehaviour
{


    [SerializeField]
    private NarrationCharacter m_Speaker;
    [SerializeField]
    private string m_Text;


    public NarrationCharacter Speaker => m_Speaker;

    public string Text => m_Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
