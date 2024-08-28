using System;

internal interface IEventBinding<T>
{
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }
}
public class EventBingding<T> : IEventBinding<T> where T:IEvent
{
    //防止空引用
    Action<T> OnEvent = _ => { };
    Action OnEventNoArgs = () => {};
    
    Action<T> IEventBinding<T>.OnEvent { get => OnEvent; set => OnEvent = value; }
    Action IEventBinding<T>.OnEventNoArgs { get => OnEventNoArgs; set => OnEventNoArgs = value; }

    public EventBingding(Action<T> onEvent) => this.OnEvent = onEvent;
    public EventBingding(Action onEventNoArgs) => this.OnEventNoArgs = onEventNoArgs;
    
    public void Add(Action onEvent) => OnEventNoArgs += onEvent;
    public void Remove(Action onEvent) => OnEventNoArgs -= onEvent;
    
    public void Add(Action<T> onEvent) => OnEvent += onEvent;
    public void Remove(Action<T> onEvent) => OnEvent -= onEvent;
}