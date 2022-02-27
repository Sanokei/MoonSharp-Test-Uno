using System.Collections;
using UnityEngine;

public static class ExtensionMethods
{
    public static void ResizeByCellsize(this RectTransform rectTransform, int width, int height, int cellSize)
    {
        // Simply it just multiplies the width and height by the cell size to make it bigger
        // Whats so werid is that the cellSize doesnt need to be multiplied by the scale of the transform
        // which means that the scale is already included in sizeDelta of the RectTransform 
        // whoever programmed the sizeDelta is a genius and also retarded
        rectTransform.sizeDelta = new Vector2Int(height * cellSize, width * cellSize);
    }

    public static Vector2Int ConvertToGridPoint(this RectTransform rectTransform, Vector2 vector2, int cellSize)
    {
        // This requires you to multiple the scale for it to work
        // Keep in mind that the grid position will change depending on the anchor position 
        // meaning you might need to change if the vector2 position should be subtracted from the rectTransform position or vice versa
        // You can think about it like a normal graph, you are just setting which Quadrant you are in with the anchor position
        return new Vector2Int(Mathf.FloorToInt((vector2.x - rectTransform.position.x) / (rectTransform.localScale.x * cellSize)), Mathf.FloorToInt((vector2.y - rectTransform.position.y) / (rectTransform.localScale.y * cellSize)));
    }
}
