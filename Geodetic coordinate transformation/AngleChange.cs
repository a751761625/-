using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geodetic_coordinate_transformation
{
    public abstract class AngleConvert
    {
        private double angle;
        public double Angle
        {
            set
            {
                angle = value;
            }
            get
            {
                return angle;
            }
        }
        public double GetAngle
        {
            get
            {
                return Convert(angle);
            }
        }
        public abstract double Convert(double angle);
    }

    public class DmsToRad : AngleConvert
    {
        public override double Convert(double angle)
        {
            DmsToDec dc = new DmsToDec();
            dc.Angle = angle;
            double dt = dc.GetAngle;
            return dt * Math.PI / 180.0;
        }

        public static implicit operator RadToDms(DmsToRad dr)
        {
            RadToDms rd = new RadToDms();
            rd.Angle = dr.GetAngle;
            return rd;
        }
    }

    public class DmsToDec : AngleConvert
    {
        public override double Convert(double angle)
        {
            double f, d, mm, ms, ss, ss1;
            if (angle != 0)
                f = angle / Math.Abs(angle);
            else
                f = 1;
            angle = Math.Abs(angle);
            d = (int)angle;
            mm = angle - d + 1.0e-12;
            ms = (int)(mm * 100) * 60;
            ss = (mm * 100 - (int)(mm * 100) + 1.0e-12) * 100;
            ss1 = (ms + ss) / 3600 + d;
            return ss1 * f;
        }
    }

    public class RadToDms : AngleConvert
    {
        public override double Convert(double angle)
        {
            double sign, d, m, ms, s;
            if (angle != 0)
                sign = angle / Math.Abs(angle);
            else
                sign = 1;
            angle = (angle * 180) / (Math.PI + 3e-16);
            angle = Math.Abs(angle);
            d = (int)(angle);
            m = angle - d;
            ms = (int)(m * 60);
            s = (m * 60 - (int)(m * 60)) * 60;
            return (d + ms / 100 + s / 10000) * sign;

        }
    }

    public class DecToDms : AngleConvert
    {
        public override double Convert(double angle)
        {
            double sign, d, m, ms, s;
            if (angle != 0)
                sign = angle / Math.Abs(angle);
            else
                sign = 1;
            angle = Math.Abs(angle);
            d = (int)(angle);
            m = angle - d;
            ms = (int)(m * 60);
            s = (m * 60 - (int)(m * 60)) * 60;
            return (d + ms / 100 + s / 10000) * sign;
        }
    }

    public class DecToRad : AngleConvert
    {
        public override double Convert(double angle)
        {
            return (angle * (Math.PI - 3e-16)) / 180;
        }

    }

    public class RadToDec : AngleConvert
    {
        public override double Convert(double angle)
        {
            return (angle * 180.0) / (Math.PI - 3e-16);
        }
    }

    public class AngleContext
    {
        AngleConvert ac;
        bool bok;
        public AngleContext(AngleConvert ac)
        {
            this.ac = ac;
            bok = true;
        }

        public AngleContext(int iselect)
        {
            bok = true;
            switch (iselect)
            {
                case 0:
                    {
                        ac = new DecToDms();
                        break;
                    }
                case 1:
                    {
                        ac = new DmsToDec();
                        break;
                    }
                case 2:
                    {
                        ac = new RadToDec();
                        break;
                    }
                case 3:
                    {
                        ac = new DecToRad();
                        break;
                    }
                default:
                    {
                        bok = false;
                        break;
                    }
            }
        }

        public double GetResult(double angle)
        {
            if (bok)
            {
                ac.Angle = angle;
                return ac.GetAngle;
            }
            else
            {
                return -1;
            }
        }
    }
}
