using MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

public class ButtonManager : MonoBehaviour
{
    public int imageWidth;
    public int imageHeight;
    private float maxWidth;
    private float maxHeight;
    public RectTransform rectTransform;
    public Vector3 imagePosition;
    public Vector3 imageRotation;
    public Vector3 trackPoint;
    HandTracker handTracker;
    private bool justCalled = false;

    public ButtonManager(int width, int height, RectTransform rectTransform, Vector3 position)
    {
        this.imageWidth = width;
        this.imageHeight = height;
        this.rectTransform = rectTransform;
        this.imagePosition = position;
    }

    void GetCurrentVertices()
    {
        Vector3[] vertices = new Vector3[4];
        this.rectTransform.GetWorldCorners(vertices);
        this.trackPoint = vertices[0];
    }


    public GameObject MakeButton(Transform image)
    {
        Vector3[] vertices = new Vector3[4];
        this.rectTransform.GetWorldCorners(vertices);

        float maxWidth = Mathf.Abs(vertices[1].x - vertices[2].x);
        this.maxWidth = maxWidth;
        float maxHeight = Mathf.Abs(vertices[0].y - vertices[1].y);
        this.maxHeight = maxHeight;

        GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);
        button.name = "button";
        button.transform.position = new Vector3(image.position.x, image.position.y, image.position.z + 0.003f);
        button.transform.localScale = new Vector3(maxWidth, maxHeight, 0.003f);
       
        this.handTracker = gameObject.AddComponent<HandTracker>();

        MRTKBaseInteractable interactable = button.AddComponent<MRTKBaseInteractable>();
        interactable.selectEntered.AddListener(x => Notify());

        return button;
        
    }

    private void Notify()
    {
        ///When a user touches the image, this function gets called twice.
        ///Check to make sure that this wasn't just called. 
        if (justCalled)
        {
            justCalled = false;
            return;
        }

        Vector3 currentTouch = this.handTracker.GetPosition();
        if (currentTouch == Vector3.zero)
        {
            Debug.LogWarning("No controller was active");
            return;
        }

        GetCurrentVertices();
        Matrix4x4 matrix = Matrix4x4.Inverse(Matrix4x4.Rotate(Quaternion.Euler(imageRotation)));
        
        currentTouch = matrix.MultiplyVector(currentTouch); ///inverse to account for rotation
        trackPoint = matrix.MultiplyVector(trackPoint); 

        float percentX = Mathf.Abs(currentTouch.x - trackPoint.x) / this.maxWidth;
        float percentY = Mathf.Abs(currentTouch.y - trackPoint.y) / this.maxHeight;

        int pixelX = Mathf.FloorToInt(percentX * this.imageWidth) + 1;
        int pixelY = Mathf.FloorToInt(percentY * this.imageHeight) + 1;

        if (pixelX > imageWidth || pixelY > imageHeight) ///bounds checking
        {
            Debug.LogError("The area touched was out of bounds of the dimensions of the image");
            return;
        }
        

        Vector2 touchedPixel = new Vector2(pixelX, pixelY); /// This is where the image was pressed.

        string name = $"({pixelX}, {pixelY})";
        Debug.Log($"pixel touched = {name}");

        justCalled = true;
    }

}
