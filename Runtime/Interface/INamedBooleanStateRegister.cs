using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface INamedBooleanStateRegister : INamedBooleanStateRegisterGet{}
public interface INamedBooleanStateRegisterSettable : INamedBooleanStateRegister, INamedBooleanStateRegisterSet { }

public interface INamedBooleanStateRegisterSet
{
    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound);
    public void Set(in string nameofboolean, in bool value, in bool createItIfNotFound, out bool hasChanged);
    public delegate void SetBooleanOfTarget(in bool newValue);
    public void CreateDirectAccessToValue(in string nameofboolean, out SetBooleanOfTarget access);
}
public interface INamedBooleanStateRegisterGet 
{
    public bool Contain(in string nameofboolean);
    public void Get(in string nameofboolean, out bool contain, out bool value);

    public delegate bool GetBooleanOfTarget();
    /// <summary>
    /// If you plan to use a lot a boolean instead of eahc time asking if it exists. Check that it exists then create direct access that will try to go thest as possible to value.
    /// </summary>
    /// <param name="nameofboolean"></param>
    /// <param name="access"></param>
    public void CreateDirectAccessToValue(in string nameofboolean, out GetBooleanOfTarget access);
}




public abstract class INamedBooleanStateRegisterUtility
{
    public abstract void SetGroup(INamedBooleanStateRegisterSet target, in bool value, bool createIfNotExisting, params string[] names);
    public abstract void SetGroup(INamedBooleanStateRegisterSet target, in bool value, bool createIfNotExisting,  IEnumerable names);
    public abstract void SwitchGroup(INamedBooleanStateRegister target,  bool createIfNotExisting, bool startValueIfNotExisting, params string[] names);
}
