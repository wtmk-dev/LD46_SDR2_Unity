using System;
public interface IState 
{
    void StateChange();
    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();
}
