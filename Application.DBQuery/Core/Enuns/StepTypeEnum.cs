﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Enuns
{
    public enum StepType
    {
        USE_ALIAS, 
        INSERT,
        INSERT_NOT_EXISTS,
        DELETE,
        DELETE_AND_INSERT,
        SELECT,
        CUSTOM_SELECT,
        UPDATE,
        INSERT_OR_UPDATE,
        WHERE,
        EXECUTE,
        DISTINCT,
        TOP,
        JOIN,
        LEFT_JOIN,
        ORDER_BY_ASC,
        ORDER_BY_DESC,
        GROUP_BY,
        PAGINATION,
    }
}
