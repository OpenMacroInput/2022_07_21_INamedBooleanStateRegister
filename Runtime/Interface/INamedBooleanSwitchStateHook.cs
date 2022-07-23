using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INamedBooleanSwitchStateHook : INamedBooleanSwitchStateHookNotify, INamedBooleanSwitchStateHookListener
{
    
}

public interface INamedBooleanSwitchStateHookListener
{
    public delegate void BooleanChanged(in string namedBoolean, in bool newValue);
    public void AddListernerTo(in string namedBoolean, in BooleanChanged callback);
    public void RemoveListenerTo(in string namedBoolean, in BooleanChanged callback);
}

public interface INamedBooleanSwitchStateHookNotify
{
    public void NotifyChanged(in string namedBoolean, in bool newValue);
}