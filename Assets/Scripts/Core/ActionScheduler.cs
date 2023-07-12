using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            if(_currentAction != null)
            {
                _currentAction.Cansel();
            }
            _currentAction = action;
        }
        
        public void CancelCurrenAction()
        {
            StartAction(null);
        }
    }
}
