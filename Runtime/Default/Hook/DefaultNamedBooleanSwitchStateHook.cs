using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultNamedBooleanSwitchStateHook : INamedBooleanSwitchStateHook
{
    public void AddListernerTo(in string namedBoolean, in INamedBooleanSwitchStateHookListener.BooleanChanged callback)
    {

        CheckExistantOfListener(in namedBoolean);
        m_booleanChangedListeners[namedBoolean] += callback;
    }
    public void RemoveListenerTo(in string namedBoolean, in INamedBooleanSwitchStateHookListener.BooleanChanged callback)
    {
        CheckExistantOfListener(in namedBoolean);
        m_booleanChangedListeners[namedBoolean] -= callback;
    }
    public void NotifyChanged(in string namedBoolean, in bool newValue)
    {
        CheckExistantOfListener(in namedBoolean);
        if (m_booleanChangedListeners[namedBoolean] != null)
            m_booleanChangedListeners[namedBoolean].Invoke(in namedBoolean, in newValue);
    }

   
    public void CheckExistantOfListener(in string namedBoolean) {
        if (m_booleanChangedListeners.ContainsKey(namedBoolean)) { }
        else m_booleanChangedListeners.Add(namedBoolean, null);
    }

    public Dictionary<string, INamedBooleanSwitchStateHookListener.BooleanChanged> m_booleanChangedListeners= new Dictionary<string, INamedBooleanSwitchStateHookListener.BooleanChanged>();
}
