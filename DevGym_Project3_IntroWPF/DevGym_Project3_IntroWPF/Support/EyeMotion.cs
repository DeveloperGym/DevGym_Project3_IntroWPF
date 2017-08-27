using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DevGym_Project3_IntroWPF
{
    public class EyeMotion
    {
        #region Properties
        public Shape ObjectToMove { get; set; }
        public Canvas Parent { get; set; }

        public double MoveSpeed { get; set; }

        public bool MovingRight { get; set; }
        #endregion

        #region Construct / Destruct
        public EyeMotion(Shape objectToMove, Canvas parent, double moveSpeed = 1d, bool movingRight = true)
        {
            // Required Parameters
            ObjectToMove = objectToMove;
            Parent = parent;

            // Optional Parameters
            MoveSpeed = moveSpeed;
            MovingRight = movingRight;
        }
        #endregion

        #region Methods
        public void Update()
        {
            var Left = Canvas.GetLeft(ObjectToMove);
            if (MovingRight)
            {
                Left += MoveSpeed;
                var MaxLeft = (Parent.ActualWidth - ObjectToMove.ActualWidth);
                if (Left > MaxLeft)
                {
                    Left = MaxLeft;
                    MovingRight = false;
                }
            }
            else
            {
                Left -= MoveSpeed;
                if (Left <= 0)
                {
                    Left = 0;
                    MovingRight = true;
                }
            }
            Canvas.SetLeft(ObjectToMove, Left);
        }
        #endregion
    }
}
