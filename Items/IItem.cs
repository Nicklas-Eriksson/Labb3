﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Labb3.Items
{
    public interface IItem
    {
        string Name { get; set; }
        int GoldCost { get; set; }
        int ItemLevel { get; set; }
        int Bonus { get; set; }
       

        void Instantiate();

    }
}
