using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private CharacterDataBase characterDataBase;

    private int selectedCharacterSequence;
    private GameObject obj;

    private void Start()
    {
        obj = gameObject;

        if (PlayerPrefs.HasKey(obj.name) == true)
        {
            obj.GetComponent<Image>().sprite = characterDataBase.icons[PlayerPrefs.GetInt(obj.name)];
            selectedCharacterSequence = PlayerPrefs.GetInt(obj.name);
        }
    }

    public void ChooseNextCharacter()
    {
        selectedCharacterSequence += 1;

        if (selectedCharacterSequence == characterDataBase.icons.Count)
        {
            selectedCharacterSequence = 0;
        }

        obj.GetComponent<Image>().sprite = characterDataBase.icons[selectedCharacterSequence];  
    }

    public void ChooseBackCharacter()
    {
        selectedCharacterSequence -= 1;
        
        if (selectedCharacterSequence < 0)
        {
            selectedCharacterSequence = characterDataBase.icons.Count - 1;
        }

        obj.GetComponent<Image>().sprite = characterDataBase.icons[selectedCharacterSequence];

    }

    public void SaveSelectedCharacterSequence()
    {
        PlayerPrefs.SetInt(obj.name, selectedCharacterSequence);
    }
}
