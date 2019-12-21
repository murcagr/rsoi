﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Exceptions
{
    public class InternalException : Exception
    {

        public InternalException()
        {

        }

        public InternalException(string errorMessage) : base(errorMessage)
        {

        }

        public InternalException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {

        }
    }
    public class RequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        public RequestException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class with the name of the
        /// parameter that causes this exception.
        /// </summary>
        /// <param name="errorMessage">The error message that explains the reason for this exception.</param>
        public RequestException(string errorMessage) : base(errorMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class with a specified
        /// error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="DatabaseAddException">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic)
        /// if no inner exception is specified.</param>
        public RequestException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {

        }
    }
}
