using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CapturedImageButtonController : MonoBehaviour
{
    public void CapturedImageButtonOnClick()
    {
        Image clickedImage = this.gameObject.GetComponent<Image>();
        CrossSceneInfo.CrossSceneInformation.Clear();
        CrossSceneInfo.CrossSceneInformation.Add(clickedImage.sprite.texture.name);
        CrossSceneInfo.CrossSceneInformation.Add(clickedImage.sprite.texture.format.ToString());
        SceneManager.LoadScene("AlbumDetailScene");
    }
}
