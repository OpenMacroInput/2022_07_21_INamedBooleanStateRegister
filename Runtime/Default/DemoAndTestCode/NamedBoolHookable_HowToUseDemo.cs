using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedBoolHookable_HowToUseDemo : MonoBehaviour
{
    public INamedBooleanStateRegisterSetAndHook m_register = new DicoINamedBooleanStateRegisterWithHook();


    public INamedBooleanStateRegisterGet.GetBooleanOfTarget getTestA;
    public INamedBooleanStateRegisterSet.SetBooleanOfTarget setTestA;
    void Start()
    {

        m_register.CreateDirectAccessToValue("TestA", out getTestA);
        m_register.CreateDirectAccessToValue("TestA", out setTestA);
        m_register.AddListernerTo("TestA", BoolChanged);
        m_register.Set("TestA", true, true);
        m_register.Set("TestA", true, true);
        m_register.Set("TestA", true, true);
        Display("TestA");
        m_register.Set("TestA", false, true);
        Display("TestA");

        Debug.Log(">> direct Test");
        Display(in getTestA);
        setTestA(true);
        Display(in getTestA);
        setTestA(false);
        Display(in getTestA);

        m_register.RemoveListenerTo("TestA", BoolChanged);
    }

    void BoolChanged(in string name, in bool value)
    {
        Debug.Log(string.Format("Changed {0}:{1}->{2}", name, !value, value));
    }

    void Display(in string name)
    {
        m_register.Get(name, out bool isContain, out bool value);
        Debug.Log(string.Format("Bool {0}:{1}->{2}", name, isContain, value));
    }
    void Display(in INamedBooleanStateRegisterGet.GetBooleanOfTarget access)
    {

        bool value = access();
        Debug.Log(string.Format("Direct Access {0}:{1}", name, value));
    }
}
