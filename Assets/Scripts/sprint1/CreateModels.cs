using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateModels : MonoBehaviour
{
    public RectTransform contents;
    public GameObject listItem;
    Sprite[] sprites;
    List <GameObject> modelObj = new List<GameObject>();

    void Start()
    {
        // Start is called before the first frame update
        int nModel = 0;

        sprites = Resources.LoadAll<Sprite>("ModelThumbnails");
        nModel = sprites.Length;

        GameObject newObj;
        
        for (int i = 0; i < nModel; i++)
        {
            newObj = (GameObject)Instantiate(this.listItem, this.contents);

            newObj.name = sprites[i].name;
            Debug.Log("newObj.name: " + newObj.name);

            if (newObj.GetComponent<Image>().sprite)
            {
                newObj.GetComponent<Image>().sprite = sprites[i];
            }

            modelObj.Add(newObj);

            // Button click event listener
            GameObject clickedButton = newObj.gameObject;
            newObj.GetComponent<Button>().onClick.AddListener(() => { ButtonClickEvent(clickedButton);});
        }
    }

    void ButtonClickEvent(GameObject clickedButton)
    {
        ARTapToPlaceObject.Instance.ButtonClickEvent(clickedButton.name);
    }
}
