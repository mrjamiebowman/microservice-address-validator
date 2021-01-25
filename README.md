# Microservice: Address Validator
.NET Microservice for Address Validation

## Address Validators
This microservice supports these address validators.

* Smarty Streets


## AppSettings.json
This file goes in the root of the AddressValidator.Api project. I'm not checking this in and ignoring it so my API keys don't accidentally get checked in.

##### src/AddressValidator.Api/appsettings.json
	{
	  "Logging": {
		"LogLevel": {
		  "Default": "Information",
		  "Microsoft": "Warning",
		  "Microsoft.Hosting.Lifetime": "Information"
		}
	  },
	  "AllowedHosts": "*",
	  "SmartyStreets": {
		"Key": "",
		"AuthToken": ""
	  }
	}

