using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Line")]
public class NarrationCharacter : ScriptableObject

{
    [SerializeField]
    private string m_CharacterName;

    public string CharacterName => m_CharacterName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
