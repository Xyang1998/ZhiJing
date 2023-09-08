using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("FungusEX","MyMenu","选项扩展")]


//对话
public class MyMenu : MySay
{
    public List<MenuData> menuList
    {
        get;
        private set;
    }
    public void InitMenuList(List<MenuData> menuData)
    {
        menuList = menuData;
    }
    

    
}
