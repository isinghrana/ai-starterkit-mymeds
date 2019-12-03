# Chapter 3 - Create Azure Function for Reading Text from Prescription Image

## Objective
Create Azure Functions for reading text from prescriptino image (C# code is provided in the repo so no coding experience is required). 

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

<img src="./images/CreateAzureFuncDetails.GIF" alt="Create Azure Function Details" width="50%" height="50%"/>

3. Review the Azure Function details - Runtime Stack must be .NET Core, Plan Type must be Consumption, Operating System and Application Insights must be Enabled (if any of the settings are different you can go back to the Preview screen and make appropriate selections).

<img src="./images/CreateAzureFuncReviewDetails.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>


4. Click *Create* button

5. Wait for the resource creation to complete and then proceed to the next Task

***

### Task 2: Specify Configuration settings for Read Text from Image Azure Function

1. Browse to the Azure Function detail page on the Azure Portal by clicking *Go to resource* button from the deployment screen or from the Resource Group detail screen (see Chapter 2 for steps to browse to the Resource Group Detail screen)

<img src="./images/AzureFuncCreateSuccess.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

2. Click *Configuration* link under the *Configuration features* section

<img src="./images/AzureFuncConfig.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

3. A total of 4 Application Settings need to be added for Read Text from Image Azure Function. Click *+ New application setting* button to add each of the following settings one by one.
    
<img src="./images/AzureFuncAddAppSetting.GIF" alt="Azure Function - Add App Setting" width="50%" height="50%"/>

<img src="./images/AzureFuncAddAppSetting2.GIF" alt="Azure Function - Add App Setting" width="50%" height="50%"/>

Its important to use the exact name of the setting as specified below so copy/paste to avoid typing mistakes.

    a. Specify name of the setting *StorageConnectionString* and set the value to connection string of the Storage Account found on the *Access Keys* page of the Storage Account.

<img src="./images/StorageAccountConnectionString.GIF" alt="Storage Account Connection String" width="50%" height="50%"/>
    
    b. Specify name of the setting *CognitiveServiceKey* and set the value to the key of the Cognitive Service found from *Keys* page of the Cognitive Service.

<img src="./images/CognitiveServiceKey.GIF" alt="Cognitive Service Key" width="50%" height="50%"/>

    c. Specify name of the setting *CognitiveServiceEndpoint* and set the value to the Endpoint for the Cognitive Service.

<img src="./images/CognitiveServiceEndpoint.GIF" alt="Cognitive Service Endpoint" width="50%" height="50%"/>

    d. Specify name the setting *PROJECT* and set the value as *code/MyMedAIStarterKit_Soln/ReadImageTextFuncApp*. This setting is used for deploying the code to Azure Function

Note - Don't forget to click the *Save* button after adding all the settings.

<img src="./images/AddAppSettingsSave.GIF" alt="Azure Func - Add App Settings Save" width="50%" height="50%"/>

***

### Task 3: Deploy Azure Function Code




[Back to Chapter 2](../chapter2/Readme.md)