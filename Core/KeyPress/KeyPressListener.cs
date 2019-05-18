using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uriel.KeyPress
{
    public class KeyPressListener
    {
        public Keys CurrentKeys { get; set; }

        public void KeyDown(object o, KeyEventArgs e)
        {
            CurrentKeys = e.KeyCode;
        }

        public void KeyUp(object o, KeyEventArgs e)
        {
            CurrentKeys = e.KeyCode;
        } 
    }

    public class KeyInterpreter
    {
        private Vertex3f GetMovement(Keys keys)
        {
            var movement = new Vertex3f();

            if (keys == Keys.NumPad8 || keys == Keys.NumPad7 || keys == Keys.NumPad9)
            {
                movement.y = 1;
            }
            else if (keys == Keys.NumPad2 || keys == Keys.NumPad1 || keys == Keys.NumPad3)
            {
                movement.y = -1;
            }
            else
            {
                movement.y = 0;
            }

            if (keys ==Keys.NumPad4  || keys == Keys.NumPad7 || keys == Keys.NumPad1)
            {
                movement.x = -1;
            }
            else if (keys == Keys.NumPad6 || keys == Keys.NumPad3 || keys == Keys.NumPad9)
            {
                movement.x = 1;
            }
            else
            {
                movement.x = 0;
            }

            if (keys == Keys.Subtract)
            {
                movement.z = -1;
            }
            else if (keys == Keys.Add)
            {
                movement.z = 1;
            }
            else
            {
                movement.z = 0;
            }

            return movement;
        }

        private Vertex3f UpdatePosition(Vertex3f oldPosition, Vertex3f newMovement, float multiplier)
        {
            Vertex3f newPosition = new Vertex3f();
            newPosition = oldPosition + newMovement * multiplier;
            return newPosition;
        }

        public TotalKeyState Update(TotalKeyState old, Keys keys)
        {
            if (keys == Keys.R)
            {
                return new TotalKeyState();
            }
            else
            {
                var newMovement = GetMovement(keys);
                var newPosition = UpdatePosition(old.Position, newMovement, .1f);
                return new TotalKeyState() { Position = newPosition, Movement = newMovement };
            }
        }

    }

    public struct TotalKeyState
    {
        public Vertex3f Position { get; set; }

        public Vertex3f Movement { get; set; }
    }
}
