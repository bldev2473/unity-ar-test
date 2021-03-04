//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class SharingButtonController : MonoBehaviour
//{
//    public void OnCLick()
//    {
//		StartCoroutine(GetImageFileAndShare());
//	}

//	private IEnumerator GetImageFileAndShare()
//	{
//		yield return new WaitForEndOfFrame();

//		//Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
//		//ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
//		//ss.Apply();

//		var nativeShareInstance = new NativeShare();

//		var imageTextureList = ImageController.Instance.GetImageTextureList();

//		foreach(var texture in imageTextureList)
//        {
//			string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
//			File.WriteAllBytes(filePath, texture.EncodeToPNG());

//			nativeShareInstance.AddFile(filePath);
//		}

//		nativeShareInstance.SetSubject("Subject goes here").SetText("Hello world!")
//			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
//			.Share();

//		ImageController.Instance.ClearImageTextureList();

//		// Share on WhatsApp only, if installed (Android only)
//		//if( NativeShare.TargetExists( "com.whatsapp" ) )
//		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
//	}
//}
