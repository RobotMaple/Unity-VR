using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class drawline : MonoBehaviour
{
    // Apply these values in the editor
    public LineRenderer LineRenderer;
    public Transform TransformOne;
    public Transform TransformTwo;
    public float cableSize = 0.05f;
    private void Update()
    {
        // set the color of the line
        LineRenderer.startColor = Color.red;
        LineRenderer.endColor = Color.red;

        // set width of the renderer
        LineRenderer.startWidth = cableSize;
        LineRenderer.endWidth = cableSize;

        // set the position
        LineRenderer.SetPosition(0, TransformOne.position); //
        LineRenderer.SetPosition(1, TransformTwo.position);
    }
}