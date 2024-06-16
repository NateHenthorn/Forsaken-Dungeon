using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTable : MonoBehaviour
{
    public static float currentYOffset = 0f;
    public static StatTable Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    public Image block;
    public int numEntitiesAlive = 0;
    public int numEntities = 0;
    public StatBlock statBlock;
    private Canvas canvas;
    private List<StatBlock> statBlocks = new List<StatBlock>();
    private int statBlockNum = 0;
     
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public int getStatBlockNum()
    {
        return statBlockNum++;
    }

    public StatBlock makeStatBlock()
    {
        if (statBlock != null && canvas != null)
        {
            StatBlock newStatBlock = Instantiate(statBlock, canvas.transform);
            RectTransform statBlockRect = newStatBlock.GetComponent<RectTransform>();

            statBlockRect.anchorMin = new Vector2(1, 1);
            statBlockRect.anchorMax = new Vector2(1, 1);
            statBlockRect.pivot = new Vector2(1, 1);

            // Insert the new stat block into the list in the correct position based on initiative
            int insertIndex = 0;
            while (insertIndex < statBlocks.Count && statBlocks[insertIndex].initi >= newStatBlock.initi)
            {
                insertIndex++;
            }
            statBlocks.Insert(insertIndex, newStatBlock);

            // Reposition all stat blocks
            RepositionStatBlocks();

            return newStatBlock;
        }
        else
        {
            return null;
        }
    }

    public void updateStatTable(int statNum)
    {
        if (statNum >= 0 && statNum < statBlocks.Count)
        {
            // Destroy the stat block to be removed
            Destroy(statBlocks[statNum].gameObject);

            // Remove the stat block from the list
            statBlocks.RemoveAt(statNum);

            // Reposition all stat blocks
            RepositionStatBlocks();
        }
    }

    private void RepositionStatBlocks()
    {
        currentYOffset = 0f;
        foreach (StatBlock block in statBlocks)
        {
            RectTransform statBlockRect = block.GetComponent<RectTransform>();
            statBlockRect.anchoredPosition = new Vector2(0, -currentYOffset);
            currentYOffset += statBlockRect.sizeDelta.y + 10f;
        }
    }
}