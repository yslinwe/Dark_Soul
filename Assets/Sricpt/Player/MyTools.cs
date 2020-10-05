using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class MyTools
    {
        bool upSideLastState;
        bool downSideLastState;
        public MyTools()
        {
            upSideLastState = new bool();
            upSideLastState = false;
            downSideLastState = new bool();
            downSideLastState = true;
        }
        public bool upSide(bool state)
        {
            if(state != upSideLastState&&upSideLastState!=true)
            {
                upSideLastState = state;
                return true;
            }
            return false;
        }
        public bool downSide(bool state)
        {
            if(state != downSideLastState&&downSideLastState!=false)
            {
                downSideLastState = state;
                return true;
            }
            return false;
        }
    }

}
