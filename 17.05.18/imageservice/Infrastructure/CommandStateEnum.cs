﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Enum of Command State (to int)
    /// </summary>
    public enum CommandStateEnum : int
    {
        NEW_FILE,
        CLOSE,
        GET_APP_CONFIG,
        GET_ALL_LOG
    }
}