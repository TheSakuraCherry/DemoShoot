using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus<T> where T : IEvent
{
    static readonly HashSet<IEventBinding<T>> bingdings = new HashSet<IEventBinding<T>>();
    
    public static void Register(EventBingding<T> binding) => bingdings.Add(binding);
    public static void DeRegister(EventBingding<T> binding) => bingdings.Remove(binding);

    public static void Raise(T @event)
    {
        // 创建临时副本用于遍历和删除操作
        var bindingsCopy = new HashSet<IEventBinding<T>>(bingdings);

        foreach (var binding in bindingsCopy)
        {
            binding.OnEvent.Invoke(@event);
            binding.OnEventNoArgs.Invoke();
            
        }
    }

    static void Clear()
    {
        bingdings.Clear();
    }
}