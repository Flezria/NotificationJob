﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationJob
{
    class Program
    {
        static void Main(string[] args)
        {
            DoNotifications DoWork = new DoNotifications();

            DoWork.GetRelevantCases();
            DoWork.SendNotifications();
        }

    }
}
