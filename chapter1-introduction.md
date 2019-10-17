# Chapter 1 - Introduction

The purpose of this course is to educate as well as showcase how Microsoft Azure AI Platform can be used to develop solutions for health care specific scenarios geared towards preventive measures for patient health. The course will be self-paced and will contain hands-on exercises where you will build a solution by following step by step instructions to create Azure resources. The learning experience will be kept to simple clicks within a browser window so coding experience is not required, code snippets will be provided where code is needed but developers are more than welcome to explore, enhance or do things differently.

## Use Case - My Medication List
The use case we will be using for this course is to maintain a list of current medications for a patient using medicine bottle or prescription images. It is very important for patients to take their medications regularly and have the medication list available in case they are treated by a new doctor in an emergency. Such a mechanism to use image to maintain medication list can be very beneficial especially for older patients who don't use computers but are comfortable with the smart phones.

As we progress through the chapters, we will be expanding the concept where a Phone App is used to upload the image to the storage account by the patient, a background process is triggered which processes image using Azure Platform AI Capabilities to read the text from the image and identify the medicine name. This process will also create a record with patient id,  image text and medicine name in the database which will serve as customer's medication list. 
 

In certain cases patient not taking the medication on time can lead to much greater complications risking patient health and this simple medication list idea can further be extended in many ways:
1. Patients can set schedule for the medicines with reminders
2. Health Care Provider can further enhance the solution to identify and warn about risks of adding new medicine with an existing medicine patient is already taking.
3. An IoT capable Medicine bottle can send messages to the system indicating if the patient took medication allowing health care provider to monitor patient medicine regimen and generate alerts if needed.
4. Medicine information can be retrieved from online Api like RxNorm to identify ingredients and do a cross-check on allergies (RXNorm API - <https://rxnav.nlm.nih.gov/APIsOverview.html>)
5. Online Pricing Api (like GoodRx Api - <https://www.goodrx.com/developer>) can be used to help find cheaper price for the medication hence reducing cost


## Audience
The courses can be used by health care professionals or technology professionals to gain better understanding on how AI can be used to improve patient health at minimal costs. Such solutions can be implemented by Health Care Providers themselves, ISVs who make software solutions for the Health Care industry or even patients interested in technology.


## Technology Platform
The main pre-requisite is an Azure Subscription and if you don't have an Azure Subscription you can create one for free - <https://azure.microsoft.com/en-us/free/>

A variety of  Azure Cloud services will be used as the technology stack, the costs of these services are very minimal and should be covered well under the Trial Subscription Credits. Even if not using the Trial Subscription, usage based pricing of Cloud Platform does not require any upfront commitment and it's not expected to cost more than a few dollars.

[Go to Chapter 2](./chapter2/chapter2.md)