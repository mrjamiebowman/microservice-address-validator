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
		"DefaultCompany": {
			"Name": "Default Company",
			"SmartyStreets": {
				"Key": "",
				"AuthToken": "",
				"Expiration": "01/01/2021"
			},
			"USPS": {
				"Username": "",
				"Password": "",
				"Expiration": ""
			}
		},
		"Companies": {
			"Configuration": {
			"1" : {
				"Name": "Company1",
				"Applications": {
					"1": {
						"Name": "App 1",
						"SmartyStreets": {
							"Key": "",
							"AuthToken": "",
							"Expiration": ""
							},
						"USPS": {
							"Username": "",
							"Password": "",
							"Expiration": ""
							}
						} 
					}	
				}
			}
		}
	}
