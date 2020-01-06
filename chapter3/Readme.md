# Chapter 3 - Create an Azure Function for Reading Text from a Prescription Image

## Objective
The business objective is to read text from a prescription image. We'll use an Azure Function to do this (it's a simple function, the C# code is provided for you so no coding experience is required). The code in Azure Function uses Azure AI Platform capabilies available as part of our Computer Vision Cognitive Service. At the end of chapter, you will be able use the AI Capabilities to read text from and image. In this chapter we will only create and enter some configuration for the Azure Function, Azure Function code deployment and testing will be done in the next chapter.

## Azure Concepts/Terms
* Azure Functions - Azure Functions is a solution for easily running small pieces of code, or "functions," in the cloud. It's server-less so you can write just the code you need for the problem at hand without worrying about a whole application or the infrastructure to run it.

* Azure Cognitive Services -  we've assembled a set of services which bring AI within reach of every developer—without requiring machine-learning expertise. All it takes is an API call to embed the ability to see, hear, speak, search, understand, and accelerate decision-making into your apps.

* Azure Computer Vision Cognitive Service - Computer Vision is one of the Cognitive Services. It uses machine learning to read text from an image.

## Learn More
* Azure Functions - <https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview>
* Azure Cognitive Services - <https://docs.microsoft.com/en-us/azure/cognitive-services/>
* Azure Computer Vision Cognitive Services - <https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/>

***

### Contents

* [Task 1: Create Read Text from Image Azure Function](#task-1-create-read-text-from-image-azure-function)
* [Task 2: Specify Configuration settings for Read Text from Image Azure Function](#task-2-specify-configuration-settings-for-read-text-from-image-azure-function)

***

### Task 1: Create Read Text from Image Azure Function

In this task you will create an empty Azure Function Resource.

1. Expand the menu on the Azure Portal by clicking the menu icon in the top left corner and then click **_+ Create a resource_**

2. The Azure Portal should show the New resource creation screen
	
	a. Start typing **_Function_**. As you are typing, **_Function App_** selection will be displayed below the textbox.

	b. Select **_Function App_** and Function App creation screen is displayed.

	c. Click **_Create_** and Function App Details screen is displayed.

	d. Enter the details - select the Resource Group created in Chapter 2. Enter a name for your function - which needs to be globally unique. (Add numbers like 123, your dog's name, or whatever to make it unique.) Select a Region. (It's not necessary, but you should probably make it the same as your Resource Group's region.) Select **_.Net Core_** for Runtime stack. Leave the rest of the options with their default settings and click the **_Review + create_** button on the bottom left.

<img src="./images/CreateAzureFuncDetails.GIF" alt="Create Azure Function Details" width="50%" height="50%"/>

3. Review the Azure Function details: Runtime Stack must be .NET Core, Plan Type must be Consumption, Operating System and Application Insights must be Enabled (if any of the settings are different you can go back to the Preview screen and make appropriate selections).

<img src="./images/CreateAzureFuncReviewDetails.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

4. Click the **_Create_** button.

5. Wait for the resource creation to complete and then proceed to the next Task.

***

### Task 2: Specify Configuration settings for Read Text from Image Azure Function

In this task you will specify a few configuration settings to the Azure Function resource created in the previous task.

Although the code for Azure Function is provided its important to explain the purpose of the code as well as how and why the below configuration settings are necessary. The Azure Function code has been implemented in C# using Visual Studio, the Visual Studio solution is available in [code folder](../code) (implementing code in C# or even using Visual Studio is not a hard requirement, Azure Functions can be implemented in a variety of languages languages including Java, Python, etc. and editor can be used). This Azure Function has been implemented to run automatically when image file is uploaded to Storage Account, the code receives location of the newly uploaded image file as argument, reads the image file and then calls the Computer Vision Cognitive Service to read text from that image.


1. Browse to the Azure Function detail page on the Azure Portal by clicking **_Go to resource_** button from the deployment screen or from the Resource Group detail screen. (See Chapter 2 for steps to browse to the Resource Group Detail screen.)

<img src="./images/AzureFuncCreateSuccess.GIF" alt="Azure Function Details" width="50%" height="50%"/>

2. Click **_Configuration_** link under the **_Configuration features_** section

<img src="./images/AzureFuncConfig.GIF" alt="Azure Function - Configuration" width="80%" height="80%"/>

3. A total of 4 Application Settings need to be added for Read Text from Image Azure Function. Click **_+ New application setting_** button to add each of the following settings one by one.
    
<img src="./images/AzureFuncAddAppSetting.GIF" alt="Azure Function - Add App Setting" width="80%" height="80%"/>

When adding application settings it's critical you use the exact name of the setting as specified below so copy/paste to avoid typing mistakes.

4. Add Storage Connection String Setting - This setting is required so that Azure Function can connect to the Storage Account where image file will be uploaded. Specify name of the setting **_StorageConnectionString_** and set the value to connection string of the Storage Account found on the **_Access Keys_** page of the Storage Account (see the second screenshot on how to lookup the Storage Account Connection String).

> Note: If you browse away from Azure Function Application Settings screen without hitting *Save* button your changes will be lost. You will be copying the value from Storage Account and Cognitive Services Account detail screens and pasting them into Azure Function Application Setting screen so it might be easier to open two tabs, one to keep the Azure Function Application Setting page open and the other where you can browse to respective service web pages to copy the setting value. 

<img src="./images/AzureFuncAddAppSetting2.GIF" alt="Azure Function - Add App Setting" width="80%" height="80%"/>

<img src="./images/StorageAccountConnectionString.GIF" alt="Storage Account Connection String" width="80%" height="80%"/>

5. Add Cognitive Service Key Settting - This setting is required so that Azure Function can authenticate against Computer Vision Cognitive Service. Specify name of the setting **_CognitiveServiceKey_** and set the value to the key of the Cognitive Service found from **_Keys_** page of the Cognitive Service.

<img src="./images/CognitiveServiceKey.GIF" alt="Cognitive Service Key" width="80%" height="80%"/>

6. Add Cognitive Service Endpoint Setting - This setting is required so that Azure Function can communicate with Computer Vision Cognitive Service. Specify name of the setting **_CognitiveServiceEndpoint_** and set the value to the Endpoint for the Cognitive Service.

<img src="./images/CognitiveServiceEndpoint.GIF" alt="Cognitive Service Endpoint" width="80%" height="80%"/>

7. Add Project setting - This setting is required for deploying code to Azure Function which will be done in the next chapter. Specify name of the setting **_PROJECT_** and set the value as **_code/MyMedAIStarterKit_Soln/ReadImageTextFuncApp_**. This setting is used for deploying the code to Azure Function

8. Don't forget to click the **_Save_** button after adding all the settings.

<img src="./images/AddAppSettingsSave.GIF" alt="Azure Func - Add App Settings Save" width="80%" height="80%"/>

***

[Previous Chapter](../chapter2/Readme.md) | [Next Chapter](../chapter4/Readme.md)