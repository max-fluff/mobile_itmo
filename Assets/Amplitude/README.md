# Amplitude Analytics API
This is a lightweight package for sending events to Amplitude Analytics via [HTTP REST API](https://developers.amplitude.com/docs/http-api-v2).

The official unity app for working with Amplitude Analytics: https://github.com/amplitude/unity-plugin 

## Installation
1. You must be logged in to our NPM Server
2. Find and install this package in the "Package Manager" window, do not forget to select the "My Registries" tab

## Example
```c#
Amplitude.Init(apiKey, userId, platform, version);

Amplitude.Logging = true;

Amplitude.DefaultUserProperties.Add(new AmplitudeProperty("user_name", "sergey"));
Amplitude.DefaultUserProperties.Add(new AmplitudeProperty("user_age", 21));

var amplitudeEvent = AmplitudeEvent.Builder
    .EventType("testType")
    .EventProperty("int", 1)
    .EventProperty("float", 1.5f)
    .EventProperty("string", "test")
    .UserProperty("int", 1)
    .UserProperty("float", 1.5f)
    .UserProperty("string", "test")
    .EventProperty("intA", new[] {1, 2, 3})
    .EventProperty("floatA", new[] {0.5f, 1.5f, 2f})
    .EventProperty("stringA", new[] {"test1", "test2"});

var logEvent = Amplitude.Send(amplitudeEvent);

StartCoroutine(logEvent);

await logEvent; // if you use UniTask
```
