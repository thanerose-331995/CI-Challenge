using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleChoose : MonoBehaviour, IPointerClickHandler
{
    public string selected;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked: " + this.gameObject.name);
        ModuleEquip.moduleName = this.gameObject.name;
        ModuleDescription.ShowInfo(this.gameObject.name);
        RobotInfo.ChangeBars(this.gameObject.name);
    }
}