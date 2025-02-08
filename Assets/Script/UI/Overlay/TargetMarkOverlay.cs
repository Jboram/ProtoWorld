using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoWorld
{
    public class TargetMarkOverlay : BaseUI
    {
        public Image imgFill; //public은 다른 클래스에서도 해당 클래스 호출시 공개적으로 호출됨
        [SerializeField] private Image fillImage; //serializedField는 다른 클래스에 공개되지 않고 inspector에만 노출됨
        //1. 현재 게임브젝트 하위의 자식에서, Fill이라는 게임 오브젝트를 찾는다. (또는 transform을 찾는다)
            //GamUtil.Bind()
        //2. public으로 inspector에 노출한다.
        //3. private인데 [SerializedField]로 노출
    }
}