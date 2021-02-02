using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class GridManager : MonoBehaviour
{
    // Images
    public GameObject capturedButtonImagePrefab;
    private Object[] textures;
    private Sprite sprite;
    private Texture2D myTexture;

    // Videos
    public GameObject videoPlayerPrefab;
    private VideoClip[] videos;

    // Toggle Button
    public static GameObject[] toggles;

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

        // 1. Load textures from image files
        textures = Resources.LoadAll("Images", typeof(Texture2D));

        for (int i = 0; i < textures.Length; i++)
        {
            newObj = (GameObject)Instantiate(capturedButtonImagePrefab, transform);

            // Apply images to GameObject

            // Create sprite
            myTexture = (Texture2D)textures[i];
            sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            newObj.GetComponent<Image>().sprite = sprite;
        }

        // Initiate toggle button
        toggles = GameObject.FindGameObjectsWithTag("ToggleForSelection");
        UIGameObjectHandler.setActiveStatus(toggles, false);

        // 2. Load textures from video files
        VideoClip video = Resources.Load("Videos/gatos_", typeof(VideoClip)) as VideoClip;

        GameObject videoPlayerGameObject = (GameObject)Instantiate(videoPlayerPrefab, transform);

        VideoPlayer videoPlayer = videoPlayerGameObject.GetComponent<VideoPlayer>();
 
        //videoPlayer.time = 0;
        //videoPlayer.Play();
        //int width = videoPlayer.texture.width;
        //int height = videoPlayer.texture.height;
        //Texture2D preview = new Texture2D(width, height, TextureFormat.RGB24, false);
        //RenderTexture.active = videoPlayer.targetTexture;
        //preview.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //preview.Apply();
        //sprite = Sprite.Create(preview, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        //videoPlayer.Pause();
        //RenderTexture.active = null;

        //newObj = (GameObject)Instantiate(capturedButtonImagePrefab, transform);

        // Create sprite
        //newObj.GetComponent<Image>().sprite = sprite;

        //for (int i = 0; i < videos.Length; i++)
        //{
        //    newObj = (GameObject)Instantiate(capturedButtonImagePrefab, transform);

        //    // Apply images to GameObject

        //    // Create sprite
        //    myTexture = (Texture2D)videoPlayer.texture;
        //    sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
        //    newObj.GetComponent<Image>().sprite = sprite;
        //}
    }
}
