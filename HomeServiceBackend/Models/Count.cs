using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceBackend.Models
{
    public class Count
    {
        public Count(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12)
        {
            January = v1;
            February = v2;
            March = v3;
            April = v4;
            May = v5;
            June = v6;
            July = v7;
            August = v8;
            September = v9;
            October = v10;
            November = v11;
            December = v12;
        }

        public void declare(int indexm, float hours)
        {
            switch (indexm)
            {
                case 1:
                    January += hours;
                    break;
                case 2:
                    February += hours;
                    break;
                case 3:
                    March += hours;
                    break;
                case 4:
                    April += hours;
                    break;
                case 5:
                    May += hours;
                    break;
                case 6:
                    June += hours;
                    break;
                case 7:
                    July += hours;
                    break;
                case 8:
                    August += hours;
                    break;
                case 9:
                    September += hours;
                    break;
                case 10:
                    October += hours;
                    break;
                case 11:
                    November += hours;
                    break;
                case 12:
                    December += hours;
                    break;
            }
        }

        public Works work { get; set; }
        public float January { get; set; }
        public float February { get; set; }
        public float March { get; set; }
        public float April { get; set; }
        public float May { get; set; }
        public float June { get; set; }
        public float July { get; set; }
        public float August { get; set; }
        public float September { get; set; }
        public float October { get; set; }
        public float November { get; set; }
        public float December { get; set; }
    }
}
