using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomDuration = 1f;
    
    public float zoomOutOrthographicSize = 5.721248f; // Desired zoom level
    public float zoomInOrthographicSize = 4.5f; // Desired zoom level
    public float endZoomSize = 3.453396f; // Desired zoom level
    
    public Vector3 zoomOutPosition;
    public Vector3 zoomInPosition; // The position to move the camera to
    public Vector3 endZoomPosition;

    [ContextMenu("Zoom In")]
    public void ZoomIn()
    {
        // Ensure the camera starts in its original position
        mainCamera.transform.position = zoomOutPosition;

        // Zoom in by reducing the orthographic size and move the camera to the target position
        Sequence zoomSequence = DOTween.Sequence();
        zoomSequence.Append(mainCamera.DOOrthoSize(zoomInOrthographicSize, zoomDuration).SetEase(Ease.InOutQuad));
        zoomSequence.Join(mainCamera.transform.DOMove(zoomInPosition, zoomDuration).SetEase(Ease.InOutQuad));
    }

    [ContextMenu("Zoom Out")]
    public void ZoomOut()
    {
        // Ensure the camera starts in its original position
        mainCamera.transform.position = zoomInPosition;

        // Zoom in by reducing the orthographic size and move the camera to the target position
        Sequence zoomSequence = DOTween.Sequence();
        zoomSequence.Append(mainCamera.DOOrthoSize(zoomOutOrthographicSize, zoomDuration).SetEase(Ease.InOutQuad));
        zoomSequence.Join(mainCamera.transform.DOMove(zoomOutPosition, zoomDuration).SetEase(Ease.InOutQuad));
    }

    [ContextMenu("EndZoom")]
    public void EndScreenZoom()
    {
        // Ensure the camera starts in its original position
        mainCamera.transform.position = zoomOutPosition;

        // Zoom in by reducing the orthographic size and move the camera to the target position
        Sequence zoomSequence = DOTween.Sequence();
        zoomSequence.Append(mainCamera.DOOrthoSize(endZoomSize, zoomDuration).SetEase(Ease.InOutQuad));
        zoomSequence.Join(mainCamera.transform.DOMove(endZoomPosition, zoomDuration).SetEase(Ease.InOutQuad));
    }
}
