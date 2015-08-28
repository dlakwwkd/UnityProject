﻿using UnityEngine;
using System.Collections.Generic;

public class ContainerSetting : MonoBehaviour
{
    public GameObject itemType = null;

    GameObject  _grid   = null;
    UIWidget    _widget = null;
    bool        _isOn   = false;

    void        Start()
    {
        _widget = GetComponent<UIWidget>();
        _widget.alpha = 0.0f;

        _grid = transform.FindChild("Scroll View").FindChild("Grid").gameObject;
    }

    public void OnOff()
    {
        if(_isOn)
        {
            _widget.alpha = 0.0f;
            _isOn = false;
        }
        else
        {
            _widget.alpha = 1.0f;
            _isOn = true;
        }
    }
    public void SettingItems(List<ItemInfo> itemList)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            var itemInfo = itemList[i];

            var newItem = Instantiate(itemType) as GameObject;
            newItem.transform.SetParent(_grid.transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.identity;
            newItem.transform.localScale = Vector3.one;
            newItem.name = itemType.name + "_" + i;

            var dragDrop = newItem.GetComponent<DragDropItem>();
            dragDrop.prefab = itemInfo.item;
            dragDrop.amount = itemInfo.amount;
            dragDrop.xSize = itemInfo.xSize;
            dragDrop.ySize = itemInfo.ySize;

            var icon = Instantiate(itemInfo.icon) as GameObject;
            icon.transform.SetParent(newItem.transform);
            icon.transform.localPosition = Vector3.zero;
            icon.transform.localRotation = Quaternion.identity;
            icon.transform.localScale = Vector3.one;

            var sprite = icon.GetComponent<UISprite>();
            var aspect = itemInfo.xSize / itemInfo.ySize;
            var w = sprite.width;
            var h = sprite.height;
            if (aspect > 1)
                h = (int)(sprite.height / aspect);
            else
                w = (int)(sprite.width * aspect);
            sprite.SetDimensions(w, h);
        }
    }
}