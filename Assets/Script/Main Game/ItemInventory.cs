
using UnityEngine;

[System.Serializable]
public abstract class ItemInventory 
{
    public abstract string GiveName();
    public virtual int MaxStacks()
    {
        return 30;
    }
   // public virtual Sprite GiveItemImage()
   // {

   // }
}
