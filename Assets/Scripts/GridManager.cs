using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public GameObject capturedImagePrefab;
    private Object[] textures;
    private Sprite sprite;
    private Texture2D myTexture;

    // Start is called before the first frame update
    void Start()
    {
        DrawGrid();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawGrid()
    {
        GameObject newObj;

        // Load textures from image files
        textures = Resources.LoadAll("Images", typeof(Texture2D));

        for (int i = 0; i < textures.Length; i++)
        {
            newObj = (GameObject)Instantiate(capturedImagePrefab, transform);

            // Apply EventTrigger to GameObject
            EventTrigger trigger = newObj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();

            // Apply images to GameObject

            // Create sprite
            myTexture = (Texture2D)textures[i];
            sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            newObj.GetComponent<Image>().sprite = sprite;

            // Copy object for closure problem
            GameObject go = newObj;

            // Callback
            entry.callback.AddListener((eventData) => { ImageClickEvent(go); });
            trigger.triggers.Add(entry);
        }
    }

    void ImageClickEvent(GameObject go)
    {
        Debug.Log("Image Clicked" + go.transform.position);
    }
}
