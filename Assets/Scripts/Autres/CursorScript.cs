using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [Header("Raycast")]
    public Transform rayOrigin;       // Main/controller
    public float maxDistance = 10f;
    public LayerMask raycastLayer;

    [Header("UI")]
    public Canvas canvas;
    public RectTransform spriteUI;    // Sprite/Image dans le Canvas

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = new Ray(
            rayOrigin.position,
            rayOrigin.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, raycastLayer))
        {
            MoveUI(hit.point);
        }
    }

    void MoveUI(Vector3 worldPosition)
    {
        Vector2 screenPos =
            cam.WorldToScreenPoint(worldPosition);

        RectTransform canvasRect =
            canvas.GetComponent<RectTransform>();

        Vector2 localPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : cam,
            out localPos
        );

        spriteUI.anchoredPosition = localPos;
    }
}