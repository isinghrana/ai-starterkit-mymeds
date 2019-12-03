# Chapter 2 - Azure Functions for reading medicine info from image

## Objective
Create Azure Functions for reading medicine info from image (C# code is provided in the repo so no coding experience is required) 


## Azure Concepts/Terms 

* Azure Functions - Azure Functions is a solution for easily running small pieces of code, or "functions," in the cloud. You can write just the code you need for the problem at hand, without worrying about a whole application or the infrastructure to run it.

## Learn More
* Azure Functions - <https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview>

***

### Task 1: Create Read Text from Image Azure Function (empty)

1. Expand the menu on the Azure Portal by clicking menu icon on top left corner and then click *+ Create a resource*

2. At this point Azure Portal should show New resouce creation screen
	
	a. Start typing *Function*, as you are typing *Function App* selection will be displayed below the textbox

	b. Select *Function App* and Function App creation screen is displayed

	c. Click Create and Function App Details screen is displayed

	d. Enter the details - select the Resource Group created in Chapter 2, specify function app name which needs to be globally unique (add numbers like 123, 456, etc. whatever to make it unique), select the Region sane as the resource group, select *.Net Core* for Runtime stack, leave remaining selection as default and click *Review + create* button on bottom left

<img src="./images/CreateAzureFunc1.GIF" alt="Create Azure Function Details" width="50%" height="50%"/>

3. Review the Azure Function details - Runtime Stack must be .NET Core, Plan Type must be Consumption, Operating System and Application Insights must be Enabled (if any of the settings are different you can go back to the Preview screen and make appropriate selections).

<img src="./images/CreateAzureFuncReviewDetails.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>


4. Click *Create* button

5. Wait for the resource creation to complete and then proceed to the next Task

***

### Task 2: Deploy code to Read Text from Image Azure Function

1. Browse to the Azure Function detail page on the Azure Portal by clicking *Go to resource* button from the deployment screen or from the Resource Group detail screen (see Chapter 2 for steps to browse to the Resource Group Detail screen)

<img src="./images/AzureFuncCreateSuccess.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

2. Click *Configuration* link under the *Configuration features* section

<img src="./images/AzureFuncConfig.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

3. 

[Back to Chapter 2](../chapter2/Readme.md)