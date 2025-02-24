using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private LineRenderer lineRenderer;

    private void Start()
    {
        startColor = sr.color;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        lineRenderer.enabled = true;
        lineRenderer.widthMultiplier = 0.05f;

        if (towerToBuild != null && tower == null)
        {
            if (towerToBuild.name == "Slowmo Turret")
            {
                DrawSquareRange(towerToBuild.targetingRange);
            }
            else
            {
                DrawCircleRange(towerToBuild.targetingRange);
            }
        }
    }        

    private void DrawCircleRange(float range)
    {
        int subdivistions = 36;
        float angleStep = 2f * Mathf.PI / subdivistions;

        lineRenderer.positionCount = subdivistions;

        for (int i = 0; i < subdivistions; i++)
        {
            float xPosition = range * Mathf.Cos(angleStep * i);
            float yPosition = range * Mathf.Sin(angleStep * i);

            Vector2 circlePoint = new Vector2(xPosition + transform.position.x, yPosition + transform.position.y);
            lineRenderer.SetPosition(i, circlePoint);
        }
    }

    private void DrawSquareRange(float range)
    {
        lineRenderer.positionCount = 4;

        lineRenderer.SetPosition(0, new Vector3(transform.position.x - range, transform.position.y + range, 0f));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x + range, transform.position.y + range, 0f));
        lineRenderer.SetPosition(2, new Vector3(transform.position.x + range, transform.position.y - range, 0f));
        lineRenderer.SetPosition(3, new Vector3(transform.position.x - range, transform.position.y - range, 0f));
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
        lineRenderer.enabled = false;
    }

    // Place selected tower
    private void OnMouseDown()
    {
        // Check there is no tower on the plot
        if (tower != null)
        {
            return;
        }

        // Check the mouse is not on the menu
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Check the player has enough currency and build if so
        Tower towerToBuld = BuildManager.main.GetSelectedTower();
        if (LevelManager.main.SpendCurrency(towerToBuld.cost))
        {
            tower = Instantiate(towerToBuld.prefab, transform.position, Quaternion.identity);
        }
    }
}
