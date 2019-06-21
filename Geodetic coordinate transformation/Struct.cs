using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geodetic_coordinate_transformation
{
    class Struct
    {
        public struct Ellipse
        {
            public string Name;//椭球名称
            public double _a;//长半径
            public double _b;//短半径
            public double _e2;//第二偏心率平方
            public double _e1;//第一偏心率平方
            public double _f;//椭球扁率
            public double _c;//极曲率半径
        }
        public static Ellipse SetEllipse(int _izwhat)
        {
            Ellipse _re = new Ellipse();
            switch (_izwhat)
            {
                case 0:
                    {
                        _re.Name = "克拉索夫斯基椭球";
                        _re._a = 6378245.0000;
                        _re._b = 6356863.01877;
                        _re._e2 = 0.00673852541468;
                        break;
                    }
                case 1:
                    {
                        _re.Name = "Bessel椭球";
                        _re._a = 6377397.155;
                        _re._b = 6356078.963;
                        _re._e2 = 0.0067192188;
                        break;
                    }
                case 2:
                    {
                        _re.Name = "西安80/1975年国际椭球";
                        _re._a = 6378140.0000;
                        _re._b = 6356755.288158;
                        _re._e2 = 0.00673950182;
                        break;
                    }
                case 3:
                    {
                        _re.Name = "WGS-84椭球";
                        _re._a = 6378137.0000;
                        _re._b = 6356752.3142;
                        _re._e2 = (Math.Pow(_re._a, 2) - Math.Pow(_re._b, 2)) / Math.Pow(_re._b, 2);
                        break;
                    }
            }
            _re._e1 = (Math.Pow(_re._a, 2) - Math.Pow(_re._b, 2)) / Math.Pow(_re._a, 2);//计算第一偏心率
            _re._f = (_re._a - _re._b) / _re._a;
            _re._c = Math.Pow(_re._a, 2) / _re._b;
            return _re;
        }
    }
}
