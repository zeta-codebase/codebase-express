﻿using RestSharp;
using System.Collections;

namespace Zeta.CodebaseExpress.Client.Common.Extensions;

public static class RestRequestExtensions
{
    private const string UniversalDateTimeFormat = "yyyy/MM/dd HH:mm:ss";

    public static void AddParameters<T>(this RestRequest restRequest, T request) where T : notnull
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var propertyValue = property.GetValue(request);

            if (propertyValue is null)
            {
                continue;
            }

            restRequest.AddParameter(property.Name, propertyValue.ToString());
        }
    }

    public static void AddQueryParameters<T>(this RestRequest restRequest, T request)
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var propertyValue = property.GetValue(request);

            if (propertyValue is null)
            {
                continue;
            }

            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                var values = (IEnumerable)propertyValue;

                foreach (var value in values)
                {
                    restRequest.AddQueryParameter(property.Name, value.ToString()!);
                }
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                var dateTime = (DateTime)propertyValue;

                restRequest.AddQueryParameter(property.Name, dateTime.ToString(UniversalDateTimeFormat));
            }
            else if (property.PropertyType == typeof(DateTime?))
            {
                var nullableDateTime = (DateTime?)propertyValue;

                restRequest.AddQueryParameter(property.Name, nullableDateTime.Value.ToString(UniversalDateTimeFormat));
            }
            else
            {
                restRequest.AddQueryParameter(property.Name, propertyValue.ToString()!);
            }
        }
    }
}
