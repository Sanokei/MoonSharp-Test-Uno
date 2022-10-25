using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartialClasses : MonoBehaviour {}

namespace Console
{
    public partial class ConsoleManager : MonoBehaviour
    {
        public static ConsoleManager CreateConsole()
        {
            // Createw PhysicPlayerOptions.Instance._Canvasal Representation of Window
            GameObject window = Instantiate(Resources.Load($"Computer/Window/Console/Window_console") as GameObject, PlayerOptions.Instance._Canvas.transform);
            
            // Get the Console
            DragUI dragUI = window.GetComponent<DragUI>(); // slow

            // Setting DragUI
            setDragUI(dragUI,PlayerOptions.Instance._Camera, PlayerOptions.Instance._Canvas, PlayerOptions.Instance._CanvasRectTransform);
            
            // Set the window's parent.
            window.transform.SetParent(PlayerOptions.Instance._Canvas.transform);

            return window.GetComponentInChildren<ConsoleManager>();
        }
        public static void setDragUI(DragUI dragUI, Camera camera, Canvas canvas, RectTransform canvasRectTransform)
        {
            dragUI._Camera = camera;
            dragUI._Canvas = canvas;
            dragUI._CanvasRectTransform = canvasRectTransform;
        }
    }
}