﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct UnitInfo
{
    public GameObject item;
    public GameObject icon;
}

public class UnitSetting : MonoBehaviour
{
    //-----------------------------------------------------------------------------------
    // inspector field
    public GameObject ItemType = null;

    //-----------------------------------------------------------------------------------
    // handler functions
    void Start()
    {
        grid = transform.FindChild("Grid").gameObject;
    }

    //-----------------------------------------------------------------------------------
    // public functions
    public void SettingUnits(List<UnitInfo> itemList)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            var info = itemList[i];
            var slot = grid.transform.FindChild("UnitSlot_" + i.ToString());

            var newItem = Instantiate(ItemType) as GameObject;
            newItem.transform.SetParent(slot.transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.localRotation = Quaternion.identity;
            newItem.transform.localScale = Vector3.one;
            newItem.name = ItemType.name + "_" + i;
            newItem.GetComponent<UISprite>().depth = 2;

            var itemInfo = newItem.GetComponent<UnitItemInfo>();
            itemInfo.Prefab = info.item;

            var icon = Instantiate(info.icon) as GameObject;
            icon.transform.SetParent(newItem.transform);
            icon.transform.localPosition = Vector3.zero;
            icon.transform.localRotation = Quaternion.identity;
            icon.transform.localScale = Vector3.one;
            icon.GetComponent<UITexture>().depth = 3;
        }
    }

    //-----------------------------------------------------------------------------------
    // private field
    GameObject grid = null;
}
