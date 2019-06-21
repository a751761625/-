using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geodetic_coordinate_transformation
{
    
        class Gauss
        {
            private double a = 0;
            private double b = 0;
            private double e = 0;
            private double e2 = 0;
            public double x = 0;
            public double y = 0;
            public double B = 0;
            public double L = 0;
            public Gauss()
            {

            }
            public Gauss(double a, double b, double e, double e2)
            {
                this.a = a;
                this.b = b;
                this.e = e;
                this.e2 = e2;
            }
            public void GetGauss(double B, double L, double L0)//高斯正算
            {
                double a0 = 1 + 3.0 / 4 * Math.Pow(e, 2) + 45 / 64.0 * Math.Pow(e, 4) + 175 / 256.0 * Math.Pow(e, 6) + 11025 / 16384.0 * Math.Pow(e, 8) + 43659 / 65336.0 * Math.Pow(e, 10) + 693693 / 1048576.0 * Math.Pow(e, 12) + 2760615 / 41944304.0 * Math.Pow(e, 14) + 703956835 / 1073741824.0 * Math.Pow(e, 16);
                double a1 = 3.0 / 4 * Math.Pow(e, 2) + 15 / 16.0 * Math.Pow(e, 4) + 525 / 512.0 * Math.Pow(e, 6) + 2205 / 4096.0 * Math.Pow(e, 8) + 72765 / 65536.0 * Math.Pow(e, 10) + 297297 / 262144.0 * Math.Pow(e, 12) + 19324305 / 16777216.0 * Math.Pow(e, 14);
                double a2 = 15 / 64.0 * Math.Pow(e, 4) + 105 / 256.0 * Math.Pow(e, 6) + 2205 / 4096.0 * Math.Pow(e, 8) + 10395 / 16384.0 * Math.Pow(e, 10) + 1486485 / 2097152.0 * Math.Pow(e, 12) + 6441435 / 8388608.0 * Math.Pow(e, 14);
                double a3 = 35 / 512.0 * Math.Pow(e, 6) + 315 / 2048.0 * Math.Pow(e, 8) + 31185 / 131072.0 * Math.Pow(e, 10) + 165165 / 524288.0 * Math.Pow(e, 12) + 6441435 / 16777216.0 * Math.Pow(e, 14);
                double a4 = 315 / 16348.0 * Math.Pow(e, 8) + 3465 / 65536.0 * Math.Pow(e, 10) + 99099 / 1048576.0 * Math.Pow(e, 12) + 585585 / 4194304.0 * Math.Pow(e, 14);
                double a5 = 693 / 131072.0 * Math.Pow(e, 10) + 9009 / 524288.0 * Math.Pow(e, 12) + 585585 / 16777216.0 * Math.Pow(e, 14);
                double a6 = 3003 / 2097152.0 * Math.Pow(e, 12) + 45045 / 8388608.0 * Math.Pow(e, 14);
                double a7 = 6435 / 16777216.0 * Math.Pow(e, 14);
                double X = a * (1 - e * e) * (a0 * B - 0.5 * a1 * Math.Sin(2 * B) + 1 / 4.0 * a2 * Math.Sin(4 * B) - 1 / 6.0 * a3 * Math.Sin(6 * B) + 1 / 8.0 * a4 * Math.Sin(8 * B) - 1 / 10.0 * a5 * Math.Sin(10 * B) + 1 / 12.0 * a6 * Math.Sin(12 * B) - 1 / 14.0 * a7 * Math.Sin(14 * B));
                double N = a / (Math.Sqrt(1 - e * e * Math.Sin(B) * Math.Sin(B)));
                double l = L - L0;
                double t = Math.Tan(B);
                double eta = e2 * Math.Cos(B);
                x = X + N / 2.0 * Math.Sin(B) * Math.Cos(B) * l * l + N / 24.0 * Math.Sin(B) * Math.Pow(Math.Cos(B), 3) * (5 - t + 9 * eta * eta + 4 * Math.Pow(eta, 4)) * Math.Pow(l, 4) + N / 720.0 * Math.Sin(B) * Math.Pow(Math.Cos(B), 5) * (61 - 58 * t * t + Math.Pow(t, 4)) * Math.Pow(l, 6);
                y = N * Math.Cos(B) * l + N / 6.0 * Math.Pow(Math.Cos(B), 3) * (1 - t * t + eta * eta) * Math.Pow(l, 3) + N / 120.0 * Math.Pow(Math.Cos(B), 5) * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * eta * eta - 58 * t * t * eta * eta) * Math.Pow(l, 5);
            }
            public void GetGPS(double x, double y, double L0)//高斯反算
            {
                double e1 = e;
                double a0 = 1 + e1 * e1 * 3 / 4 + Math.Pow(e1, 4) * 45 / 64 + Math.Pow(e1, 6) * 175 / 256
                   + Math.Pow(e1, 8) * 11025 / 16384 + Math.Pow(e1, 10) * 43659 / 65336
                   + Math.Pow(e1, 12) * 693693 / 1048576 + Math.Pow(e1, 14) * 2760615 / 41944304
                   + Math.Pow(e1, 16) * 703956835 / 1073741824;
                double B0 = x / (a * (1 - e1 * e1) * a0);
                double C1 = Math.Pow(e1, 2) * 3 / 8 + Math.Pow(e1, 4) * 3 / 16
                    + Math.Pow(e1, 6) * 213 / 2048 + Math.Pow(e1, 8) * 255 / 4096;
                double C2 = Math.Pow(e1, 4) * 21 / 256 + Math.Pow(e1, 6) * 21 / 256
                    + Math.Pow(e1, 8) * 533 / 8192;
                double C3 = Math.Pow(e1, 6) * 151 / 6144 + Math.Pow(e1, 8) * 151 / 4096;
                double C4 = Math.Pow(e1, 8) * 1097 / 131072;
                double Bf = B0 + C1 * Math.Sin(2 * B0) + C2 * Math.Sin(4 * B0)
                    + C3 * Math.Sin(6 * B0) + C4 * Math.Sin(8 * B0);
                double tf = Math.Tan(Bf);
                double nf = e2 * Math.Cos(Bf);
                double Nf = a / Math.Sqrt(1 - Math.Pow(e1 * Math.Sin(Bf), 2));
                double Mf = Nf / (1 + Math.Pow(e2 * Math.Cos(Bf), 2));
                B = Bf - (tf * y * y) / (2 * Mf * Nf)
                    + tf * (5 + 3 * tf * tf + nf * nf - 9 * Math.Pow(nf * tf, 2)) * Math.Pow(y, 4) / (24 * Mf * Math.Pow(Nf, 3))
                    + tf * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(y, 6) / (720 * Mf * Math.Pow(Nf, 5));
                L = L0 + y / (Nf * Math.Cos(Bf)) - Math.Pow(y, 3) * (1 + 2 * tf * tf + nf * nf) / (6 * Math.Pow(Nf, 3) * Math.Cos(Bf))
                    + Math.Pow(y, 5) * (5 + 28 * Math.Pow(tf, 2) + 24 * Math.Pow(tf, 4) + 6 * Math.Pow(nf, 2) + 8 * Math.Pow(nf * tf, 2)) / (120 * Math.Pow(Nf, 5) * Math.Cos(Bf));

            }
        }
    }


