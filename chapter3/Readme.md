# Chapter 3 - Create an Azure Function for Reading Text from a Prescription Image

## Objective
The business objective here is to read text from a prescription image. We'll use an Azure Function to do this. (It's a simple function, the C# code is provided for you so no coding experience is required). The code in Azure Function uses Azure AI Platform capabilies available as part of our Compute Vision Cognitive Service. At the end of chapter, you will be able use the AI Capabilities to read text from and image.

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
* [Task 3: Deploy Azure Function Code](#task-3-deploy-azure-function-code)
* [Task 4: Test Azure Function](#task-4-test-azure-function)

***

### Task 1: Create Read Text from Image Azure Function

In this task you will create an empty Azure Function Resource.

1. Expand the menu on the Azure Portal by clicking the menu icon in the top left corner and then click *+ Create a resource*

2. The Azure Portal should show the New resource creation screen
	
	a. Start typing *Function*. As you are typing, *Function App* selection will be displayed below the textbox.

	b. Select *Function App* and Function App creation screen is displayed.

	c. Click *Create* and Function App Details screen is displayed.

	d. Enter the details - select the Resource Group created in Chapter 2. Enter a name for your function - which needs to be globally unique. (Add numbers like 123, your dog's name, or whatever to make it unique.) Select a Region. (It's not necessary, but you should probably make it the same as your Resource Group's region.) Select *.Net Core* for Runtime stack. Leave the rest of the options with their default settings and click the *Review + create* button on the bottom left.

<img src="./images/CreateAzureFuncDetails.GIF" alt="Create Azure Function Details" width="50%" height="50%"/>

3. Review the Azure Function details: Runtime Stack must be .NET Core, Plan Type must be Consumption, Operating System and Application Insights must be Enabled (if any of the settings are different you can go back to the Preview screen and make appropriate selections).

<img src="./images/CreateAzureFuncReviewDetails.GIF" alt="Create Azure Function - Review Details" width="50%" height="50%"/>

4. Click the *Create* button.

5. Wait for the resource creation to complete and then proceed to the next Task.

***

### Task 2: Specify Configuration settings for Read Text from Image Azure Function

In this task you will specify a few configuration settings to the Azure Function resource created in the previous task.

1. Browse to the Azure Function detail page on the Azure Portal by clicking *Go to resource* button from the deployment screen or from the Resource Group detail screen. (See Chapter 2 for steps to browse to the Resource Group Detail screen.)

<img src="./images/AzureFuncCreateSuccess.GIF" alt="Azure Function Details" width="50%" height="50%"/>

2. Click *Configuration* link under the *Configuration features* section

<img src="./images/AzureFuncConfig.GIF" alt="Azure Function - Configuration" width="80%" height="80%"/>

3. A total of 4 Application Settings need to be added for Read Text from Image Azure Function. Click *+ New application setting* button to add each of the following settings one by one.
    
<img src="./images/AzureFuncAddAppSetting.GIF" alt="Azure Function - Add App Setting" width="80%" height="80%"/>

When adding application settings it's critical you use the exact name of the setting as specified below so copy/paste to avoid typing mistakes.

4. Add Storage Connection String Setting - Specify name of the setting **StorageConnectionString** and set the value to connection string of the Storage Account found on the **Access Keys** page of the Storage Account (see the second screenshot on how to lookup the Storage Account Connection String).

<img src="./images/AzureFuncAddAppSetting2.GIF" alt="Azure Function - Add App Setting" width="80%" height="80%"/>

<img src="./images/StorageAccountConnectionString.GIF" alt="Storage Account Connection String" width="80%" height="80%"/>

5. Add Cognitive Service Key Settting - Specify name of the setting **CognitiveServiceKey** and set the value to the key of the Cognitive Service found from **Keys** page of the Cognitive Service.

<img src="./images/CognitiveServiceKey.GIF" alt="Cognitive Service Key" width="80%" height="80%"/>

6. Add Cognitive Service Endpoint Setting - Specify name of the setting **CognitiveServiceEndpoint** and set the value to the Endpoint for the Cognitive Service.

<img src="./images/CognitiveServiceEndpoint.GIF" alt="Cognitive Service Endpoint" width="80%" height="80%"/>

7. Add Project setting - Specify name of the setting **PROJECT** and set the value as **code/MyMedAIStarterKit_Soln/ReadImageTextFuncApp**. This setting is used for deploying the code to Azure Function

*Note: Don't forget to click the *Save* button after adding all the settings.*

<img src="./images/AddAppSettingsSave.GIF" alt="Azure Func - Add App Settings Save" width="80%" height="80%"/>

***

### Task 3: Deploy Azure Function Code

In this task you will add the code to the Azure Function resoruce so that the function can be used to read text from image.

1. On the Azure Function Detail page, click *Platform features* tab and then click *Container settings* under *Code Deployment* section

<img src="./images/AzureFuncDeployment.GIF" alt="Azure Func Deployment" width="80%" height="80%"/>

2. Code will be deployed from Public Github Repo so select *External* under *Manual Deployment (push/sync) section and then click *Continue* button at the bottom.

<img src="./images/AzureFuncDeploymentSetupCodeSource.GIF" alt="Azure Func Deployment - Code Source" width="80%" height="80%"/>

3. Select *App Service build service* for Build Provider and then click *Continue* button at the bottom.

<img src="./images/AzureFuncDeploymentBuildProvider.GIF" alt="Azure Func Deployment - Build Provider" width="80%" height="80%"/>

4. Specify the Code Repository configuration - specify *https://github.com/isinghrana/ai-starterkit-mymeds* for Repository, *master* for Branch, leave Repository Type as *Git* and specify No for Private Repository. 

<img src="./images/AzureFuncDeploymentCodeRepoConfig.GIF" alt="Azure Func Deployment - Build Provider" width="80%" height="80%"/>

5. Click *Continue* and then *Finish* button, this starts the deployment of the code to your Azure Function. Wait for the deployment to succeed and the proceed to the next task, screenshot below shows a Successful deployment

<img src="./images/AzureFuncDeploymentSuccess.GIF" alt="Azure Func Deployment - Success" width="80%" height="80%"/>

***

### Task 4: Test Azure Function

In this task, you will upload a test image to Storage Account and then invoke Azure Function to read text from the image. 

1. Download the test image file to your local computer. Right-click the image below and select *Save Image as...* option.

<img src="../test-images/lipitor.jpg" alt="Test Prescription Image" width="50%" height="50%"/>

2. Browse to the detail page for Storage Account created in Chapter 2, click Containers and the click *+ Container* 

3. In this context, a Container can be considered a top-level folder in a Storage Account, give an appropriate name to the container. Example - *test-images*.

<img src="./images/StorageAccountCreateContainer.GIF" alt="Storage Account - Create Container" width="80%" height="80%"/>

4. Click the newly created folder and click *Upload* button on the toolbar to upload the test image downloaded in Step 1 above.

<img src="./images/StorageAccountUploadTestImage.GIF" alt="Storage Account - Upload Test Image" width="80%" height="80%"/>

5. Close the Upload dialog on the right side, click the elipsis *...* next to the test image file to open menu, and click *Properties* and the copy the URL for the file.

<img src="./images/StorageAccountTestImage.GIF" alt="Storage Account - Test Image" width="80%" height="80%"/>

<img src="./images/StorageAccountTestImageProperties.GIF" alt="Storage Account - Test Image Properties" width="80%" height="80%"/>

6. Browse to the Azure Function detail page, select the Function and click *Test* tab on the right side to open the Test Dialog.

<img src="./images/AzureFuncTest1.GIF" alt="Azure Func - Test Dialog" width="80%" height="80%"/>

7. Copy the code snippet below and paste into the *Request Body* text box, replace the placeholder text (Paste the Test Image....) with URL of the test image from Step 5 and click *Run* button. Verify that output contains test from the image. 

```
{
    "blobUrl": "<Paste Test Image URL from Step 5 here>"
}
```

<img src="./images/AzureFuncTestResult.GIF" alt="Azure Func - Test Result" width="80%" height="80%"/>

***

**Congratulations! At this point, you have implemented an Azure Function which uses AI Capabilities of Azure Computer Vision Cognitive Service to read text from an image.**

***

[Previous Chapter](../chapter2/Readme.md) | [Next Chapter](../chapter4/Readme.md)
