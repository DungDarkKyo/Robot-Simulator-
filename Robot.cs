using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    public class Robot
    {
        private IList<string> _validFacing = new List<string> { "WEST", "NORTH", "EAST", "SOUTH" };

        private Dictionary<string, (int x, int y)> _validMove = new()
        {
            { "NORTH", (0,1) },
            { "SOUTH", (0,-1) },
            { "EAST", (1,0) },
            { "WEST", (-1,0) },
        };

        private int _currentX = -1;
        private int _currentY = -1;
        private string _currentFacing = string.Empty;

        public Robot()
        {
        }

        public override string ToString()
        {
            return $"X:{_currentX}, Y:{_currentY}, F:{_currentFacing}";
        }

        private bool IsPositionValid(int x, int y)
        {
            if (x < 0 || x > 5)
            {
                return false;
            }

            if (y < 0 || y > 5)
            {
                return false;
            }

            return true;
        }

        private bool IsMoveValid(int x, int y, string facing)
        {
            if (!_validMove.ContainsKey(facing))
            {
                return false;
            }

            var validMove = _validMove[facing];

            if (!IsPositionValid(x + validMove.x, y + validMove.y))
            {
                return false;
            }

            return true;
        }

        private bool IsPlacingValid(int x, int y, string facing)
        {
            if (!IsPositionValid(x, y))
            {
                return false;
            }

            if (_validFacing.IndexOf(facing) <= -1)
            {
                return false;
            }

            return true;
        }

        public bool Placing(int x, int y, string facing)
        {
            if (IsPlacingValid(x, y, facing))
            {
                this._currentX = x;
                this._currentY = y;
                this._currentFacing = facing;
                return true;
            }

            return false;
        }

        public bool Move()
        {
            if (IsMoveValid(_currentX, _currentY, _currentFacing))
            {
                var validMove = _validMove[_currentFacing];

                _currentX += validMove.x;
                _currentY += validMove.y;

                return true;
            }

            return false;
        }

        public bool Left()
        {
            if (_validFacing.IndexOf(_currentFacing) >= 0)
            {
                var facingIndex = _validFacing.IndexOf(_currentFacing);

                var newFacingIndex = facingIndex - 1;

                newFacingIndex = newFacingIndex < 0 ? _validFacing.Count - 1 : newFacingIndex;

                _currentFacing = _validFacing[newFacingIndex];

                return true;
            }

            return false;
        }

        public bool Right()
        {
            if (_validFacing.IndexOf(_currentFacing) >= 0)
            {
                var facingIndex = _validFacing.IndexOf(_currentFacing);

                var newFacingIndex = facingIndex + 1;

                newFacingIndex = newFacingIndex > _validFacing.Count - 1 ? 0 : newFacingIndex;

                _currentFacing = _validFacing[newFacingIndex];

                return true;
            }

            return false;
        }
    }
}