using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageGetter : MonoBehaviour
{
    public string url;
    public Vector3 imagePosition;
    public Vector3 imageRotation;

    void Start()
    {
        StartCoroutine(FetchImage());
    }
    
    IEnumerator FetchImage()
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(url);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.Success)
        {
            ///make image
            Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
            GameObject imageObject = new GameObject("Image Object");
            SpriteRenderer renderer = imageObject.AddComponent<SpriteRenderer>();
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); ///this line of code is AI generated
            renderer.sprite = sprite;

            ///image scales
            int width = texture.width;
            int height = texture.height;

            float scalar = 0;

            while (width > 1 && height > 1)
            {
                width /= 8;
                height /= 8;
                scalar++;
            }

            imageObject.transform.localScale = new Vector3(1f / Mathf.Pow(2, scalar), 1f / Mathf.Pow(2, scalar));

            ///button is placed behind image
            ButtonManager buttonManager = gameObject.AddComponent<ButtonManager>();
            buttonManager.imageWidth = texture.width;
            buttonManager.imageHeight = texture.height;

            var rectTransform = imageObject.AddComponent<RectTransform>();
            buttonManager.rectTransform = rectTransform;
            buttonManager.imagePosition = imagePosition;
            buttonManager.imageRotation = imageRotation;
            var button = buttonManager.MakeButton(imageObject.transform);

            ///button and image become children of same parent transform
            imageObject.transform.SetParent(transform.parent, false);
            button.transform.SetParent(transform.parent, false); 
            imageObject.transform.localPosition = Vector3.zero;
            button.transform.localPosition = new Vector3(0, 0, 0.003f);

            ///parent object moves and rotates
            transform.parent.position = imagePosition;
            transform.parent.rotation = Quaternion.Euler(imageRotation);


            imageRequest.Dispose(); 
        }
        else
        {
            Debug.LogError($"Unable to fetch the image at url: {url}");
            Debug.LogError($"{imageRequest.error}");    
        }
            
    }
}
