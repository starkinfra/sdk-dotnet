# Stark Infra .NET SDK

Welcome to the Stark Infra .NET SDK! This tool is made for .NET 
developers who want to easily integrate with our API.
This SDK version is compatible with the Stark Infra API v2.

# Introduction

## Index

- [Introduction](#introduction)
    - [Supported .NET versions](#supported-net-versions)
    - [API documentation](#stark-infra-api-documentation)
    - [Versioning](#versioning)
- [Setup](#setup)
    - [Install our SDK](#1-install-our-sdk)
    - [Create your Private and Public Keys](#2-create-your-private-and-public-keys)
    - [Register your user credentials](#3-register-your-user-credentials)
    - [Setting up the user](#4-setting-up-the-user)
    - [Setting up the error language](#5-setting-up-the-error-language)
- [Resource listing and manual pagination](#resource-listing-and-manual-pagination)
- [Testing in Sandbox](#testing-in-sandbox) 
- [Usage](#usage)
    - [Issuing](#issuing)
        - [Products](#query-issuingproducts): View available sub-issuer card products (a.k.a. card number ranges or BINs)
        - [Holders](#create-issuingholders): Manage card holders
        - [Cards](#create-issuingcards): Create virtual and/or physical cards
        - [Design](#query-issuingdesigns): View your current card or package designs
        - [EmbossingKit](#query-issuingembossingkits): View your current embossing kits
        - [Stock](#query-issuingstocks): View your current stock of a certain IssuingDesign linked to an Embosser on the workspace
        - [Restock](#create-issuingrestocks): Create restock orders of a specific IssuingStock object
        - [StockRule](#create-issuingstockrules): Create notification rules for a specific IssuingStock object
        - [EmbossingRequest](#create-issuingembossingrequests): Create embossing requests
        - [Purchases](#process-purchase-authorizations): Authorize and view your past purchases
        - [Invoices](#create-issuinginvoices): Add money to your issuing balance
        - [Withdrawals](#create-issuingwithdrawals): Send money back to your Workspace from your issuing balance
        - [Balance](#get-your-issuingbalance): View your issuing balance
        - [Transactions](#query-issuingtransactions): View the transactions that have affected your issuing balance
        - [Enums](#issuing-enums): Query enums related to the issuing purchases, such as merchant categories, countries and card purchase methods
    - [Pix](#pix)
        - [PixRequests](#create-pixrequests): Create Pix transactions
        - [PixReversals](#create-pixreversals): Reverse Pix transactions
        - [PixBalance](#get-your-pixbalance): View your account balance
        - [PixStatement](#create-a-pixstatement): Request your account statement
        - [PixKey](#create-a-pixkey): Create a PixKey
        - [PixClaim](#create-a-pixclaim): Claim a PixKey
        - [PixDirector](#create-a-pixdirector): Create a Pix Director
        - [PixInfraction](#create-pixinfractions): Create Pix Infraction reports
        - [PixFraud](#create-pixfrauds): Create Pix Fraud reports
        - [PixKeyHolmes](#create-pixkeyholmes): Investigate Pix Key registration status
        - [PixInternalTransactionReport](#create-pixinternaltransactionreports): Report transactions that happen outside the SPI
        - [PixChargeback](#create-pixchargebacks): Create Pix Chargeback requests
        - [PixDomain](#query-pixdomains): View registered SPI participants certificates
        - [StaticBrcode](#create-staticbrcodes): Create static Pix BR codes
        - [DynamicBrcode](#create-dynamicbrcodes): Create dynamic Pix BR codes
        - [BrcodePreview](#create-brcodepreviews): Read data from BR Codes before paying them
        - [BrcodePreview](#create-brcodepreviews): Read data from BR Codes before paying them
        - [PixDispute](#create-pixdisputes): Create PixDisputes
        - [PixPullSubscription](#create-pixpullsubscriptions): Set up recurring Pix debit authorizations
        - [PixPullRequest](#create-pixpullrequests): Trigger automatic Pix debits against a subscription
    - [Lending](#lending)
        - [CreditNote](#create-creditnotes): Create credit notes
        - [CreditPreview](#create-creditpreviews): Create credit previews
        - [CreditHolmes](#create-creditholmes): Create credit holmes debt verification
    - [Identity](#identity)
        - [IndividualIdentity](#create-individualidentities): Create individual identities
        - [IndividualDocument](#create-individualdocuments): Create individual documents
        - [BusinessIdentity](#create-businessidentities): Create business identities
        - [BusinessAttachment](#create-businessattachments): Create business attachments
    - [Webhook](#webhook):
        - [Webhook](#create-a-webhook-subscription): Configure your webhook endpoints and subscriptions
        - [WebhookEvents](#process-webhook-events): Manage webhook events
        - [WebhookEventAttempts](#query-failed-webhook-event-delivery-attempts-information): Query failed webhook event deliveries
- [Handling errors](#handling-errors)
- [Help and Feedback](#help-and-feedback)

## Supported .NET Versions

This library supports the following .NET versions:

* .NET Standard 2.0+

## Stark Infra API documentation

Feel free to take a look at our [API docs](https://www.starkinfra.com/docs/api).

## Versioning

This project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.

# Setup

## 1. Install our SDK

StarkInfra`s .NET SDK is available on NuGet as starkinfra 0.13.0

1.1 To install the Package Manager:

```sh
Install-Package starkinfra -Version 0.13.0
```

1.2 To install the .NET CLI:

```sh
dotnet add package starkinfra --version 0.13.0
```

1.3 To install by PackageReference:

```sh
<PackageReference Include="starkinfra" Version="0.13.0" />
```

1.4 To install with Packet CLI:

```sh
packet add starkinfra --version 0.13.0
```

## 2. Create your Private and Public Keys

We use ECDSA. That means you need to generate a secp256k1 private
key to sign your requests to our API, and register your public key
with us, so we can validate those requests.

You can use one of the following methods:

2.1. Check out the options in our [tutorial](https://starkbank.com/faq/how-to-create-ecdsa-keys). 

2.2. Use our SDK:

```c#
using System;
using StarkInfra;

(string privateKey, string publicKey) = StarkInfra.Key.Create();

// or, to also save .pem files in a specific path
(string privateKey, string publicKey) = StarkInfra.Key.Create("file/keys");
```

**NOTE**: When you are creating new credentials, it is recommended that you create the
keys inside the infrastructure that will use it, in order to avoid risky internet
transmissions of your **private-key**. Then you can export the **public-key** alone to the
computer where it will be used in the new Project creation.

## 3. Register your user credentials

You can interact directly with our API using two types of users: Projects and Organizations.

- **Projects** are workspace-specific users, that is, they are bound to the workspaces they are created in.
One workspace can have multiple Projects.
- **Organizations** are general users that control your entire organization.
They can control all your Workspaces and even create new ones. The Organization is bound to your company's tax ID only.
Since this user is unique in your entire organization, only one credential can be linked to it.

3.1. To create a Project in Sandbox:

3.1.1. Log into [StarkInfra Sandbox](https://web.sandbox.starkinfra.com)

3.1.2. Go to Menu > Integrations

3.1.3. Click on the "New Project" button

3.1.4. Create a Project: Give it a name and upload the public key you created in section 2

3.1.5. After creating the Project, get its Project ID

3.1.6. Use the Project ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
using System;
using StarkInfra;

string privateKeyContent = "-----BEGIN EC PARAMETERS-----\nBgUrgQQACg==\n-----END EC PARAMETERS-----\n-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIMCwW74H6egQkTiz87WDvLNm7fK/cA+ctA2vg/bbHx3woAcGBSuBBAAK\noUQDQgAE0iaeEHEgr3oTbCfh8U2L+r7zoaeOX964xaAnND5jATGpD/tHec6Oe9U1\nIF16ZoTVt1FzZ8WkYQ3XomRD4HS13A==\n-----END EC PRIVATE KEY-----";

StarkInfra.Project project = new StarkInfra.Project(
    environment: "sandbox",
    id: "5656565656565656",
    privateKey: privateKeyContent
);
```

3.2. To create Organization credentials in Sandbox:

3.2.1. Log into [StarkInfra Sandbox](https://web.sandbox.starkbank.com)

3.2.2. Go to Menu > Integrations

3.2.3. Click on the "Organization public key" button

3.2.4. Upload the public key you created in section 2 (only a legal representative of the organization can upload the public key)

3.2.5. Click on your profile picture and then on the "Organization" menu to get the Organization ID

3.2.6. Use the Organization ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
using System;
using StarkInfra;

string privateKeyContent = "-----BEGIN EC PARAMETERS-----\nBgUrgQQACg==\n-----END EC PARAMETERS-----\n-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIMCwW74H6egQkTiz87WDvLNm7fK/cA+ctA2vg/bbHx3woAcGBSuBBAAK\noUQDQgAE0iaeEHEgr3oTbCfh8U2L+r7zoaeOX964xaAnND5jATGpD/tHec6Oe9U1\nIF16ZoTVt1FzZ8WkYQ3XomRD4HS13A==\n-----END EC PRIVATE KEY-----";

StarkInfra.Organization organization = new StarkInfra.Organization(
    environment: "sandbox",
    id: "5656565656565656",
    privateKey: privateKeyContent,
    workspaceID: null  // You only need to set the workspaceID when you are operating a specific workspaceID
);

// To dynamically use your organization credentials in a specific workspaceID,
// you can use the Organization.Replace() method:
StarkInfra.PixBalance.Get(user: Organization.Replace(organization, "4848484848484848"));
```

NOTE 1: Never hard-code your private key. Get it from an environment variable or an encrypted database.

NOTE 2: We support `'sandbox'` and `'production'` as environments.

NOTE 3: The credentials you registered in `sandbox` do not exist in `production` and vice versa.

## 4. Setting up the user

There are three kinds of users that can access our API: **Organization**, **Project**, and **Member**.

- `Project` and `Organization` are designed for integrations and are the ones meant for our SDKs.
- `Member` is the one you use when you log into our webpage with your e-mail.

There are two ways to inform the user to the SDK:

4.1 Passing the user as an argument in all functions:

```c#
using System;
using StarkInfra;


StarkInfra.PixBalance balance = StarkInfra.PixBalance.Get(user: project); //or organization
```

4.2 Set it as a default user in the SDK:

```c#
using System;
using StarkInfra;


StarkInfra.Settings.User = project; //or organization

StarkInfra.PixBalance balance = StarkInfra.PixBalance.Get();
```

Just select the way of passing the user that is more convenient to you.
On all following examples, we will assume a default user has been set.

## 5. Setting up the error language

The error language can also be set in the same way as the default user:

```c#
StarkInfra.Settings.Language = "en-US";
```

Language options are "en-US" for English and "pt-BR" for Brazilian Portuguese. English is the default.

# Resource listing and manual pagination

Almost all SDK resources provide a `query` and a `page` function.

- The `query` function provides a straightforward way to efficiently iterate through all results that match the filters you inform,
seamlessly retrieving the next batch of elements from the API only when you reach the end of the current batch.
If you are not worried about data volume or processing time, this is the way to go.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixRequest> requests = StarkInfra.PixRequest.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixRequest request in requests) {
    Console.WriteLine(request);
}
```

- The `page` function gives you full control over the API pagination. With each function call, you receive up to
100 results and the cursor to retrieve the next batch of elements. This allows you to stop your queries and
pick up from where you left off whenever it is convenient. When there are no more elements to be retrieved, the returned cursor will be `null`.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixRequest> page;
string cursor = null;

while (true)
{
    (page, cursor) = StarkInfra.PixRequest.Page(cursor: cursor);
    foreach (StarkInfra.PixRequest entity in page)
    {
        Console.WriteLine(entity);
    }
    if (cursor == null)
    {
        break;
    }
}
```

To simplify the following SDK examples, we will only use the `query` function, but feel free to use `page` instead.

# Testing in Sandbox

Your initial balance is zero. For many operations in Stark Infra, you'll need funds
in your account, which can be added to your balance by creating a StarkBank.Invoice. 

In the Sandbox environment, most of the created StarkBank.Invoices will be automatically paid,
so there's nothing else you need to do to add funds to your account. Just create
a few StarkBank.Invoice and wait around a bit.

In Production, you (or one of your clients) will need to actually pay this Pix Request
for the value to be credited to your account.

# Usage

Here are a few examples on how to use the SDK. If you have any doubts, check out
the function or class docstring to get more info or go straight to our [API docs].

## Issuing

### Query IssuingProducts

To take a look at the sub-issuer card products available to you, just run the following:

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingProduct> products = StarkInfra.IssuingProduct.Query()

foreach (StarkInfra.IssuingProduct product in products) {
    Console.Write(product);
}
```

This will tell which card products and card number prefixes you have at your disposal.

### Create IssuingHolders

You can create card holders to which your cards will be bound.
They support spending rules that will apply to all underlying cards.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.IssuingHolder> holders = StarkInfra.IssuingHolder.Create(
    new List<StarkInfra.IssuingHolder> {
        new StarkInfra.IssuingHolder(
            name: "Jamie Lanister",
            externalID : "external_id_12345",
            taxID : "012.345.678-90",
            tags : new List<string> { "Traveler Employee" },
            rules: new List<StarkInfra.IssuingRule> {
                new StarkInfra.IssuingRule(
                    name: "general",
                    interval: "week",
                    amount: 100000,
                    currencyCode: "USD"
                )
            }
        )
    }
);

foreach (StarkInfra.IssuingHolder holder in holders) {
    Console.Write(holder);   
}
```

**Note**: Instead of using IssuingHolder objects, you can also pass each element in dictionary format

### Query IssuingHolders

You can query multiple holders according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingHolder> holders = StarkInfra.IssuingHolder.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach(StarkInfra.IssuingHolder holder in holders)
{
    Console.Write(holder);
}
```

### Cancel an IssuingHolder

To cancel a single Issuing Holder by its id, run:

```c#
using System;
using StarkInfra;


StarkInfra.IssuingHolder holder = StarkInfra.IssuingHolder.Cancel("5353197895942144");

Console.Write(holder);
```

### Get an IssuingHolder

After its creation, information on a holder may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingHolder holder = StarkInfra.IssuingHolder.Get("5353197895942144");

Console.Write(holder);
```

### Query IssuingHolder logs

You can query holder logs to better understand holder life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingHolder.Log> logs = StarkInfra.IssuingHolder.Log.Query(limit: 10);

foreach (StarkInfra.IssuingHolder.Log log in logs)
{
    Console.Write(log);
}
```

### Get an IssuingHolder log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingHolder.Log log = StarkInfra.IssuingHolder.Log.Get("6299741604282368");

Console.Write(log);
```

### Create IssuingCards

You can issue cards with specific spending rules.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.IssuingCard> cards = StarkInfra.IssuingCard.Create(
    new List<StarkInfra.IssuingCard> {
        new StarkInfra.IssuingCard(
            holderName : "Developers",
            holderTaxID : "012.345.678-90",
            holderExternalID : "672",
            rules: new List<StarkInfra.IssuingRule> {
                new StarkInfra.IssuingRule(
                    name: "general",
                    interval: "week",
                    amount: 100000,
                    currencyCode: "USD"
                )
            }
        )
    }
);

foreach(StarkInfra.IssuingCard card in cards)
{
    Console.Write(card);
}
```

### Query IssuingCards

You can get a list of created cards given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingCard> cards = StarkInfra.IssuingCard.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach(StarkInfra.IssuingCard card in cards)
{
    Console.Write(card);
}
```

### Get an IssuingCard

After its creation, information on a card may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingCard card = StarkInfra.IssuingCard.Get("5353197895942144");

Console.Write(card);
```

### Update an IssuingCard

You can update a specific card by its id.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


Dictionary<string, object> patchData = new Dictionary<string, object> {
    { "status", "blocked" }
};

StarkInfra.IssuingCard card = StarkInfra.IssuingCard.Update("5353197895942144", patchData);

Console.Write(card);
```

### Cancel an IssuingCard

You can also cancel a card by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingCard card = StarkInfra.IssuingCard.Cancel("5353197895942144");

Console.Write(card);
```

### Query IssuingCard logs

You can query card logs to better understand card life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingCard.Log> logs = StarkInfra.IssuingCard.Log.Query(limit: 10);

foreach (StarkInfra.IssuingCard.Log log in logs)
{
    Console.Write(log);
}
```

### Get an IssuingIssuingCard log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingCard.Log log = StarkInfra.IssuingCard.Log.Get("6299741604282368");

Console.Write(log);
```

### Query IssuingDesigns

You can get a list of available designs given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingDesign> designs = StarkInfra.IssuingDesign.Query(limit: 10);

foreach (StarkInfra.IssuingDesign design in designs) {
    Console.Write(design);
}
```

### Get an IssuingDesign

Information on a design may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingDesign design = StarkInfra.IssuingDesign.Get("5353197895942144");

Console.Write(design);
```

### Query IssuingEmbossingKits

You can get a list of existing embossing kits given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingEmbossingKit> kits = StarkInfra.IssuingEmbossingKit.Query(
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.IssuingEmbossingKit kit in kits)
{
    Console.Write(kit);
}
```

### Get an IssuingEmbossingKit

After its creation, information on an embossing kit may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingEmbossingKit kit = StarkInfra.IssuingEmbossingKit.Get("5155165527080960");

Console.Write(kit);
```

### Query IssuingStocks

You can get a list of available stocks given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingStock> stocks = StarkInfra.IssuingStock.Query(
    after: new DateTime(2020, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingStock stock in stocks) {
    Console.Write(stock);
}
```

### Get an IssuingStock

Information on a stock may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingStock stock = StarkInfra.IssuingStock.Get("5353197895942144");

Console.Write(stock);
```

### Query IssuingStock logs

Logs are pretty important to understand the life cycle of a stock.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingStock.Log> logs = StarkInfra.IssuingStock.Log.Query(limit: 10);

foreach (StarkInfra.IssuingStock.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingStock log

You can get a single log by its id.

```c#
using System;
using System.Collections.Generic;

StarkInfra.IssuingStock.Log log = StarkInfra.IssuingStock.Log.Get("5353197895942144");

Console.Write(log);
```

### Create IssuingRestocks

You can order restocks for a specific IssuingStock.

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.IssuingRestock> restocks = StarkInfra.IssuingRestock.Create( 
    new List<IssuingRestock>{
        new IssuingRestock(
            count: 100,
            id: "5136459887542272"
        )
    };
);
           
foreach (StarkInfra.IssuingRestock restock in restocks){
    Console.Write(restock);
}
```

### Query IssuingRestocks

You can get a list of created restocks given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingRestock> restocks = StarkInfra.IssuingRestock.Query(
    after: new DateTime(2020, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingRestock restock in restocks){
    Console.Write(restock);
}
```

### Get an IssuingRestock

After its creation, information on a restock may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingRestock restock = StarkInfra.IssuingRestock.Get("5353197895942144");

Console.Write(restock);
```

### Query IssuingRestock logs

Logs are pretty important to understand the life cycle of a restock.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingRestock.Log> logs = StarkInfra.IssuingRestock.Log.Query(limit: 50);

foreach (StarkInfra.IssuingRestock.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingRestock log

You can get a single log by its id.

```c#
using System;

StarkInfra.IssuingRestock.Log log = StarkInfra.IssuingRestock.Log.Get("5353197895942144");

Console.Write(log);
```

### Create IssuingStockRules

You can create notification rules for a specific IssuingStock. When the linked stock balance reaches the minimum balance, the listed emails and phones are notified.

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.IssuingStockRule> rules = StarkInfra.IssuingStockRule.Create(
    new List<IssuingStockRule>{
        new IssuingStockRule(
            minimumBalance: 10000,
            stockID: "5136459887542272",
            emails: new List<string>{ "john.doe@enterprise.com" }
        )
    }
);

foreach (StarkInfra.IssuingStockRule rule in rules){
    Console.Write(rule);
}
```

### Query IssuingStockRules

You can get a list of created stock rules given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingStockRule> rules = StarkInfra.IssuingStockRule.Query(
    after: new DateTime(2020, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingStockRule rule in rules){
    Console.Write(rule);
}
```

### Get an IssuingStockRule

After its creation, information on a stock rule may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingStockRule rule = StarkInfra.IssuingStockRule.Get("5664445921492992");

Console.Write(rule);
```

### Update an IssuingStockRule

You can update a stock rule's minimumBalance, tags, emails and phones by its id.

```c#
using System;

StarkInfra.IssuingStockRule rule = StarkInfra.IssuingStockRule.Update("5664445921492992", minimumBalance: 20000);

Console.Write(rule);
```

### Cancel an IssuingStockRule

To cancel a single stock rule by its id, run:

```c#
using System;

StarkInfra.IssuingStockRule rule = StarkInfra.IssuingStockRule.Cancel("5664445921492992");

Console.Write(rule);
```

### Create IssuingEmbossingRequests

You can create a request to emboss a physical card.

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.IssuingEmbossingRequest> requests = StarkInfra.IssuingEmbossingRequest.Create(
    new List<IssuingEmbossingRequest>() { 
        new IssuingEmbossingRequest(
            cardID: "5714424132272128", 
            cardDesignID: "5648359658356736", 
            displayName1: "Antonio Stark", 
            envelopeDesignID: "5747368922185728", 
            shippingCity: "Sao Paulo", 
            shippingCountryCode: "BRA", 
            shippingDistrict: "Bela Vista", 
            shippingService: "loggi", 
            shippingStateCode: "SP", 
            shippingStreetLine1: "Av. Paulista, 200", 
            shippingStreetLine2: "10 andar", 
            shippingTrackingNumber: "My_custom_tracking_number", 
            shippingZipCode: "12345-678",
            embosserID: "5746980898734080"
        );
    }
);

foreach (StarkInfra.IssuingEmbossingRequest request in requests){
    Console.Write(request);
}
```

### Query IssuingEmbossingRequests

You can get a list of created embossing requests given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingEmbossingRequest> requests = StarkInfra.IssuingEmbossingRequest.Query(
    after: new DateTime(2020, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingEmbossingRequest request in requests){
    Console.Write(request);
}
```

### Get an IssuingEmbossingRequest

After its creation, information on an embossing request may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingEmbossingRequest request = StarkInfra.IssuingEmbossingRequest.Get("5353197895942144");

Console.Write(request);
```

### Query IssuingEmbossingRequest logs

Logs are pretty important to understand the life cycle of an embossing request.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingEmbossingRequest.Log> logs = StarkInfra.IssuingEmbossingRequest.Log.Query(
    after: new DateTime(2020, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingEmbossingRequest.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingEmbossingRequest log

You can get a single log by its id.

```c#
using System;

StarkInfra.IssuingEmbossingRequest.Log log = StarkInfra.IssuingEmbossingRequest.Log.Get("5353197895942144");

Console.Write(log);
```

### Process Purchase authorizations

It's easy to process purchase authorizations delivered to your endpoint.
Remember to pass the signature header so the SDK can make sure it's StarkInfra that sent you the event.
If you do not approve or decline the authorization within 2 seconds, the authorization will be denied.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


Response response = listen();  // this is the method you made to get the events posted to your webhook endpoint

StarkInfra.IssuingPurchase authorization = StarkInfra.IssuingPurchase.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
);

sendResponse(  // you should also implement this method
    StarkInfra.IssuingPurchase.Response(  // this optional method just helps you build the response JSON
        status: "accepted",
        amount: authorization.amount,
        tags= new List<string> { "my-purchase-id/123"}
    )
)

// or 

sendResponse(
    StarkInfra.IssuingPurchase.Response(
        status: "denied",
        reason: "other",
        tags: new List<string> { "other-id/456" },
    )
)
```

### Query IssuingPurchases

You can get a list of created purchases given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingPurchase> purchases = StarkInfra.IssuingPurchase.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingPurchase purchase in purchases)
{
    Console.Write(purchase);
}
```

### Get an IssuingPurchase

After its creation, information on a purchase may be retrieved by its id. 

```c#
using System;

StarkInfra.IssuingPurchase purchase = StarkInfra.IssuingPurchase.Get("5642359077339136");

Console.Write(purchase);
```

### Query IssuingPurchase logs

You can query purchase logs to better understand purchase life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingPurchase.Log> logs = StarkInfra.IssuingPurchase.Log.Query(limit: 10);

foreach (StarkInfra.IssuingPurchase.Log log in logs)
{
    Console.Write(log);
}
```

### Get an IssuingPurchase log

You can get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingPurchase.Log log = StarkInfra.IssuingPurchase.Log.Get("6428086769811456");

Console.Write(log);
```


### Create IssuingInvoices

You can create Pix invoices to transfer money from accounts you have in any bank to your Issuing balance,
allowing you to run your issuing operation.

```c#
using System;

StarkInfra.IssuingInvoice invoice = StarkInfra.IssuingInvoice.Create(
    new StarkInfra.IssuingInvoice(
        amount: 10000
    )
);
    
Console.Write(invoice);
```

**Note**: Instead of using IssuingInvoice objects, you can also pass each element in dictionary format

### Query IssuingInvoices

You can get a list of created invoices given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingInvoice> invoices = StarkInfra.IssuingInvoice.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingInvoice invoice in invoices)
{
    Console.Write(invoice);
}
```

### Get an IssuingInvoice

After its creation, information on an invoice may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingInvoice invoice = StarkInfra.IssuingInvoice.Get("5709933853016064");

Console.Write(invoice);
```

### Query IssuingInvoice logs

You can query invoice logs to better understand invoice life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingInvoice.Log> logs = StarkInfra.IssuingInvoice.Log.Query(limit: 10);

foreach (StarkInfra.IssuingInvoice.Log log in logs)
{
    Console.Write(log);
}
```

### Get IssuingInvoice logs

Logs are pretty important to understand the life cycle of an invoice.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


StarkInfra.IssuingInvoice.Log log = StarkInfra.IssuingInvoice.Log.Get("4649340324806656");

Console.Write(log);
```

### Create IssuingWithdrawals

You can create withdrawals to send cash back from your Issuing balance to your Banking balance
by using the Withdrawal resource.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingWithdrawal withdrawal = StarkInfra.IssuingWithdrawal.Create(
    new StarkInfra.IssuingWithdrawal(
        amount: 10000,
        externalID: "3257",
        description: "Sending back"
    )
);

Console.Write(withdrawal);
```

**Note**: Instead of using IssuingWithdrawal objects, you can also pass each element in dictionary format

### Get an IssuingWithdrawal

After its creation, information on a withdrawal may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingWithdrawal withdrawal = StarkInfra.IssuingWithdrawal.Get("5440727945314304");

Console.Write(withdrawal);
```

### Query IssuingWithdrawals

You can get a list of created withdrawals given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingWithdrawal> withdrawals = StarkInfra.IssuingWithdrawal.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingWithdrawal withdrawal in withdrawals)
{
    Console.Write(withdrawal);
}
```

### Get your IssuingBalance

To know how much money you have available to run authorizations, run:

```c#
using System;
using StarkInfra;


StarkInfra.IssuingBalance balance = StarkInfra.IssuingBalance.Get();

Console.Write(balance);
```

### Query IssuingTransactions

To understand your balance changes (issuing statement), you can query
transactions. Note that our system creates transactions for you when
you make purchases, withdrawals, receive issuing invoice payments, for example.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IssuingTransaction> transactions = StarkInfra.IssuingTransaction.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingTransaction transaction in transactions)
{
    Console.Write(transaction);
}
```

### Get an IssuingTransaction

After its creation, information on a transaction may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IssuingTransaction transaction = StarkInfra.IssuingTransaction.Get("6539944898068480");

Console.Write(transaction);
```

### Issuing Enums

#### Query MerchantCategories

You can query any merchant categories using this resource.
You may also use MerchantCategories to define specific category filters in IssuingRules.
Either codes (which represents specific MCCs) or types (code groups) will be accepted as filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.MerchantCategory> categories = StarkInfra.MerchantCategory.Query(
    search: "food"
);

foreach (StarkInfra.MerchantCategory category in categories)
{
    Console.Write(category);
}
```

#### Query MerchantCountries

You can query any merchant countries using this resource.
You may also use MerchantCountries to define specific country filters in IssuingRules.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.MerchantCountry> countries = StarkInfra.MerchantCountry.Query(
    search: "brazil"
);

foreach (StarkInfra.MerchantCountry country in countries)
{
    Console.Write(country);
}
```

#### Query CardMethods

You can query available card methods using this resource.
You may also use CardMethods to define specific purchase method filters in IssuingRules.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.CardMethod> methods = StarkInfra.CardMethod.Query(
    search: "token"
);

foreach (StarkInfra.CardMethod method in methods)
{
    Console.Write(method);
}
```

## Pix

### Create PixRequests

You can create a Pix request to transfer money from one of your users to anyone else:

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixRequest> requests = StarkInfra.PixRequest.Create(
    new List<StarkInfra.PixRequest> {
        new StarkInfra.PixRequest(
            amount: 100,  // (R$ 1.00)
            externalID: "141234121",  // so we can block anything you send twice by mistake
            senderBranchCode: "0000",
            senderAccountNumber: "00000-0",
            senderAccountType: "checking",
            senderName: "Tyrion Lannister",
            senderTaxID: "012.345.678-90",
            receiverBankCode: "00000001",
            receiverBranchCode: "0001",
            receiverAccountNumber: "00000-1",
            receiverAccountType: "checking",
            receiverName: "Jamie Lannister",
            receiverTaxID: "45.987.245/0001-92",
            endToEndID: EndToEndID.Create(bankCode: Environment.GetEnvironmentVariable("BANK_CODE")),
            description: "For saving my life",
            reason: "subscriptionFlaw",
            priority: "low"
        ),
        new StarkInfra.PixRequest(
            amount: 200,  // (R$ 2.00)
            externalID: "2135613462",  // so we can block anything you send twice by mistake
            senderAccountNumber: "00000-0",
            senderBranchCode: "0000",
            senderAccountType: "checking",
            senderName: "Arya Stark",
            senderTaxID: "012.345.678-90",
            receiverBankCode: "00000001",
            receiverAccountNumber: "00000-1",
            receiverBranchCode: "0001",
            receiverAccountType: "checking",
            receiverName: "John Snow",
            receiverTaxID: "012.345.678-90",
            endToEndID: EndToEndID.Create(bankCode: Environment.GetEnvironmentVariable("BANK_CODE")),
            tags: new List<string> { "Needle", "sword" },
            reason: "subscriptionFlaw",
            priority: "low"
        )
    }
);

foreach(StarkInfra.PixRequest request in requests) {
    Console.WriteLine(request);
}
```

**Note**: Instead of using PixRequest objects, you can also pass each element in dictionary format

### Query PixRequests

You can query multiple Pix requests according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixRequest> requests = StarkInfra.PixRequest.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixRequest request in requests) {
    Console.WriteLine(request);
}
```

### Get a PixRequest

After its creation, information on a request may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixRequest request = StarkInfra.PixRequest.Get("5155165527080960");

Console.WriteLine(request);
```

### Process inbound PixRequest authorizations

It's easy to process authorization requests that arrived at your endpoint.
Remember to pass the signature header so the SDK can make sure it's StarkInfra that sent you the event.
If you do not approve or decline the authorization within 1 second, the authorization will be denied.

```c#
using System;
using StarkInfra;


Response response = listen(); // this is your handler to listen for authorization requests

StarkInfra.PixRequest request= StarkInfra.PixRequest.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
)

Console.WriteLine(request);

sendResponse( // you should also implement this method
    StarkInfra.PixRequest.Response( // this optional method just helps you build the response JSON
        status: "approved"
    )
)

// or

sendResponse( // you should also implement this method
    StarkInfra.PixRequest.Response( // this optional method just helps you build the response JSON
        status: "denied",
        reason: "orderRejected"
    )
)
```

### Query PixRequest logs

You can query Pix request logs to better understand PixRequest life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixRequest.Log> logs = StarkInfra.PixRequest.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkInfra.PixRequest.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixRequest log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixRequest.Log log = StarkInfra.PixRequest.Log.Get("4701727546671104");

Console.WriteLine(log);
```

### Create PixReversals

You can reverse a PixRequest either partially or totally using a PixReversal.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixReversal> reversal = StarkInfra.PixReversal.Create(
    new List<StarkInfra.PixReversal> {
        new StarkInfra.PixReversal(
            amount: 100,
            endToEndID: "E20018183202201060100rzsJzG9PzMg",
            externalID: "17238435823958934",
            reason: "bankError"
        )
    }
);

foreach(StarkInfra.PixReversal reversal in reversals) {
    Console.WriteLine(reversal);
}
```

### Query PixReversals 

You can query multiple Pix reversals according to filters. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixReversal> reversals = StarkInfra.PixReversal.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixReversal reversal in reversals) {
    Console.WriteLine(reversal);
}
```

### Get a PixReversal

After its creation, information on a Pix reversal may be retrieved by its id.
Its status indicates whether it has been successfully processed.

```c#
using System;
using StarkInfra;


StarkInfra.PixReversal reversal = StarkInfra.PixReversal.Get("5155165527080960");

Console.WriteLine(reversal);
```

### Process inbound PixReversal authorizations

It's easy to process authorization requests that arrived at your endpoint.
Remember to pass the signature header so the SDK can make sure it's StarkInfra that sent you the event.
If you do not approve or decline the authorization within 1 second, the authorization will be denied.

```c#
using System;
using StarkInfra;


Response response = listen(); // this is your handler to listen for authorization reversals

StarkInfra.PixReversal reversal= StarkInfra.PixReversal.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
)

Console.WriteLine(reversal);

sendResponse( // you should also implement this method
    StarkInfra.PixReversal.Response( // this optional method just helps you build the response JSON
        status: "approved"
    )
)

// or

sendResponse( // you should also implement this method
    StarkInfra.PixReversal.Response( // this optional method just helps you build the response JSON
        status: "denied",
        reason: "orderRejected"
    )
)
```

### Query PixReversal logs

You can query PixReversal logs to better understand PixReversal life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixReversal.Log> logs = StarkInfra.PixReversal.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkInfra.PixReversal.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixReversal log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixReversal.Log log = StarkInfra.PixReversal.Log.Get("4701727546671104");

Console.WriteLine(log);
```

### Get your PixBalance 

To see how much money you have in your account, run:

```c#
using System;
using StarkInfra;


StarkInfra.PixBalance balance = StarkInfra.PixBalance.Get();

Console.WriteLine(reversal);
```

### Create a PixStatement

Statements are generated directly by the Central Bank and are only available for direct participants.
To create a statement of all the transactions that happened on your account during a specific day, run:

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixStatement> statement = StarkInfra.PixStatement.Create(
    new List<StarkInfra.PixStatement> {
        new StarkInfra.PixStatement(
            after: DateTime.Today.Date.AddDays(-1), // This is the date that you want to create a statement.
            before: DateTime.Today.Date.AddDays(-1), // After and before must be the same date.
            type: "transaction" // Options are "interchange", "interchangeTotal", "transaction".
        )
    }
);

foreach(StarkInfra.PixStatement statement in statements) {
    Console.WriteLine(statement);
}
```

### Query PixStatements

You can query multiple PixStatements according to filters. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixStatement> statements = StarkInfra.PixStatement.Query(
    limit: 50,
    after: DateTime.Today.Date.AddDays(-10), // Note that this after and before parameters are different from the ones used in the creation of the statement.
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixStatement statement in statements) {
    Console.WriteLine(statement);
}
```

### Get a PixStatement

After its creation, information on a statement may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixStatement statement = StarkInfra.PixStatement.Get("5155165527080960");

Console.WriteLine(statement);
```

### Get a PixStatement .csv file

To get a .csv file of a Pix statement using its id, run:

```c#
using System;
using StarkInfra;


byte[] csv = StarkInfra.PixStatement.Csv("5155165527080960");

System.IO.File.WriteAllBytes("statement.zip", csv);
```

### Create a PixKey

You can create a PixKey to link a bank account information to a key id:

```c#
using System;
using StarkInfra;


StarkInfra.PixKey key = StarkInfra.PixKey.Create(
    new StarkInfra.PixKey(
        accountCreated: new DateTime(2022, 02, 01),
        accountNumber: "00000",
        accountType: "savings",
        branchCode: "0000",
        name: "Jamie Lannister",
        taxID: "012.345.678-90",
        id: "+5511989898989"
    )
);

Console.Write(key);
```

### Query PixKeys

You can query multiple PixKeys you own according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixKey> keys = StarkInfra.PixKey.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 01, 12),
    status: new List<string> { "created" },
    ids: new List<string> { "+5511989898989" },
    type: "phone"
);

foreach (StarkInfra.PixKey key in keys)
{
    Console.Write(key);
}
```

### Get a PixKey

Information on a Pix key may be retrieved by its id and the tax ID of the consulting agent.
An endToEndID must be informed so you can link any resulting purchases to this query,
avoiding sweep blocks by the Central Bank.

```c#
using System;
using StarkInfra.Utils;
using StarkInfra;


StarkInfra.PixKey key = StarkInfra.PixKey.Get(
    id: "5155165527080960",
    payerID: "012.345.678-90",
    parameters: new Dictionary<string, object> {
        { "endToEndID", EndToEndID.Create("20018183") }
    }
);

Console.Write(key);
```

### Update a PixKey

Update the account information linked to a Pix key.

```c#
using System;
using StarkInfra;


StarkInfra.PixKey key = StarkInfra.PixKey.Update(
    id: "+5511998989898",
    reason: "branchTransfer",
    patchData: new Dictionary<string, object> {
        { "name", "Jamie Lannister" }
    }
);

Console.Write(key);
```

### Cancel a PixKey

Cancel a specific Pix key using its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixKey key = StarkInfra.PixKey.Cancel("+5511912345678");

Console.Write(key);
```

### Query PixKey logs

You can query PixKey logs to better understand a PixKey life cycle.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixKey.Log> logs = StarkInfra.PixKey.Log.Query(
    limit: 50,
    ids: new List<string> { "6126693430525952" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 05, 20),
    types: new List<string> { "created" },
    keyIds: new List<string> { "+5511912345678" }
);

foreach(StarkInfra.PixKey.Log log in logs)
{
    Console.Write(log);
}
```

### Get a PixKey log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixKey.Log log = StarkInfra.PixKey.Log.Get("5566693430525952");

Console.Write(log);
```

### Create a PixClaim

You can create a PixClaim to request the transfer of a Pix key from another bank to one of your accounts:

```c#
using System;
using StarkInfra;


StarkInfra.PixClaim claim = StarkInfra.PixClaim.Create(
    new StarkInfra.PixClaim(
        accountCreated: new DateTime(2022, 02, 01),
        accountNumber: "5692908409716736",
        accountType: "checking",
        branchCode: "0001",
        name: "testKey",
        taxID: "012.345.678-90",
        keyID: "+5511989298469"
    )
);

Console.Write(claim);
```

### Query PixClaims

You can query multiple PixClaims according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixClaim> claims = StarkInfra.PixClaim.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 09, 12),
    status: new List<string> { "delivered" },
    ids: new List<string> { "6481646396112896" },
    type: "ownership",
    agent: "claimer",
    keyType: "phone",
    keyID: "+5511989298469"
);

foreach(StarkInfra.PixClaim claim in claims)
{
    Console.Write(claim);
}
```

### Get a PixClaim

After its creation, information on a PixClaim may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixClaim claim = StarkInfra.PixClaim.Get("6481646396112896");

Console.Write(claim);
```

### Update a PixClaim

A PixClaim can be confirmed or canceled by patching its status.
A received PixClaim must be confirmed by the donor to be completed.
Ownership PixClaims can only be canceled by the donor if the reason is "fraud".
A sent PixClaim can also be canceled.

```c#
using System;
using StarkInfra;


Dictionary<string, object> patchData = new Dictionary<string, object> {
    { "status", "canceled" }
};

StarkInfra.PixClaim claim = StarkInfra.PixClaim.Update(id: "4508444895739904", patchData: patchData);

Console.Write(claim);
```

### Query PixClaim logs

You can query PixClaim logs to better understand Pix claim life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixClaim.Log> logs = StarkInfra.PixClaim.Log.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "canceled" },
    claimIds: new List<string> { "4508444895739904" }
);

foreach(StarkInfra.PixClaim.Log log in logs)
{
    Console.Write(log);
}
```

### Get a PixClaim log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixClaim.Log log = StarkInfra.PixClaim.Log.Get("4598641893310464");

Console.Write(log);
```

### Create a PixDirector

To register the Pix director contact information at the Central Bank, run the following:

```c#
using System;
using StarkInfra;


StarkInfra.PixDirector director = StarkInfra.PixDirector.Create(
    new StarkInfra.PixDirector(
        name: "Edward Stark",
        taxID: "012.345.678-90",
        phone: "+5511999999999",
        email: "ned.stark@company.com",
        password: "12345678",
        teamEmail: "pix.team@company.com",
        teamPhones: new List<string> { "+5511988889999", "+5511988889998" }
    )
);

Console.Write(director);
```

### Create PixInfractions

Pix Infraction reports are used to report transactions that raise fraud suspicion, to request a refund or to
reverse a refund. Infraction reports can be created by either participant of a transaction.

```c#
using System;
using StarkInfra;


StarkInfra.PixInfraction infractions = StarkInfra.PixInfraction.Create(
    new List<StarkInfra.PixInfraction>{
        new StarkInfra.PixInfraction(
            referenceID: "E20018183202204951450u34sDGd19lz",
            type: "fraud",
            method: "scam",
            operatorEmail: "fraud@company.com",
            operatorPhone: "+5511989898989"
        )
    }
);

foreach(StarkInfra.PixInfraction infraction in infractions)
{
    Console.Write(infraction);
}
```

### Query PixInfractions

You can query multiple infraction reports according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixInfraction> infractions = StarkInfra.PixInfraction.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: "created",
    ids: new List<string> { "5724541800153088" }
);

foreach(StarkInfra.PixInfraction infraction in infractions)
{
    Console.Write(infraction);
}
```

### Get a PixInfraction

After its creation, information on a Pix Infraction may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Get("5724541800153088");

Console.Write(infraction);
```

### Update a PixInfraction

A received Pix infraction can be confirmed or declined by patching its status.
After a Pix infraction is patched, its status changes to closed.

```c#
using System;
using StarkInfra;


StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Update(id: "5586201146818560", result: "agreed");

Console.Write(infraction);
```

### Cancel a PixInfraction

Cancel a specific Pix Chargeback using its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Cancel("5586201146818560");

Console.Write(infraction);
```
### Query PixInfraction logs

You can query Pix infraction logs to better understand their life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixInfraction.Log> logs = StarkInfra.PixInfraction.Log.Query(
    limit: 50,
    ids: new List<string> { "6307030096674816" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "closed" },
    infractionIds: new List<string> { "5586201146818560" }
);

foreach(StarkInfra.PixInfraction.Log log in logs)
{
    Console.Write(log);
}
```

### Get a PixInfraction log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixInfraction.Log log = StarkInfra.PixInfraction.Log.Get("6307030096674816");

Console.Write(log);
```

### Create PixFrauds

Pix Fraud reports are used to report a PixKey or taxId when a fraud has been confirmed.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixFraud> frauds = StarkInfra.PixFraud.Create(
    new List<StarkInfra.PixFraud>{
        new StarkInfra.PixFraud(
            externalID: "my_external_id",
            type: "scam",
            taxID: "01234567890",
            tags: new List<string> { "fraudulent" }
        )
    }
);

foreach(StarkInfra.PixFraud fraud in frauds)
{
    Console.Write(fraud);
}
```

### Query PixFrauds

You can query multiple Pix Fraud reports according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixFraud> frauds = StarkInfra.PixFraud.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string> { "registered" },
    ids: new List<string> { "5724541800153088" },
    bacenID: "ccf9bd9c-e99d-999e-bab9-b999ca999f99",
    type: new List<string> { "reversal" }
);

foreach(StarkInfra.PixFraud fraud in frauds)
{
    Console.Write(fraud);
}
```

### Get a PixFraud

After its creation, information on a Pix Fraud report may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixFraud fraud = StarkInfra.PixFraud.Get("5724541800153088");

Console.Write(fraud);
```

### Delete a PixFraud

Delete a specific Pix Fraud report using its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixFraud fraud = StarkInfra.PixFraud.Delete("5586201146818560");

Console.Write(fraud);
```

### Query PixFraud logs

You can query Pix Fraud logs to better understand their life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixFraud.Log> logs = StarkInfra.PixFraud.Log.Query(
    limit: 50,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "registered" },
    fraudIds: new List<string> { "5586201146818560" },
    ids: new List<string> { "6307030096674816" }
);

foreach(StarkInfra.PixFraud.Log log in logs)
{
    Console.Write(log);
}
```

### Get a PixFraud log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixFraud.Log log = StarkInfra.PixFraud.Log.Get("6307030096674816");

Console.Write(log);
```

### Create PixKeyHolmes

A PixKeyHolmes investigates the registration status of a Pix Key in the Central
Bank's DICT. Open one per key you want to check; the API resolves it asynchronously
and reports back whether the key is registered.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixKeyHolmes> holmes = StarkInfra.PixKeyHolmes.Create(
    new List<StarkInfra.PixKeyHolmes>() {
        new StarkInfra.PixKeyHolmes(
            keyID: "valid@sandbox.com",
            tags: new List<string>{ "travel", "food" }
        )
    }
);

foreach (StarkInfra.PixKeyHolmes sherlock in holmes)
{
    Console.Write(sherlock);
}
```

### Query PixKeyHolmes

You can query multiple PixKeyHolmes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixKeyHolmes> holmes = StarkInfra.PixKeyHolmes.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "solved" }
);

foreach(StarkInfra.PixKeyHolmes sherlock in holmes)
{
    Console.Write(sherlock);
}
```

### Create PixInternalTransactionReports

Transactions that happen internally — outside of the SPI — must be reported to the
Central Bank so they are reflected in the participant's statements. You can create a
PixInternalTransactionReport for each such transaction:

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixInternalTransactionReport> reports = StarkInfra.PixInternalTransactionReport.Create(
    new List<StarkInfra.PixInternalTransactionReport> {
        new StarkInfra.PixInternalTransactionReport(
            amount: 1234,  // (R$ 12.34)
            created: DateTime.Now,
            endToEndID: EndToEndID.Create(bankCode: Environment.GetEnvironmentVariable("SANDBOX_BANKCODE")),
            method: "manual",
            referenceType: "request",
            senderAccountNumber: "00000-0",
            senderBranchCode: "0000",
            senderAccountType: "checking",
            senderBankCode: Environment.GetEnvironmentVariable("SANDBOX_BANKCODE"),
            senderTaxID: "012.345.678-90",
            receiverAccountNumber: "00000-1",
            receiverBranchCode: "0001",
            receiverAccountType: "checking",
            receiverBankCode: "20018183",
            receiverTaxID: "45.987.245/0001-92"
        )
    }
);

foreach(StarkInfra.PixInternalTransactionReport report in reports) {
    Console.WriteLine(report);
}
```

**Note**: Instead of using PixInternalTransactionReport objects, you can also pass each element in dictionary format

### Query PixInternalTransactionReports

You can query multiple PixInternalTransactionReports according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixInternalTransactionReport> reports = StarkInfra.PixInternalTransactionReport.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixInternalTransactionReport report in reports) {
    Console.WriteLine(report);
}
```

### Get a PixInternalTransactionReport

After its creation, information on a report may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixInternalTransactionReport report = StarkInfra.PixInternalTransactionReport.Get("5155165527080960");

Console.WriteLine(report);
```

### Query PixInternalTransactionReport logs

You can query PixInternalTransactionReport logs to better understand PixInternalTransactionReport life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixInternalTransactionReport.Log> logs = StarkInfra.PixInternalTransactionReport.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkInfra.PixInternalTransactionReport.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixInternalTransactionReport log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixInternalTransactionReport.Log log = StarkInfra.PixInternalTransactionReport.Log.Get("4701727546671104");

Console.WriteLine(log);
```

### Create PixChargebacks

Pix chargebacks can be created when fraud is detected on a transaction or a system malfunction 
results in an erroneous transaction.

```c#
using System;
using StarkInfra;


StarkInfra.PixChargeback chargebacks = StarkInfra.PixChargeback.Create(
    new List<StarkInfra.PixChargeback>{
        new StarkInfra.PixChargeback(
            amount: 100,
            referenceID: "E20018183202201201450u34sDGd19lz",
            reason: "fraud"
        )
    }
);

foreach(StarkInfra.PixChargeback chargeback in chargebacks)
{
    Console.Write(chargeback);
}
```

### Query PixChargebacks

You can query multiple PixChargebacks according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixChargeback> chargebacks = StarkInfra.PixChargeback.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: "delivered",
    ids: new List<string> { "6689875965247488" }
);

foreach(StarkInfra.PixChargeback chargeback in chargebacks)
{
    Console.Write(chargeback);
}
```
### Get a PixChargeback

After its creation, information on a Pix Chargebacks may be retrieved by its.

```c#
using System;
using StarkInfra;


StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Get("6689875965247488");

Console.Write(chargeback);
```

### Update a PixChargeback

A received Pix Chargeback can be accepted or rejected by patching its status.
After a Pix Chargeback is patched, its status changes to closed.

```c#
using System;
using StarkInfra.Utils;
using StarkInfra;


StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Update(
    id: "6689875965247488",
    result: "accepted",
    patchData: new Dictionary<string, object> {
        { "reversalReferenceID", ReturnID.Create("20018183") }
    }
);

Console.Write(chargeback);
```

### Cancel a PixChargeback

Cancel a specific Pix reversal using its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Cancel("6689875965247488");

Console.Write(chargeback);
```

### Query PixChargeback logs

You can query PixChargeback logs to better understand Pix chargeback life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixChargeback.Log> logs = StarkInfra.PixChargeback.Log.Query(
    limit: 50,
    ids: new List<string> { "5036445332930560" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "closed" },
    chargebackIds: new List<string> { "4806677870149632" }
);

foreach (StarkInfra.PixChargeback.Log log in logs)
{
    Console.Write(log);
}
```
### Get a PixChargeback log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixChargeback.Log log = StarkInfra.PixChargeback.Log.Get("5036445332930560");

Console.Write(log);
```

### Query PixDomains

Here you can list all Pix Domains registered at the Brazilian Central Bank. The Pix Domain object displays the domain
name and the QR Code domain certificates of registered Pix participants able to issue dynamic QR Codes.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixDomain> domains = StarkInfra.PixDomain.Query();

foreach (StarkInfra.PixDomain domain in domains)
{
    Console.Write(domain);
}
```

### Create StaticBrcodes

StaticBrcodes store account information via a BR code or an image (QR code)
that represents a PixKey and a few extra fixed parameters, such as an amount 
and a reconciliation ID. They can easily be used to receive Pix transactions.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.StaticBrcode> brcodes = StarkInfra.StaticBrcode.Create(
    new List<StarkInfra.StaticBrcode>
    {
        new StarkInfra.StaticBrcode(
            name: "Jamie Lannister",
            keyID: "+5511988887777",
            amount: 100,
            reconciliationID: "123",
            city: "Rio de Janeiro"
        )
    }
);

foreach (StarkInfra.StaticBrcode brcode in brcodes)
{
    Console.Write(brcode);
}
```

### Query StaticBrcodes

You can query multiple StaticBrcodes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.StaticBrcode> brcodes = StarkInfra.StaticBrcode.Query(
    limit: 1,
    after: new DateTime(2022, 06, 01),
    before: new DateTime(2022, 06, 30),
    uuids: new List<string> { "5ddde28043a245c2848b08cf315effa2" }
);

foreach (StarkInfra.StaticBrcode brcode in brcodes)
{
    Console.Write(brcode);
}
```

### Get a StaticBrcodes

After its creation, information on a StaticBrcode may be retrieved by its UUID.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


StarkInfra.StaticBrcode brcode = StarkInfra.StaticBrcode.Get("5ddde28043a245c2848b08cf315effa2");

Console.Write(brcode);
```

### Create DynamicBrcodes

BR codes store information represented by Pix QR Codes, which are used to send 
or receive Pix transactions in a convenient way.
DynamicBrcodes represent charges with information that can change at any time,
since all data needed for the payment is requested dynamically to an URL stored
in the BR Code. Stark Infra will receive the GET request and forward it to your
registered endpoint with a GET request containing the UUID of the BR code for
identification.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.DynamicBrcode> brcodes = StarkInfra.DynamicBrcode.Create(
    new List<StarkInfra.DynamicBrcode>
    {
        new StarkInfra.DynamicBrcode(
            name: "Jamie Lannister",
            city: "Rio de Janeiro",
            externalID: "my_unique_id_01",
            type: "instant"
        )
    }
);

foreach (StarkInfra.DynamicBrcode brcode in brcodes)
{
    Console.Write(brcode);
}
```

### Query DynamicBrcodes

You can query multiple DynamicBrcodes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.DynamicBrcode> brcodes = StarkInfra.DynamicBrcode.Query(
    limit: 1,
    after: new DateTime(2022, 06, 01),
    before: new DateTime(2022, 06, 30),
    uuids: new List<string> { "ac7caa14e601461dbd6b12bf7e4cc48e" }
);

foreach (StarkInfra.DynamicBrcode brcode in brcodes)
{
    Console.Write(brcode);
}
```

### Get a DynamicBrcode

After its creation, information on a DynamicBrcode may be retrieved by its UUID.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


StarkInfra.DynamicBrcode brcode = StarkInfra.DynamicBrcode.Get("ac7caa14e601461dbd6b12bf7e4cc48e");

Console.Write(brcode);
```

### Verify a DynamicBrcode read

When a DynamicBrcode is read by your user, a GET request will be made to the your registered URL to 
retrieve additional information needed to complete the transaction.
Use this method to verify the authenticity of a GET request received at your registered endpoint.
If the provided digital signature does not check out with the StarkInfra public key, a Error.InvalidSignatureException will be raised.

```c#
using System;
using StarkInfra;


Response response = listen();  // this is the method you made to get the read requests posted to your registered endpoint

string uuid = StarkInfra.DynamicBrcode.Verify(
    uuid: response.Url.getParameter("uuid"),
    signature: response.Headers["Digital-Signature"]
);
```

### Answer to a Due DynamicBrcode read

When a Due DynamicBrcode is read by your user, a GET request containing 
the BR code UUID will be made to your registered URL to retrieve additional 
information needed to complete the transaction.

The GET request must be answered in the following format within 5 seconds 
and with an HTTP status code 200.

```c#
using System;
using StarkInfra;


Response response = listen();  // this is the method you made to get the read requests posted to your registered endpoint

string uuid = StarkInfra.DynamicBrcode.Verify(
    uuid: response.Url.getParameter("uuid"),
    signature: response.Headers["Digital-Signature"]
);

List<StarkInfra.Invoice> invoice = getMyInvoice(uuid); // you should implement this method to get the information of the BR code from its uuid

sendResponse(  // you should also implement this method to respond the read request
    StarkInfra.DynamicBrcode.responseDue(
        version: invoice.version,
        created: invoice.created,
        due: invoice.due,
        keyID: invoice.keyID,
        status: invoice.status,
        reconciliationID: invoice.reconciliationID,
        amount: invoice.amount,
        senderName: invoice.senderName,
        senderTaxID: invoice.senderTaxID,
        receiverName: invoice.receiverName,
        receiverTaxID: invoice.receiverTaxid,
        receiverStreetLine: invoice.receiverStreetLine,
        receiverCity: invoice.receiverCity,
        receiverStateCode: invoice.receiverStateCode,
        receiverZipCode: invoice.receiverZipCode
    );
);
```

### Answer to an Instant DynamicBrcode read

When an Instant DynamicBrcode is read by your user, a GET request 
containing the BR code UUID will be made to your registered URL to retrieve 
additional information needed to complete the transaction.

The get request must be answered in the following format 
within 5 seconds and with an HTTP status code 200.

```c#
using System;
using StarkInfra;


Response response = listen();  // this is the method you made to get the read requests posted to your registered endpoint

string uuid = StarkInfra.DynamicBrcode.Verify(
    uuid: response.Url.getParameter("uuid"),
    signature: response.Headers["Digital-Signature"]
);

List<StarkInfra.Invoice> invoice = getMyInvoice(uuid); // you should implement this method to get the information of the BR code from its uuid

sendResponse(  // you should also implement this method to respond the read request
    StarkInfra.DynamicBrcode.responseInstant(
        version: invoice.version,
        created: invoice.created,
        keyID: invoice.keyID,
        status: invoice.status,
        reconciliationID: invoice.reconciliationID,
        amount: invoice.amount,
        cashierType: invoice.cashierType,
        cashierBankCode: invoice.cashierBankCode,
        cashAmount: invoice.cashAmount
    );
);
```

### Create BrcodePreviews
You can create BrcodePreviews to preview BR Codes before paying them.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.BrcodePreview> previews = StarkInfra.BrcodePreview.Create(
    new List<StarkInfra.BrcodePreview>
    {
        new StarkInfra.BrcodePreview(
            id: "00020126420014br.gov.bcb.pix0120nedstark@hotmail.com52040000530398654075000.005802BR5909Ned Stark6014Rio de Janeiro621605126674869738606304FF71"
        ),
        new StarkInfra.BrcodePreview(
            id: "00020126430014br.gov.bcb.pix0121aryastark@hotmail.com5204000053039865406100.005802BR5910Arya Stark6014Rio de Janeiro6216051262678188104863042BA4"
        )
    }
);

foreach (StarkInfra.BrcodePreview preview in previews)
{
    Console.Write(preview);
}
```

### Create PixDisputes

Pix disputes can be created when a fraud is detected creating a chain of transactions in order to reverse the funds to the origin.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


List<StarkInfra.PixDispute> disputes = StarkInfra.PixDispute.Create(
    new List<StarkInfra.PixDispute> {
        new StarkInfra.PixDispute(
            referenceID: "E39908427202512222248Lo3j8SXPF6z",
            method: "scam",
            operatorEmail: "test@test.com",
            operatorPhone: "+5511999999999"
        )
    }
);

foreach(StarkInfra.PixDispute dispute in disputes) {
    Console.WriteLine(dispute);
}
```

**Note**: Instead of using PixDispute objects, you can also pass each element in dictionary format

### Query PixDisputes

You can query multiple PixDisputes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixDispute> disputes = StarkInfra.PixDispute.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixDispute dispute in disputes) {
    Console.WriteLine(dispute);
}
```

### Get a PixDispute

After its creation, information on a dispute may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixDispute dispute = StarkInfra.PixDispute.Get("5155165527080960");

Console.WriteLine(dispute);
```

### Cancel a PixDispute

Cancel a specific PixDispute using its id.

```c#
using System;
using StarkInfra;


StarkInfra.PixDispute dispute = StarkInfra.PixDispute.Cancel("+5511912345678");

Console.Write(dispute);
```

### Query PixDispute logs

You can query PixDispute logs to better understand PixDispute life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.PixDispute.Log> logs = StarkInfra.PixDispute.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkInfra.PixDispute.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixDispute log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixDispute.Log log = StarkInfra.PixDispute.Log.Get("4701727546671104");

Console.WriteLine(log);
```

### Create PixPullSubscriptions

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.PixPullSubscription> subscriptions = StarkInfra.PixPullSubscription.Create(
    new List<StarkInfra.PixPullSubscription> {
        new StarkInfra.PixPullSubscription(
            bacenID: "RR2017032900000000000000003",
            externalID: "my-subscription-001",
            installmentStart: DateTime.UtcNow.AddDays(1),
            interval: "month",
            receiverName: "Edward Stark",
            receiverTaxID: "20.018.183/0001-80",
            senderAccountNumber: "876543-2",
            senderBankCode: "20018183",
            senderBranchCode: "1357-9",
            senderTaxID: "01234567890",
            type: "push",
            amount: 11234,
            description: "Monthly subscription",
            tags: new List<string> { "employees", "monthly" }
        )
    }
);

foreach (StarkInfra.PixPullSubscription subscription in subscriptions) {
    Console.WriteLine(subscription);
}
```

### Query PixPullSubscriptions

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixPullSubscription> subscriptions = StarkInfra.PixPullSubscription.Query(
    limit: 10,
    after: new DateTime(2026, 1, 1),
    before: new DateTime(2026, 4, 30),
    status: new List<string> { "active" },
    tags: new List<string> { "monthly" }
);

foreach (StarkInfra.PixPullSubscription subscription in subscriptions) {
    Console.WriteLine(subscription);
}
```

### Get a PixPullSubscription

```c#
StarkInfra.PixPullSubscription subscription = StarkInfra.PixPullSubscription.Get("5656565656565656");
Console.WriteLine(subscription);
```

### Update a PixPullSubscription

When patching `status` to `"confirmed"`, `senderCityCode` MUST be present in patchData.

```c#
using System.Collections.Generic;

StarkInfra.PixPullSubscription subscription = StarkInfra.PixPullSubscription.Update(
    id: "5656565656565656",
    patchData: new Dictionary<string, object> {
        { "status", "confirmed" },
        { "senderCityCode", "3550308" }
    }
);
Console.WriteLine(subscription);
```

### Cancel a PixPullSubscription

The `reason` is sent as a query parameter on the DELETE request.

```c#
StarkInfra.PixPullSubscription subscription = StarkInfra.PixPullSubscription.Cancel(
    id: "5656565656565656",
    reason: "accountClosed"
);
Console.WriteLine(subscription);
```

### Query PixPullSubscription logs

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixPullSubscription.Log> logs = StarkInfra.PixPullSubscription.Log.Query(
    limit: 50,
    after: new DateTime(2026, 1, 1),
    before: new DateTime(2026, 4, 30),
    subscriptionIds: new List<string> { "5656565656565656" }
);

foreach (StarkInfra.PixPullSubscription.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixPullSubscription log

```c#
StarkInfra.PixPullSubscription.Log log = StarkInfra.PixPullSubscription.Log.Get("4701727546671104");
Console.WriteLine(log);
```

### Process inbound PixPullSubscription events

```c#
StarkInfra.PixPullSubscription subscription = StarkInfra.PixPullSubscription.Parse(
    content: "{\"bacenId\": \"RR2017032900000000000000003\", ...}",
    signature: "MEUCIQC7FVhXdripx/aXg5yNLxmNoZlehpyvX3QYDXJ8o3PAZQIgVe1omKFh7Vd54ML4U1z7L+kpx+GHl+G2XLeFTLZeBJk="
);
Console.WriteLine(subscription);
```

### Create PixPullRequests

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.PixPullRequest> requests = StarkInfra.PixPullRequest.Create(
    new List<StarkInfra.PixPullRequest> {
        new StarkInfra.PixPullRequest(
            amount: 11234,
            due: DateTime.UtcNow.AddDays(2),
            endToEndID: "E00002649202201172211u34srod19le",
            receiverAccountNumber: "876543-2",
            receiverAccountType: "checking",
            receiverBankCode: "20018183",
            reconciliationID: "cycle-202604",
            subscriptionID: "5656565656565656",
            tags: new List<string> { "monthly" }
        )
    }
);

foreach (StarkInfra.PixPullRequest request in requests) {
    Console.WriteLine(request);
}
```

### Query PixPullRequests

```c#
IEnumerable<StarkInfra.PixPullRequest> requests = StarkInfra.PixPullRequest.Query(
    limit: 10,
    status: new List<string> { "created", "active" },
    subscriptionIds: new List<string> { "5656565656565656" }
);

foreach (StarkInfra.PixPullRequest request in requests) {
    Console.WriteLine(request);
}
```

### Get a PixPullRequest

```c#
StarkInfra.PixPullRequest request = StarkInfra.PixPullRequest.Get("5656565656565656");
Console.WriteLine(request);
```

### Update a PixPullRequest

When denying, `reason` is required.

```c#
StarkInfra.PixPullRequest request = StarkInfra.PixPullRequest.Update(
    id: "5656565656565656",
    patchData: new Dictionary<string, object> {
        { "status", "denied" },
        { "reason", "senderAccountClosed" }
    }
);
Console.WriteLine(request);
```

### Cancel a PixPullRequest

```c#
StarkInfra.PixPullRequest request = StarkInfra.PixPullRequest.Cancel(
    id: "5656565656565656",
    reason: "senderUserRequested"
);
Console.WriteLine(request);
```

### Query PixPullRequest logs

```c#
IEnumerable<StarkInfra.PixPullRequest.Log> logs = StarkInfra.PixPullRequest.Log.Query(
    limit: 50,
    requestIds: new List<string> { "5656565656565656" }
);

foreach (StarkInfra.PixPullRequest.Log log in logs) {
    Console.WriteLine(log);
}
```

### Get a PixPullRequest log

```c#
StarkInfra.PixPullRequest.Log log = StarkInfra.PixPullRequest.Log.Get("4701727546671104");
Console.WriteLine(log);
```

## Lending
If you want to establish a lending operation, you can use Stark Infra to
create a CCB contract. This will enable your business to lend money without
requiring a banking license, as long as you use a Credit Fund 
or Securitization company.

The required steps to initiate the operation are:
 1. Have funds in your Credit Fund or Securitization account
 2. Request the creation of an [Identity Check](#create-individualidentities)
for the credit receiver (make sure you have their documents and express authorization)
 3. (Optional) Create a [Credit Simulation](#create-creditpreviews) 
with the desired installment plan to display information for the credit receiver
 4. Create a [Credit Note](#create-creditnotes)
with the desired installment plan


### Create CreditNotes
You can create CreditNotes to generate a CCB contracts:

```c#
using System;
using StarkInfra;


List<StarkInfra.CreditNote> notes = StarkInfra.CreditNote.Create(
    new List<StarkInfra.CreditNote>() { 
        new StarkInfra.CreditNote(
            templateID: "5707012469948416",
            name: "Jamie Lannister",
            taxID: "012.345.678-90",
            nominalAmount: 500000,
            scheduled: new DateTime(2022, 01, 01),
            invoices: new List<Invoice> {
                new StarkInfra.CreditNote.Invoice(
                    amount: 510000,
                    due: new DateTime(2023, 01, 21)
                 )
            },
            payment: new StarkInfra.CreditNote.Transfer(
                bankCode: "00000000",
                branchCode: "1234",
                accountNumber: "129340-1",
                name: "Jamie Lannister",
                taxID: "012.345.678-90",
            ),
            paymentType: "transfer",
            signers: new List<StarkInfra.CreditNote.Signer>{
                new StarkInfra.CreditNote.Signer(
                    name: "Jamie Lannister",
                    contact: "jamie.lannister@gmail.com",
                    method: "link"
                )
            },
            rules: new List<StarkInfra.CreditNote.Rule>{
                new StarkInfra.CreditNote.Rule(
                    key: "invoiceCreationMode",
                    value: "scheduled"
                )
            },
            externalID: "my_external_id_10",
            streetLine1: "Av. Paulista, 200",
            streetLine2: "10 andar",
            district: "Bela Vista",
            city: "São Paulo",
            stateCode: "SP",
            zipCode: "01310-000"
        )    
    }
);

foreach(StarkInfra.CreditNote note in notes)
{
    Console.Write(note);
}
```

**Note**: Instead of using CreditNote objects, you can also pass each element in dictionary format

### Query CreditNotes

You can query multiple CreditNotes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.CreditNote> notes = StarkInfra.CreditNote.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "signed" },
    tags: new List<string>{ "iron", "suit" }
);

foreach(StarkInfra.CreditNote note in notes)
{
    Console.Write(note);
}
```

### Get a CreditNote

After its creation, information on a CreditNote may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.CreditNote note = StarkInfra.CreditNote.Get("5155165527080960");

Console.Write(note);
```

### Cancel a CreditNote

You can cancel a CreditNote if it has not been signed yet.

```c#
using System;
using StarkInfra;


StarkInfra.CreditNote note = StarkInfra.CreditNote.Cancel("5155165527080960");

Console.Write(note);
```

### Query CreditNote logs

You can query CreditNote logs to better understand CreditNote life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.CreditNote.Log> logs = StarkInfra.CreditNote.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.CreditNote.Log log in logs)
{
    Console.Write(log);
}
```

### Get a CreditNote log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.CreditNote.Log log = StarkInfra.CreditNote.Log.Get("5155165527080960");

Console.Write(log);
```

### Create CreditPreviews

You can preview a credit operation before creating them (Currently we only have CreditNote / CCB previews):

```c#
using System;
using StarkInfra;


List<StarkInfra.CreditPreview> previews = StarkInfra.CreditPreview.Create(
    new List<StarkInfra.CreditPreview>() {
        new StarkInfra.CreditPreview(
            type: "credit-note",
            credit: new StarkInfra.CreditPreview.CreditNotePreview(
                initialAmount: 2478,
                initialDue: new DateTime(2022, 10, 22),
                nominalAmount: 90583,
                nominalInterest: 3.7F,
                rebateAmount: 23,
                scheduled: new DateTime(2022, 09, 28),
                taxID: "477.954.506-44",
                type: "sac"
            )
        ),
        new StarkInfra.CreditPreview(
            type: "credit-note",
            credit: new StarkInfra.CreditPreview.CreditNotePreview(
                initialAmount: 4449,
                initialDue: new DateTime(2022, 10, 16),
                interval: "year",
                nominalAmount: 96084,
                nominalInterest: 3.1F,
                rebateAmount: 239,
                scheduled: new DateTime(2022, 10, 02),
                taxID: "81.882.684/0001-02",
                type: "price"
            )
        ),
        new StarkInfra.CreditPreview(
            type: "credit-note",
            credit: new StarkInfra.CreditPreview.CreditNotePreview(
                count: 8,
                initialDue: new DateTime(2022, 10, 18),
                nominalAmount: 6161,
                nominalInterest: 3.2F,
                scheduled: new DateTime(2022, 10, 03),
                taxID: "59.352.830/0001-20",
                type: "american"
            )
        ),
        new StarkInfra.CreditPreview(
            type: "credit-note",
            credit: new StarkInfra.CreditPreview.CreditNotePreview(
                initialDue: new DateTime(2022, 10, 13),
                nominalAmount: 86237,
                nominalInterest: 2.6F,
                scheduled: new DateTime(2022, 10, 03),
                taxID: "37.293.955/0001-94",
                type: "bullet"
            )
        ),
        new StarkInfra.CreditPreview(
            type: "credit-note",
            credit: new StarkInfra.CreditPreview.CreditNotePreview(
                invoices: new List<StarkInfra.Invoice> {
                    new StarkInfra.Invoice(
                        amount: 14500,
                        due: new DateTime(2022, 11, 19)
                    ),
                    new StarkInfra.Invoice(
                        amount: 14500,
                        due: new DateTime(2022, 12, 25)
                    )
                },
                nominalAmount: 29000,
                rebateAmount: 900,
                scheduled: new DateTime(2022, 10, 31),
                taxID: "36.084.400/0001-70",
                type: "custom"
            )
        )
    }
);

foreach (StarkInfra.CreditPreview preview in previews)
{
    Console.Write(preview);
}
```

**Note**: Instead of using CreditPreview objects, you can also pass each element in dictionary format

### Create CreditHolmes

Before you request a credit operation, you may want to check previous credit operations
the credit receiver has taken.

For that, open up a CreditHolmes investigation to receive information on all debts and credit
operations registered for that individual or company inside the Central Bank's SCR.

```c#
using System;
using StarkInfra;

List<StarkInfra.CreditHolmes> holmes = StarkInfra.CreditHolmes.Create(
    new List<StarkInfra.CreditHolmes>() {
        new StarkInfra.CreditHolmes(
            taxID: "123.456.789-00",
            competence: "2022-09"
        ),
        new StarkInfra.CreditHolmes(
            taxID: "123.456.789-00",
            competence: "2022-08"
        ),
        new StarkInfra.CreditHolmes(
            taxID: "123.456.789-00",
            competence: "2022-07"
        )
    }
);

foreach (StarkInfra.CreditHolmes sherlock in holmes)
{
    Console.Write(sherlock);
}
```

### Query CreditHolmes

You can query multiple credit holmes according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.CreditHolmes> holmes = StarkInfra.CreditHolmes.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "success" }
);

foreach(StarkInfra.CreditHolmes sherlock in holmes)
{
    Console.Write(sherlock);
}
```

### Get a CreditHolmes

After its creation, information on a credit holmes may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.CreditHolmes holmes = StarkInfra.CreditHolmes.Get("5657818854064128");

Console.Write(holmes);
```

### Query CreditHolmes logs

You can query credit holmes logs to better understand their life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.CreditHolmes.Log> logs = StarkInfra.CreditHolmes.Log.Query(
    limit: 50, 
    ids: new List<string>{ "5729405850615808" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string>{ "created" }
);

foreach (StarkInfra.CreditHolmes.Log log in logs)
{
    Console.Write(log);
}
```

### Get a CreditHolmes log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.CreditNote.Log log = StarkInfra.CreditNote.Log.Get("5155165527080960");

Console.Write(log);
```

## Identity
Several operations, especially credit ones, require that the identity
of a person or business is validated beforehand.

Identities are validated according to the following sequence:
1. The Identity resource is created for a specific Tax ID
2. Documents are attached to the Identity resource
3. The Identity resource is updated to indicate that all documents have been attached
4. The Identity is sent for validation and returns a webhook notification to reflect
the success or failure of the operation

### Create IndividualIdentities

You can create an IndividualIdentity to validate a document of a natural person

```c#
using System;
using StarkInfra;


List<StarkInfra.IndividualIdentity> identities = StarkInfra.IndividualIdentity.Create(
    new List<StarkInfra.IndividualIdentity>() { 
        new StarkInfra.IndividualIdentity(
            name: "Walter White",
            taxID: "012.345.678-90",
            tags: new List<string>{ "breaking", "bad" }
        );
    }
);

foreach(StarkInfra.IndividualIdentity identity in identities)
{
    Console.Write(identity);
}
```

**Note**: Instead of using IndividualIdentity objects, you can also pass each element in dictionary format

### Query IndividualIdentity

You can query multiple individual identities according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IndividualIdentity> identities = StarkInfra.IndividualIdentity.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "success" },
    tags: new List<string>{ "breaking", "bad" }
);

foreach(StarkInfra.IndividualIdentity identity in identities)
{
    Console.Write(identity);
}
```

### Get an IndividualIdentity

After its creation, information on an individual identity may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualIdentity identity = StarkInfra.IndividualIdentity.Get("5155165527080960");

Console.Write(identity);
```

### Update an IndividualIdentity

You can update a specific identity status to "processing" for send it to validation.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualIdentity identity = StarkInfra.IndividualIdentity.Update("5155165527080960", "processing");

Console.Write(identity);
```

**Note**: Before sending your individual identity to validation by patching its status, you must send all the required documents using the create method of the CreditDocument resource. Note that you must reference the individual identity in the create method of the CreditDocument resource by its id.

### Cancel an IndividualIdentity

You can cancel an individual identity before updating its status to processing.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualIdentity identity = StarkInfra.IndividualIdentity.Cancel("5155165527080960");

Console.Write(identity);
```

### Query IndividualIdentity logs

You can query individual identity logs to better understand individual identity life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IndividualIdentity.Log> logs = StarkInfra.IndividualIdentity.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.IndividualIdentity.Log log in logs)
{
    Console.Write(log);
}
```

### Get an IndividualIdentity log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualIdentity.Log log = StarkInfra.IndividualIdentity.Log.Get("5155165527080960");

Console.Write(log);
```

### Create IndividualDocuments

You can create an individual document to attach images of documents to a specific individual Identity.
You must reference the desired individual identity by its id.

```c#
using System;
using StarkInfra;


List<StarkInfra.IndividualDocument> documents = StarkInfra.IndividualDocument.Create(
    new List<StarkInfra.IndividualDocument>() { 
        new StarkInfra.IndividualDocument(
            type: "identity-front",
            content: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...",
            identityID: '5155165527080960',
            tags: new List<string>{ "breaking", "bad" }
        )
    }
);

documents = StarkInfra.IndividualDocument.Create(
    new List<StarkInfra.IndividualDocument>() { 
        new StarkInfra.IndividualDocument(
            type: "identity-back",
            content: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...",
            identityID: '5155165527080960',
            tags: new List<string>{ "breaking", "bad" }
        )
    }
);

documents = StarkInfra.IndividualDocument.Create(
    new List<StarkInfra.IndividualDocument>() { 
        new StarkInfra.IndividualDocument(
            type: "selfie",
            content: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...",
            identityID: '5155165527080960',
            tags: new List<string>{ "breaking", "bad" }
        )
    }
);

foreach(StarkInfra.CreditNote document in documents)
{
    Console.Write(document);
}
```

**Note**: Instead of using IndividualDocument objects, you can also pass each element in dictionary format

### Query IndividualDocuments

You can query multiple individual documents according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IndividualDocument> documents = StarkInfra.IndividualDocument.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "success" },
    tags: new List<string>{ "breaking", "bad" }
);

foreach(StarkInfra.IndividualDocument document in documents)
{
    Console.Write(document);
}
```

### Get an IndividualDocument

After its creation, information on an individual document may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualDocument document = StarkInfra.IndividualDocument.Get("5155165527080960");

Console.Write(document);
```
  
### Query IndividualDocument logs

You can query individual document logs to better understand individual document life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.IndividualDocument.Log> logs = StarkInfra.IndividualDocument.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.IndividualDocument.Log log in logs)
{
    Console.Write(log);
}
```

### Get an IndividualDocument log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.IndividualDocument.Log log = StarkInfra.IndividualDocument.Log.Get("5155165527080960");

Console.Write(log);
```

### Create BusinessIdentities

You can create a BusinessIdentity to validate a document of a legal person

```c#
using System;
using StarkInfra;


List<StarkInfra.BusinessIdentity> identities = StarkInfra.BusinessIdentity.Create(
    new List<StarkInfra.BusinessIdentity>() { 
        new StarkInfra.BusinessIdentity(
            taxID: "20.018.183/0001-80",
            tags: new List<string>{ "breaking", "bad" }
        );
    }
);

foreach(StarkInfra.BusinessIdentity identity in identities)
{
    Console.Write(identity);
}
```

**Note**: Instead of using BusinessIdentity objects, you can also pass each element in dictionary format

### Query BusinessIdentity

You can query multiple business identities according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.BusinessIdentity> identities = StarkInfra.BusinessIdentity.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "success" },
    tags: new List<string>{ "breaking", "bad" }
);

foreach(StarkInfra.BusinessIdentity identity in identities)
{
    Console.Write(identity);
}
```

### Get a BusinessIdentity

After its creation, information on a business identity may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessIdentity identity = StarkInfra.BusinessIdentity.Get("5155165527080960");

Console.Write(identity);
```

### Update a BusinessIdentity

You can update a specific identity status to "processing" for send it to validation.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessIdentity identity = StarkInfra.BusinessIdentity.Update("5155165527080960", status: "processing");

Console.Write(identity);
```

**Note**: Before sending your business identity to validation by patching its status, you must send all the required attachments using the create method of the BusinessAttachment resource. Note that you must reference the business identity in the create method of the BusinessAttachment resource by its id.

### Cancel a BusinessIdentity

You can cancel a business identity before updating its status to processing.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessIdentity identity = StarkInfra.BusinessIdentity.Cancel("5155165527080960");

Console.Write(identity);
```

### Query BusinessIdentity logs

You can query business identity logs to better understand business identity life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.BusinessIdentity.Log> logs = StarkInfra.BusinessIdentity.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.BusinessIdentity.Log log in logs)
{
    Console.Write(log);
}
```

### Get a BusinessIdentity log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessIdentity.Log log = StarkInfra.BusinessIdentity.Log.Get("5155165527080960");

Console.Write(log);
```

### Create BusinessAttachments

You can create a business attachment to attach documents to a specific business Identity.
You must reference the desired business identity by its id.

```c#
using System;
using StarkInfra;


List<StarkInfra.BusinessAttachment> attachments = StarkInfra.BusinessAttachment.Create(
    new List<StarkInfra.BusinessAttachment>() { 
        new StarkInfra.BusinessAttachment(
            name: "articles-of-incorporation.png",
            content: "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8/5+hHgAHggJ/PchI7wAAAABJRU5ErkJggg==",
            contentType: "image/png",
            businessIdentityID: "5155165527080960",
            tags: new List<string>{ "breaking", "bad" }
        )
    }
);

foreach(StarkInfra.BusinessAttachment attachment in attachments)
{
    Console.Write(attachment);
}
```

**Note**: Instead of using BusinessAttachment objects, you can also pass each element in dictionary format

### Query BusinessAttachments

You can query multiple business attachments according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.BusinessAttachment> attachments = StarkInfra.BusinessAttachment.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "approved" },
    tags: new List<string>{ "breaking", "bad" }
);

foreach(StarkInfra.BusinessAttachment attachment in attachments)
{
    Console.Write(attachment);
}
```

### Get a BusinessAttachment

After its creation, information on a business attachment may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessAttachment attachment = StarkInfra.BusinessAttachment.Get("5155165527080960");

Console.Write(attachment);
```

### Cancel a BusinessAttachment

You can cancel a business attachment by its id.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessAttachment attachment = StarkInfra.BusinessAttachment.Cancel("5155165527080960");

Console.Write(attachment);
```

### Query BusinessAttachment logs

You can query business attachment logs to better understand business attachment life cycles. 

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.BusinessAttachment.Log> logs = StarkInfra.BusinessAttachment.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01)
);

foreach (StarkInfra.BusinessAttachment.Log log in logs)
{
    Console.Write(log);
}
```

### Get a BusinessAttachment log

You can also get a specific log by its id.

```c#
using System;
using StarkInfra;


StarkInfra.BusinessAttachment.Log log = StarkInfra.BusinessAttachment.Log.Get("5155165527080960");

Console.Write(log);
```

## Webhook

### Create a Webhook subscription

To create a webhook subscription and be notified whenever an event occurs, run:

```c#
using System;
using StarkInfra;


StarkInfra.Webhook webhook = StarkInfra.Webhook.Create(
    url: "https://webhook.site/6cd3c98d-60ca-4f4a-97c6-a7d0365b15B7",
    subscription: new List<string> {
        "credit-note",
        "issuing-card",
        "issuing-invoice",
        "issuing-purchase",
        "pix-request.in",
        "pix-request.out",
        "pix-reversal.in",
        "pix-reversal.out",
        "pix-claim",
        "pix-key",
        "pix-chargeback",
        "pix-infraction",
        "business-identity"
    }
);

Console.Write(webhook);
```

### Query Webhooks

To search for registered webhooks, run:

```c#
using System;
using System.Collections.Generic;
using StarkInfra;


IEnumerable<StarkInfra.Webhook> webhooks = StarkInfra.Webhook.Query();

foreach (StarkInfra.Webhook webhook in webhooks)
{
    Console.Write(webhook);
}
```

### Get a Webhook

After its creation, information on a webhook may be retrieved by its id.

```c#
using System;
using StarkInfra;


StarkInfra.Webhook webhook = StarkInfra.Webhook.Get("1082736198236817");

Console.Write(webhook);
```

### Delete a Webhook

You can also delete a specific webhook by its id.

```c#
using System;
using StarkInfra;


StarkInfra.Webhook webhook = StarkInfra.Webhook.Delete("1082736198236817");

Console.Write(webhook);
```

### Process Webhook events

It's easy to process events delivered to your Webhook endpoint.
Remember to pass the signature header so the SDK can make sure it was StarkInfra that sent you the event.

```c#
using System;
using StarkInfra;


Response response = listen();  // this is the method you made to get the events posted to your webhook endpoint

StarkInfra.Event parsedEvent = StarkInfra.Event.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
);

if (parsedEvent.Subscription.Contains("pix-key") {
    StarkInfra.PixKey.Log log = parsedEvent.Log as StarkInfra.PixKey.Log;
    Console.WriteLine(log.PixKey);
}
if (parsedEvent.Subscription.Contains("pix-claim") {
    StarkInfra.PixClaim.Log log = parsedEvent.Log as StarkInfra.PixClaim.Log;
    Console.WriteLine(log.PixClaim);
}
if (parsedEvent.Subscription.Contains("pix-infraction") {
    StarkInfra.PixInfraction.Log log = parsedEvent.Log as StarkInfra.PixInfraction.Log;
    Console.WriteLine(log.PixInfraction);
}
if (parsedEvent.Subscription.Contains("pix-chargeback") {
    StarkInfra.PixChargeback.Log log = parsedEvent.Log as StarkInfra.PixChargeback.Log;
    Console.WriteLine(log.PixChargeback);
}
if (parsedEvent.Subscription.Contains("pix-request") {
    StarkInfra.PixRequest.Log log = parsedEvent.Log as StarkInfra.PixRequest.Log;
    Console.WriteLine(log.PixRequest);
}
if (parsedEvent.Subscription.Contains("pix-reversal") {
    StarkInfra.PixReversal.Log log = parsedEvent.Log as StarkInfra.PixReversal.Log;
    Console.WriteLine(log.PixReversal);
}
if (parsedEvent.Subscription.Contains("credit-note") {
    StarkInfra.CreditNote.Log log = parsedEvent.Log as StarkInfra.CreditNote.Log;
    Console.WriteLine(log.CreditNote);
}
if (parsedEvent.Subscription.Contains("issuing-card") {
    StarkInfra.IssuingCard.Log log = parsedEvent.Log as StarkInfra.IssuingCard.Log;
    Console.WriteLine(log.IssuingCard);
}
if (parsedEvent.Subscription.Contains("issuing-invoice") {
    StarkInfra.IssuingInvoice.Log log = parsedEvent.Log as StarkInfra.IssuingInvoice.Log;
    Console.WriteLine(log.IssuingInvoice);
}
if (parsedEvent.Subscription.Contains("issuing-purchase") {
    StarkInfra.IssuingPurchase.Log log = parsedEvent.Log as StarkInfra.IssuingPurchase.Log;
    Console.WriteLine(log.IssuingPurchase);
}
if (parsedEvent.Subscription.Contains("business-identity") {
    StarkInfra.BusinessIdentity.Log log = parsedEvent.Log as StarkInfra.BusinessIdentity.Log;
    Console.WriteLine(log.Identity);
}
```

### Query Webhook events

To search for webhook events, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.Event> events = StarkInfra.Event.Query(
    after: DateTime.Today,
    isDelivered: false
);

foreach(StarkInfra.Event event in events) {
    Console.WriteLine(event);
}
```

### Get a webhook event

You can get a specific webhook event by its id.

```c#
using System;
using StarkInfra;


StarkInfra.Event event = StarkInfra.Event.Get("1082736198236817");

Console.WriteLine(event);
```

### Delete a webhook event

You can also delete a specific webhook event by its id.

```c#
using System;
using StarkInfra;


StarkInfra.Event event = StarkInfra.Event.Delete("1082736198236817");

Console.WriteLine(event);
```

### Set webhook events as delivered

This can be used in case you"ve lost events.
With this function, you can manually set events retrieved from the API as
"delivered" to help future event queries with `isDelivered: false`.

```c#
using System;
using StarkInfra;


StarkInfra.Event event = StarkInfra.Event.Update("1298371982371921", isDelivered: true);

Console.WriteLine(event);
```

### Query failed webhook event delivery attempts information

You can also get information on failed webhook event delivery attempts.

```c#
using System;
using StarkInfra;


List<StarkInfra.Event.Attempt> attempts = StarkInfra.Event.Attempt.Query(after: "2020-03-20").ToList();

foreach(StarkInfra.Event.Attempt attempt in attempts) {
    Console.WriteLine(attempt);
}
```

### Get a failed webhook event delivery attempt information

To retrieve information on a single attempt, use the following function:

```c#
using System;
using StarkInfra;


StarkInfra.Event.Attempt attempt = StarkInfra.Event.Attempt.Get("1616161616161616");

Console.WriteLine(attempt);
```

# Request

This resource allows you to send HTTP requests to StarkInfra routes.

## GET

You can perform a GET request to any StarkInfra route.

It's possible to get a single resource using its id in the path.

```c#
using StarkInfra;

string exampleId = "5155165527080960"
JObejct request = Request.Get(
    path="/pix-request/" + exampleId
).Json();

Console.WriteLine(request.ToString());
```

You can also get the specific resource log,

```c#
using StarkInfra;

string exampleId = "5155165527080960";
JObejct request = Request.Get(
    path="/pix-request/log/" + exampleId
).Json();

Console.WriteLine(request.ToString());
```

This same method will be used to list all created items for the requested resource.

```c#
using StarkInfra;

string after = "2024-01-01";
string before = "2024-02-01";
string cursor = "";

JObject request = Request.Get(
        path="/pix-request/",
        query={
            "after": after,
            "before": before,
            "cursor": cursor
        }
    ).Json();

Console.WriteLine(request.ToString());
```

To list logs, you will use the same logic as for getting a single log.

```c#
using StarkInfra;

string after = "2024-01-01";
string before = "2024-02-01";
string cursor = "";

JObject request = Request.Get(
        path="/pix-request/log",
        query={
            "after": after,
            "before": before,
            "cursor": cursor
        }
    ).Json();

Console.WriteLine(request.ToString());
```


You can get a resource file using this method.

```c#
using StarkInfra;

string exampleId = "5155165527080960";
[]byte pdf = Request.Get(
    path="/pix-request/" + exampleId + "/pdf",
).ByteContent;

System.IO.File.WriteAllBytes("request.pdf", pdf);
```

## POST

You can perform a POST request to any StarkInfra route.

This will create an object for each item sent in your request

**Note**: It's not possible to create multiple resources simultaneously. You need to send separate requests if you want to create multiple resources, such as requests and boletos.

```c#
using StarkInfra;

Dictionary<string, object> data = new Dictionary<string, object>() {
        {
            "holders", new List<Dictionary<string, object>>() { new Dictionary<string, object>()
                {
                    { "name", "Jaime Lannister " + Guid.NewGuid().ToString() },
                    { "externalId", Guid.NewGuid().ToString() },
                    { "taxId", "20.018.183/0001-80" }
                },

            }
        }
    };

JObject request = Request.Post(
    path="/issuing-holder",
    body=data,
).Json();
Console.WriteLine(request.ToString())
```

## PATCH

You can perform a PATCH request to any StarkInfra route.

It's possible to update a single item of a StarkInfra resource.
```c#
using StarkInfra;

string exampleId = "5155165527080960"

Dictionary<string, object> data = new Dictionary<string, object>() { { "tags", new List<string> { Guid.NewGuid().ToString() } } };

JObject request = Request.Patch(
    path="/issuing-holder/" + exampleId,
    body=data,
).Json();
Console.WriteLine(request.ToString());
```

## DELETE

You can perform a DELETE request to any StarkInfra route.

It's possible to delete a single item of a StarkInfra resource.
```c#
using StarkInfra;

string exampleId = "5155165527080960"
JObject request = Request.Delete(
    path="/issuing-holder/" + exampleId,
).Json();
Console.WriteLine(request.ToString());        
```

# Handling errors

The SDK may raise one of four types of errors: __InputErrors__, __InternalServerError__, __UnknownError__, __InvalidSignatureError__

__InputErrors__ will be raised whenever the API detects an error in your request (status code 400).
If you catch such an error, you can get its elements to verify each of the
individual errors that were detected in your request by the API.
For example:

```c#
using System;
using System.Collections.Generic;
using System.Error;
using StarkInfra;


try {
    List<StarkInfra.PixReversal> reversal = StarkInfra.PixReversal.Create(
        new List<StarkInfra.PixReversal> {
            new StarkInfra.PixReversal(
                amount: 100,
                endToEndID: "E00000000202201060100rzsJzG9PzMg",
                externalID: "1723843582395893",
                reason: "bankError"
            )
        }
);
} catch (InputErrors e) {
    foreach (ErrorElement error in e.Errors) {
        Console.WriteLine(error.Code);
        Console.WriteLine(error.Message);
    }
}
```

__InternalServerError__ will be raised if the API runs into an internal error.
If you ever stumble upon this one, rest assured that the development team
is already rushing in to fix the mistake and get you back up to speed.

__UnknownError__ will be raised if a request encounters an error that is
neither __InputErrors__ nor an __InternalServerError__, such as connectivity problems.

__InvalidSignatureError__ will be raised specifically by StarkInfra.Event.Parse()
when the provided content and signature do not check out with the Stark Infra public
key.

# Help and Feedback

If you have any questions about our SDK, just send us an email.
We will respond you quickly, pinky promise. We are here to help you integrate with us ASAP.
We also love feedback, so don't be shy about sharing your thoughts with us.

Email: help@starkbank.com
