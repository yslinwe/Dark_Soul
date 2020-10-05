using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class UIManage : MonoBehaviour
    {
       public GameObject selectWindow;
       public GameObject inventoryWindow;
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }
        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
        public void OpenInventoryWindow()
        {
            inventoryWindow.SetActive(true);
        }
        public void CloseInventoryWindow()
        {
            inventoryWindow.SetActive(false);
        }
    }
}

