using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum CoreReturns
    {
        SUCCESS = 0,
        PATH_IS_INVALID = 1,
        FILE_EXISTS = 2,
        ON_PROCESS = 3,
        NOT_INSERTED = 4,
        NOT_UPDATEED = 5,
        NOT_DELETED = 6,
        NOT_FOUND = 7,
        COMMAND_INVALID = 8,
        DATABASE_DISCONNECTED = 9,
        MAIL_NOT_SENT = 10,
        COULD_NOT_UPDATE_THE_VALUE = 11,
        IS_NULL = 12,
        WAITING_TO_INITIALIZE = 13,
        NOT_EQUAL = 14,
        PARAMETERS_EQAUL = 15,
        OUT_RETURN = 16,
        PASSWORD_INVALID = 17,
        IS_NULL_OR_EMPTY = 18,
        ERROR = 19,
        USERNAME_IS_NULL = 20,
        LASTNAME_IS_NULL = 21,
        USERNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE = 22,
        EMAIL_IS_NULL_OR_EMPTY_OR_WHITESPACE = 23,
        PASSWORD_IS_NULL_OR_EMPTY_OR_WHITESPACE = 24,
        FIRSTNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE = 25,
        LASTNAME_IS_NULL_OR_EMPTY_OR_WHITESPACE = 26,
    }
}
