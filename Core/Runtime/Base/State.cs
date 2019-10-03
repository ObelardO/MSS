using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class State : Collection<Tween>
    {
        #region Properties

        //[SerializeField/*, HideInInspector*/]


        //private float s_Delay;


        public float Delay;// { set; get; }
        /*{
            set => s_Delay = value;
            get => s_Delay;
        }*/

        //[SerializeField/*, HideInInspector*/]
        //private float s_Duration = 1;
        public float Duration = 1;
        /*{
            set => s_Duration = value;
            get => s_Duration;
        }*/

        //[SerializeField/*, HideInInspector*/]
        //private string s_Name = "NewState";
        public override string Name
        {
            get
            {
                if (IsClosedState) return "closed";
                if (IsOpenedState) return "opened";
                return base.Name;
            }
            set
            {
                if (!IsDefaultState) base.Name = value;
            }
             
        }
        //першин кирилл борисович - эксимер
        //Юсеф - россолимо

        //[SerializeField/*, HideInInspector*/]
        /*private bool s_Enabled = true;
        public bool Enabled
        {
            private set => s_Enabled = value;
            get => IsDefaultState || s_Enabled;
        }
        */

        public bool IsClosedState => this == ((StatesGroup)Parent)?.ClosedState;
        public bool IsOpenedState => this == ((StatesGroup)Parent)?.OpenedState;
        public bool IsDefaultState => IsClosedState || IsOpenedState;

        #endregion
    }
}