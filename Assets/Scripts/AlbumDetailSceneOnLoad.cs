using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumDetailSceneOnLoad : MonoBehaviour
{
    private Sprite sprite;
    private Texture2D myTexture;

    void Start()
    {
        Debug.Log(CrossSceneInfo.CrossSceneInformation);

        string fileFormat = CrossSceneInfo.CrossSceneInformation[1].ToString();

        // Load textures from image files
        Object texture = Resources.Load("Images/" + CrossSceneInfo.CrossSceneInformation[0], typeof(Texture2D));

        // Apply images to GameObject
        // Create sprite from texture
        myTexture = (Texture2D)texture;
        sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));

        GameObject.FindGameObjectWithTag("AlbumDetailImage").GetComponent<Image>().sprite = sprite;
    }
}
