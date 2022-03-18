﻿namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public abstract class BaseApi
{
    protected abstract string ControllerName { get; }

    protected readonly HttpClient _httpClient;

    protected BaseApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}