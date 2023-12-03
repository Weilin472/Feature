using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make a kind of list that can store two kind of variables
public class AttackComboList<T,F>
{
    private List<T> listT = new List<T>();
    private List<F> listF = new List<F>();

    public void Add(T t,F f)
    {
        listT.Add(t);
        listF.Add(f);
    }

    public T GetFirstValue(int index)
    {
        return listT[index];
    }

    public F GetSecondValue(int index)
    {
        return listF[index];
    }

    public int Count
    {
        get { return listT.Count; }   
    }
}
