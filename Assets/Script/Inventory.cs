using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeReference] public List<ItemSlotInfo> items = new List<ItemSlotInfo>();

    [Space]
    [Header("Inventory Menu Components")]
    public GameObject inventoryMenu;
    public GameObject itemPanel;
    public GameObject itemPanelGrid;

    [Space]
    public int inventorySize = 16;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            items.Add(new ItemSlotInfo(null, 0));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                inventoryMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
}
