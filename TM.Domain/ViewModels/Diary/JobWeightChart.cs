﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModels
{
    public class JobWeightChart
    {
        public List<string> Legend { get; set; }

        public List<Series> Series { get; set; }
    }

    public class Series
    {
        public decimal? value { get; set; }

        public string name { get; set; }
    }
}
