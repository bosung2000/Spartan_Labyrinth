using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    //condition이라는 틀에대한 정보를 받고 UI에 보여주는 역활 
    Condition condition; //구성 틀 =condition
    public Image UiBar;


    private void Start()
    {
        condition =new Condition() {curValue= 0,startValue= 100,maxValue= 100};

        condition.curValue = condition.startValue;
    }

    private void Update()
    {
        UiBar.fillAmount = GetPercentage();
    }

    private void Settcondition(Condition _condition)
    {
        condition = _condition;
    }

    private void Getcondition()
    {
        //condition 이라는 값을 UI에 표시해주는 역활 
    }
    float GetPercentage()
    {
        return condition.curValue / condition.maxValue;
    }
    public void Add(float value)
    {
        condition.curValue = Mathf.Min(condition.curValue + value, condition.maxValue);
    }

    public void Subtract(float value)
    {
        condition.curValue = Mathf.Max(condition.curValue - value, 0);
    }
}
