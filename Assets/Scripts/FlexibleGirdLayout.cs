using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGirdLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;
        //float parentHeight = 1000f;

        float cellWidth = parentWidth / (float)columns;
        float cellHeight = parentHeight / (float)rows;
        //float cellHeight = 200f;

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}
