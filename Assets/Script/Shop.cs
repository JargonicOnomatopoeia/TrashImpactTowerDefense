using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class attribute
{
    public bool print;
    public int type;
    public int price;
    public Button TowerButton;
    public GameObject Tower;
}


public class Shop : MonoBehaviour {

    public List<attribute> buttons;
    private TurretBuildManager build;
    private GameObject buttonList;
    void Start()
    {   
        buttonList = GameObject.Find("Shop/Buttons");
        build = TurretBuildManager.tbm;
        loopInstantiate();
    }

    void InstantiateButton(int index)
    {
        Button cloneButton = (Button)Instantiate(buttons[index].TowerButton,buttonList.transform);
        cloneButton.GetComponentInChildren<Text>().text= buttons[index].price.ToString();
        cloneButton.onClick.AddListener(delegate{towerSelect(index);});
    }

    void towerSelect(int index){
        if(build.buy == false && Stats.Money >= buttons[index].price){
            build.turretToBuild = buttons[index].Tower;
            build.price = buttons[index].price;
            build.type = buttons[index].type;
            build.buy = true;
        }else{
            build.buy = false;
        }
    }

    void loopInstantiate()
    {
        for(int x=0;x != buttons.Count ; x++)
        {
            if(buttons[x].print != false){
                InstantiateButton(x);
            }
            
        }
    }
}
