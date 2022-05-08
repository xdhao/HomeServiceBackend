using FizzWare.NBuilder.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class Count
    {
        public float[] months { get; set; }

        public Count(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12)
        {
            months = new float[12];
            months[0] = v1;
            months[1] = v2;
            months[2] = v3;
            months[3] = v4;
            months[4] = v5;
            months[5] = v6;
            months[6] = v7;
            months[7] = v8;
            months[8] = v9;
            months[9] = v10;
            months[10] = v11;
            months[11] = v12;
        }

        public void declare(int indexm, float hours)
        {
            switch (indexm)
            {
                case 1:
                    months[0] += hours;
                    break;
                case 2:
                    months[1] += hours;
                    break;
                case 3:
                    months[2] += hours;
                    break;
                case 4:
                    months[3] += hours;
                    break;
                case 5:
                    months[4] += hours;
                    break;
                case 6:
                    months[5] += hours;
                    break;
                case 7:
                    months[6] += hours;
                    break;
                case 8:
                    months[7] += hours;
                    break;
                case 9:
                    months[8] += hours;
                    break;
                case 10:
                    months[9] += hours;
                    break;
                case 11:
                    months[10] += hours;
                    break;
                case 12:
                    months[11] += hours;
                    break;
            }
        }
        public dynamic work { get; set; }
    }

    public class TravelTime
    {
        public TimeSpan[] months { get; set; }

        public TravelTime(TimeSpan v1, TimeSpan v2, TimeSpan v3, TimeSpan v4, TimeSpan v5
            , TimeSpan v6, TimeSpan v7, TimeSpan v8, TimeSpan v9, TimeSpan v10, TimeSpan v11, TimeSpan v12)
        {
            months = new TimeSpan[12];
            months[0] = v1;
            months[1] = v2;
            months[2] = v3;
            months[3] = v4;
            months[4] = v5;
            months[5] = v6;
            months[6] = v7;
            months[7] = v8;
            months[8] = v9;
            months[9] = v10;
            months[10] = v11;
            months[11] = v12;
        }

        public void declare(int indexm, TimeSpan hours)
        {
            switch (indexm)
            {
                case 1:
                    months[0] += hours;
                    break;
                case 2:
                    months[1] += hours;
                    break;
                case 3:
                    months[2] += hours;
                    break;
                case 4:
                    months[3] += hours;
                    break;
                case 5:
                    months[4] += hours;
                    break;
                case 6:
                    months[5] += hours;
                    break;
                case 7:
                    months[6] += hours;
                    break;
                case 8:
                    months[7] += hours;
                    break;
                case 9:
                    months[8] += hours;
                    break;
                case 10:
                    months[9] += hours;
                    break;
                case 11:
                    months[10] += hours;
                    break;
                case 12:
                    months[11] += hours;
                    break;
            }
        }
        public dynamic employee { get; set; }
    }
}
