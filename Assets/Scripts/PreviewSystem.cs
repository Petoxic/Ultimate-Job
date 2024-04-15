using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0f;
    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObject;
    [SerializeField] private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;
    private Renderer cellIndicatorRenderer;

    public void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void Update()
    {
        if (PlacementSystem.isPreview && (Input.GetKeyDown(KeyCode.Escape)))
        {
            StopShowingPlacementPreview();
        }
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        PlacementSystem.isPreview = true;
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    public void StopShowingPlacementPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        PlacementSystem.isPreview = false;
    }

    public void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(DataManager.cellSize, DataManager.cellSize, 1);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }
        MoveCursor(position);
        ApplyFeedbackToCursor(validity);
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y, position.z);
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    public void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
    }
}
