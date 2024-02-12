using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Scripts.General.StateMachine
{
    public interface IState
    {
        //public event Action OnFinished;
        public void Enter();
        public void Update();
        public void Exit();
    }
}
