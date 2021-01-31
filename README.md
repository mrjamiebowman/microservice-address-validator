# Microservice: Address Validator
.NET Microservice for Address Validation

* .NET 5
* Swagger UI
* Kubernetes / Helm
* Health Checks

## Tenancy Configuration
This microservice is capable of servicing multiple Companies/Applications, that way specific API keys can be used for each Compan/Application. By default none of these values are required. It's up to you to properly configure this. If the configuraiton is there it works, if something is missing it will output an error.

### Single Tenant
This is by default and the configuration section for this is the "Default  Company". These API configuration settings can be used for API failover as well in a multi-tenancy configuration.

### Multi-Tenant
To set up a multi-tenant configuration you will need to create an array of companies and applications in the configuration file. The "1" is used in a Dictionary to look up the companies/applications and is also the CompanyID / Application ID used to make API requests to the microservice.

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


## Address Validators
This microservice supports these address validators. The design of this system makes it easy to add other address validator services. Pull requests are welcomed. To start with these are included by default.

* Smarty Streets
* USPS

## Environment Variables (Features Switches)
There are some features included in here for using different configuration providers, failover, and health checks.

### Fail to Default Company Settings
`FEATURE_FAIL_TO_DEFAULT = SmartyStreets` - In the event that a key is expired, default and re-try using the default company settings. The value that should be entered here is the **API Configuration Position (SmartyStreets, USPS).**

### Azure App Configuration
`FEATURE_CONFIG_PROVIDER = azure` - Defaults to using appsettings.json. Setting the value to `azure` will allow the configuration below to be pulled from **Azure App Configuration + Azure Key Vault.**

### Health Check API Expiration
`FEATURE_API_KEY_EXP_DAYS = 90` - This by default is `30` days in the system. Used by the Health Checks, the number of days (integer value) will determine when the API's health will become `degraded`. For example, if the value `90` is passed and the expiration date is set for the API Keys then it will become degraded if the API Key comes within 90 days of expiration. This makes it easy to check for expiring API keys.

## Environment Variables (Azure App Config)

`AZ_TENANT_ID` -  Azure Tenant ID  
`AZ_CLIENT_ID` -  Client ID set up in Azure Active Directory Applications  
`AZ_CLIENT_SECRET` - Client Secret set up in Azure Active Directory Applications   
`AZ_APPCONFIG_CONNECTION_STRING` - Connection string to Azure App Configuration  
`AZ_APPCONFIG_LABEL_FILTER` - This is the label for filtering app configuraiton on. For example: `ADDRVLTR`  

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
