using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicoINamedBooleanStateRegisterWithHook : INamedBooleanStateRegisterSetAndHook
{
    public INamedBooleanSwitchStateHook m_hookListener = new DefaultNamedBooleanSwitchStateHook();
    public INamedBooleanStateRegisterSettable m_register = new DicoINamedBooleanStateRegister();
    public Dictionary<string, INamedBooleanStateRegisterSet.SetBooleanOfTarget> m_registerSet = new Dictionary<string, INamedBooleanStateRegisterSet.SetBooleanOfTarget>();

    public void AddListernerTo(in string namedBoolean, in INamedBooleanSwitchStateHookListener.BooleanChanged callback)
    {
        m_hookListener.AddListernerTo(in namedBoolean, in callback);
    }

    public bool Contain(in string nameofboolean)
    {
        return m_register.Contain(in nameofboolean);
    }

    public void CreateDirectAccessToValue(in string nameofboolean, out INamedBooleanStateRegisterGet.GetBooleanOfTarget access)
    {
        m_register.CreateDirectAccessToValue(in nameofboolean, out access);
    }

    public void CreateDirectAccessToValue(in string nameofboolean, out INamedBooleanStateRegisterSet.SetBooleanOfTarget access)
    {
        if (!m_registerSet.ContainsKey(nameofboolean))
        {
            string id = nameofboolean.ToString();
            INamedBooleanStateRegisterSet.SetBooleanOfTarget m = (in bool newValue) => {
                m_register.Set(id, in newValue, true, out bool hasChanged);
                
                    if (hasChanged)
                    {
                        NotifyChanged(in id, in newValue);
                    }
            };
            m_registerSet.Add(id, m);
        }
        access = m_registerSet[nameofboolean];
    }

    public void Get(in string nameofboolean, out bool contain, out bool value)
    {
        m_register.Get(in nameofboolean, out  contain, out  value);
    }

    public void NotifyChanged(in string namedBoolean, in bool newValue)
    {
        m_hookListener.NotifyChanged(in namedBoolean, in newValue);
    }

    public void RemoveListenerTo(in string namedBoolean, in INamedBooleanSwitchStateHookListener.BooleanChanged callback)
    {
        m_hookListener.RemoveListenerTo(in namedBoolean, in callback);
    }

    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound)
    {
        Set(in nameofboolean, in value, in createItIfNotFound, out bool hasChanged) ;
    }

    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound, out bool hasChanged)
    {
        m_register.Set(in nameofboolean, in value, createItIfNotFound, out  hasChanged);

        if (hasChanged)
        {
            NotifyChanged(in nameofboolean, in value);
        }
    }
}
