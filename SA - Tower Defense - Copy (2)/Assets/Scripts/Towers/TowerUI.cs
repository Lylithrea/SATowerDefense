using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image background;
    public Image icon;
    public Text costText;
    private int cost;
    private GameObject pointer;

    private SO_Tower soTower;

    public Tilemap tilemap;
    public TileBase towerBase;

    public GameObject hoverObject;

    public void Setup(SO_Tower tower)
    {
        soTower = tower;
        icon.sprite = tower.icon;
        costText.text = "$" + tower.cost.ToString();
        cost = tower.cost;
        GameManager.onMoneyUpdate += checkMoney;
        checkMoney();
    }


    public void checkMoney()
    {
        if(GameManager.playerMoney >= cost)
        {
            setAlphaOfImages(1);
        }
        else
        {
            setAlphaOfImages(0.5f);
        }
    }

    private void setAlphaOfImages(float alpha)
    {
        Color iconColor = icon.color;
        iconColor.a = alpha;
        icon.color = iconColor;
        Color backgroundColor = background.color;
        backgroundColor.a = alpha;
        background.color = backgroundColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        pointer = Instantiate(hoverObject);
        pointer.gameObject.transform.Find("Icon").GetComponent<Image>().sprite = icon.sprite;
        pointer.gameObject.transform.Find("Radius").transform.localScale = new Vector3(soTower.radius, soTower.radius, soTower.radius);
        pointer.transform.SetParent(this.transform.parent.parent);
        pointer.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3Int cellPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(eventData.position));
        cellPos.z = 0;
        Vector3 newPos = cellPos;
        newPos.x += 0.5f;
        newPos.y += 0.5f;
        pointer.transform.position = Camera.main.WorldToScreenPoint(newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(pointer.gameObject);
        Vector3Int cellPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(eventData.position));
        cellPos.z = 0;
        if (tilemap.GetTile(cellPos) == towerBase)
        {
            GameManager.towerHandler.CreateTower(tilemap.CellToWorld(cellPos), soTower);
        }
    }


}
