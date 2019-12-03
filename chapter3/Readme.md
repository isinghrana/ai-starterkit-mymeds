# Chapter 3 - Create Azure Function for Reading Text from Prescription Image

## Objective
Create Azure Function for reading text from prescription image (C# code is provided in the repo so no coding experience is required). The code in Azure Function uses Azure AI Platform capabilies available as part of Compute Vision Cognitive Service. At the end of chapter you will be able use the AI Capabilities to read text from and image.

## Azure Concepts/Terms 

* Azure Functions - Azure Functions is a solution for easily running small pieces of code, or "functions," in the cloud. You can write just the code you need for the problem at hand, without worrying about a whole application or the infrastructure to run it.

* Azure Cognitive Services -  are a set of services which bring AI within reach of every developer—without requiring machine-learning expertise. All it takes is an API call to embed the ability to see, hear, speak, search, understand, and accelerate decision-making into your apps.

* Azure Computer Vision Cognitive Service - is one of the Cognitive Services providing AI capability of reading  text from an image.

## Learn More
* Azure Functions - <https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview>
* Azure Cogntiive Services - <https://docs.microsoft.com/en-us/azure/cognitive-services/>
* Azure Computer Vision Cognitive Services - <https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/>

***

### Contents

* [Task 1: Create Read Text from Image Azure Function](###Task-1:-Create-Read-Text-from-Image-Azure-Function)
* [Task 2: Specify Configuration settings for Read Text from Image Azure Function](###Task-2:-Specify-Configuration-settings-for-Read-Text-from-Image-Azure-Function)
* [Task 3: Deploy Azure Function Code](###Task-3:-Deploy-Azure-Function-Code)
* [Task 4: Test Azure Function](###Task-4:-Test-Azure-Function)

***

### Task 1: Create Read Text from Image Azure Function

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

1. On the Azure Function Detail page, click *Platform features* tab and then click *Container settings* under *Code Deployment* section

<img src="./images/AzureFuncDeployment.GIF" alt="Azure Func Deployment" width="50%" height="50%"/>

2. Code will be deployed from Public Github Repo so select *External* under *Manual Deployment (puhs/sync) section and then click *Continue* button at the bottom.

<img src="./images/AzureFuncDeploymentSetupCodeSource.GIF" alt="Azure Func Deployment - Code Source" width="50%" height="50%"/>

3. Select *App Service build service* for Build Provider (this uses Kudu engine already included in the Azure Function infrastructure as the build service) and then click *Continue* button at the bottom.

<img src="./images/AzureFuncDeploymentBuildProvider.GIF" alt="Azure Func Deployment - Build Provider" width="50%" height="50%"/>

4. Specify the Code Repository configuration - specify *https://github.com/isinghrana/ai-starterkit-mymeds* for Repository, *master* for Branch, leave Repository Type as *Git* and specify No for Private Repository. 

<img src="./images/AzureFuncDeploymentCodeRepoConfig.GIF" alt="Azure Func Deployment - Build Provider" width="50%" height="50%"/>

5. Click *Continue* and then *Finish* button, this starts the deployment of the code to your Azure Function. Wait for the deployment to succeed and the proceed to the next task, screenshot below shows a Successful deployment

<img src="./images/AzureFuncDeploymentSuccess.GIF" alt="Azure Func Deployment - Success" width="50%" height="50%"/>

***

### Task 4: Test Azure Function

In this task you will uplod a test image to Storage Account and then invoke Azure Function to read text from the image. 

1. Download the test image file <a id='testimage1url' href="https://raw.githubusercontent.com/isinghrana/ai-starterkit-mymeds/master/test-images/lipitor.jpg" download target="_blank">Click here to download test image file</a>

2. Browse to the detail page for Storage Account created in Chapter 2, click Containers and the click *+ Container* 

3. Container can be considered a top-level folder in a Storage Account, give an appropriate name to the container. Example - *test-images*. 

<img src="./images/StorageAccountCreateContainer.GIF" alt="Storage Account - Create Container" width="50%" height="50%"/>

4. Click the newly created folder and click *Upload* button on the toolbar to upload the test image downloaded in Step 1 above.

<img src="./images/StorageAccountUploadTestImage.GIF" alt="Storage Account - Upload Test Image" width="50%" height="50%"/>

5. Close the Upload dialog on the right side, click the elipsis *...* next to the test image file to open menu, click *Properties* and the copy the URL for the file.

<img src="./images/StorageAccountTestImage.GIF" alt="Storage Account - Test Image" width="50%" height="50%"/>

<img src="./images/StorageAccountTestImageProperties.GIF" alt="Storage Account - Test Image Properties" width="50%" height="50%"/>

6. Browse to the Azure Function detail page, select the Function and click *Test* tab on the right side to open the Test Dialog.

<img src="./images/AzureFuncTest1.GIF" alt="Azure Func - Test Dialog" width="50%" height="50%"/>

7. Copy the code snippet below and paste into the *Request Body* text box, replace the placeholder text (Paste the Test Image....) with URL of the test image from Step 5 and click *Run* button. Verify that output contains test from the image. 

```
{
    "blobUrl": "<Paste Test Image URL from Step 5 here>"
}
```

<img src="./images/AzureFuncTestResult.GIF" alt="Azure Func - Test Result" width="50%" height="50%"/>

***

**Congratulations! At this point you have implemented as Azure Function which uses AI Capabilities of Azure Computer Vision Cognitive Service to read text from an image.**

[Back to Chapter 2](../chapter2/Readme.md)