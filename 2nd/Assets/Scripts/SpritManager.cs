using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritManager : MonoBehaviour
{
    public static SpritManager instance;

    [SerializeField] List<Sprite> allSprites;

    private void Awake()
    {
        if (instance == null)//ΩÃ±€≈Ê
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
    }
    public Sprite GetSprite(string _spriteName) 
    {
        int count = allSprites.Count;
        for(int iNum = 0; iNum < count; iNum++) 
        {
            if (_spriteName == allSprites[iNum].name) 
            {
                return allSprites[iNum];
            }
        } 
        return null;
    }
}
