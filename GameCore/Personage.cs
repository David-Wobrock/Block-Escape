using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class Personage : UIObject
    {
        private static int MoveSpeed = 20;
        private static int JumpSpeed = 40;
        private static int GravityAction = 2;

        private Boolean IsJumping = false;
        private int ActuelJumpSpeed = 0;
        private int InitialJumpPosition = 0;

        public Personage(int x, int y)
            : base(x, y)
        {
            ImageName = "Personge_Immobile.jpg";
            Height = 18;
            Width = 18;
        }

        public void Move(List<Action> a)
        {
            UpdateImage(a.First());

            if (a.Contains(Action.Left))
                MoveLeft();
            else if (a.Contains(Action.Right))
                MoveRight();

            JumpingAction(a.Contains(Action.Jump));
        }

        private void MoveLeft()
        {
            // Next move is going the put the personage out of the window
            if (X - MoveSpeed < 0)
                X = 0;
            else
                X -= MoveSpeed;
        }

        private void MoveRight()
        {
            // Next move is goigng to put the personage out of the window (13 = error margin)
            if (X + Width + 13 + MoveSpeed > 1000)
                X = 1000 - 13 - Width;
            else
                X += MoveSpeed;
        }

        /// <summary>
        /// Handles the jump
        /// </summary>
        /// <param name="jumpButtonPressed">Boolean : if the Up button is pressed</param>
        private void JumpingAction(bool jumpButtonPressed)
        {
            // Begin the jump
            if (!IsJumping && jumpButtonPressed)
            {
                InitialJumpPosition = Y;
                ActuelJumpSpeed = JumpSpeed;
                IsJumping = true;
            }
            // If the jump is happening
            if (IsJumping)
            {
                Y -= ActuelJumpSpeed;
                ActuelJumpSpeed -= GravityAction;
            }

            // End the jump
            if (IsJumping && Y == InitialJumpPosition)
                IsJumping = false;
        }

        private void UpdateImage(Action a)
        {
            if (IsJumping)
            {
                if (ActuelJumpSpeed > 0)
                    ImageName = "Personage_Jumping_Up.jpg";
                else
                    ImageName = "Personage_Jumping_Down.jpg";
            }
            else if (a == Action.Left)
                ImageName = "Personage_Moving_Left.jpg";
            else if (a == Action.Right)
                ImageName = "Personage_Moving_Right.jpg";
            else
                ImageName = "Personage_Immobile.jpg";
        }
    }
}
