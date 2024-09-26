# Options pattern example

This project aims to show the differences between:
- IOptions
- IOptionsSnapshot
- IOptionsMonitor

it exposes an endpoint: /options-pattern

and has appsettings.json with this configuration section:

```json
"AppOptions": {
    "Value": "Value 10"
  }
```

if the `"Value 10"` is changed when the application is running and the /options-pattern is called, in the response only the iOptionsSnapshot and iOptionsMonitorCurrentValue will be changed.

Response:
```json
{
  "iOptions": "Value 10",
  "iOptionsSnapshot": "Value 10",
  "iOptionsMonitorCurrentValue": "Value 10"
}
```

and if the `"Value"` has no value, like `"Value": ""`, the application will not start and throw an exception with the message:

`"AppOptions.Value must have a value other than null or empty or white space."`