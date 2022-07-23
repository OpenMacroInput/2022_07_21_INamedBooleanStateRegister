using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicoINamedBooleanStateRegister : INamedBooleanStateRegisterSettable
{
    public Dictionary<string, bool> m_register = new Dictionary<string, bool>();
    public Dictionary<string, INamedBooleanStateRegisterGet.GetBooleanOfTarget> m_registerGet = new Dictionary<string, INamedBooleanStateRegisterGet.GetBooleanOfTarget>();
    public Dictionary<string, INamedBooleanStateRegisterSet.SetBooleanOfTarget> m_registerSet = new Dictionary<string, INamedBooleanStateRegisterSet.SetBooleanOfTarget>();

    public bool Contain(in string nameofboolean)
    {
        return m_register.ContainsKey(nameofboolean);
    }

    public void CreateDirectAccessToValue(in string nameofboolean, out INamedBooleanStateRegisterGet.GetBooleanOfTarget access)
    {
        if (!m_registerGet.ContainsKey(nameofboolean)) {
            string id = nameofboolean.ToString();
            INamedBooleanStateRegisterGet.GetBooleanOfTarget m = () => { return m_register[id]; };
            m_registerGet.Add(id, m); 
        }
        access = m_registerGet[nameofboolean];
        
    }
    
    public void CreateDirectAccessToValue(in string nameofboolean, out INamedBooleanStateRegisterSet.SetBooleanOfTarget access)
    {
        if (!m_registerSet.ContainsKey(nameofboolean))
        {
            string id = nameofboolean.ToString();
            INamedBooleanStateRegisterSet.SetBooleanOfTarget m = (in bool newValue) => { m_register[id]= newValue; };
            m_registerSet.Add(id, m);
        }
        access = m_registerSet[nameofboolean];
    }

    public void Get(in string nameofboolean, out bool contain, out bool value)
    {
        contain = m_register.ContainsKey(nameofboolean);
        if (contain)
            value = m_register[nameofboolean];
        else value = false;
    }

    public void Set(in bool value, in bool createItIfNotFound, params string[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] != null)
            {
                Set(in names[i], in value, in createItIfNotFound);
            }
        }
    }

    public void Set(in bool value, in bool createItIfNotFound,  IEnumerable<string> names)
    {
        foreach (var item in names)
        {
            Set(in item, in value, in createItIfNotFound);
        }
    }

    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound)
    {
        if (Contain(in nameofboolean))
        {
            m_register[nameofboolean] = value;
        }
        else {
            if (createItIfNotFound) {
                m_register.Add(nameofboolean, value);
            }
        }
    }

    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound, out bool hasChanged)
    {
        if (Contain(in nameofboolean))
        {
            bool previous = m_register[nameofboolean];
            m_register[nameofboolean] = value;
            hasChanged = previous != value;
            return;
        }
        else
        {
            if (createItIfNotFound)
            {
                hasChanged = true;
                m_register.Add(nameofboolean, value);
                return;
            }
        }
        hasChanged = false;

    }
}
