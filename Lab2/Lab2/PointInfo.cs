using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class PointInfo
    {
        int x;
        int y;
        Boolean isRed;
        Boolean isClear;

        public PointInfo(int x_, int y_)
        {
            this.x = x_;
            this.y = y_;
            this.isRed = false;
            this.isClear = false;
        }

        public Boolean getClear()
        {
            return this.isClear;
        }
        public void makeClear()
        {
            this.isRed = false;
            this.isClear = true;
        }
        public void unClear()
        {
            this.isClear = false;
        }

        public void makeBlack()
        {
            this.isClear = false;
        }

        public void makeRed() {
            this.isRed = true;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public Boolean getRed()
        {
            return this.isRed;
        }

    }
}
