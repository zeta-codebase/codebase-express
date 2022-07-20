﻿using Zeta.CodebaseExpress.Shared.Common.Constants;

namespace Zeta.CodebaseExpress.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with {CommonDisplayTextFor.Id} [{key}] could not be found.")
    {
    }

    public NotFoundException(string entityName, string entityField, object value)
        : base($"{entityName} with {entityField} is {value} could not be found.")
    {
    }
}
