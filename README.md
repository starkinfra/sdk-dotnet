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
        - [BINs](#query-issuingbins): View available sub-issuer BINs (a.k.a. card number ranges)
        - [Holders](#create-issuingholders): Manage card holders
        - [Cards](#create-issuingcards): Create virtual and/or physical cards
        - [Purchases](#process-purchase-authorizations): Authorize and view your past purchases
        - [Invoices](#create-issuinginvoices): Add money to your issuing balance
        - [Withdrawals](#create-issuingwithdrawals): Send money back to your Workspace from your issuing balance
        - [Balance](#get-your-issuingbalance): View your issuing balance
        - [Transactions](#query-issuingtransactions): View the transactions that have affected your issuing balance
    - [Pix](#pix)
        - [PixRequests](#create-pixrequests): Create Pix transactions
        - [PixReversals](#create-pixreversals): Reverse Pix transactions
        - [PixBalance](#get-your-pixbalance): View your account balance
        - [PixStatement](#create-a-pixstatement): Request your account statement
        - [PixKey](#create-a-pixkey): Create a PixKey
        - [PixClaim](#create-a-pixclaim): Claim a PixKey
        - [PixDirector](#create-a-pixdirector): Create a Pix Director
        - [PixInfraction](#create-pixinfractions): Create Pix Infraction reports
        - [PixChargeback](#create-pixchargebacks): Create Pix Chargeback requests
        - [PixDomain](#query-pixdomains): View registered SPI participants certificates
    - [Credit Note](#credit-note)
        - [CreditNote](#create-creditnotes): Create Credit Notes
    - [Webhook](#webhook):
        - [Webhook](#create-a-webhook-subscription): Configure your webhook endpoints and subscriptions
    - [Webhook Events](#webhook-events):
        - [WebhookEvents](#process-webhook-events): Manage webhook events
        - [WebhookEventAttempts](#query-failed-webhook-event-delivery-attempts-information): Query failed webhook event deliveries
- [Handling errors](#handling-errors)
- [Help and Feedback](#help-and-feedback)

## Supported .NET Versions

This library supports the following Python versions:

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

StarkInfra`s .NET SDK is available on NuGet as starkinfra 0.1.0

1.1 To install the Package Manager:

```sh
Install-Package starkinfra -Version 0.1.0
```

1.2 To install the .NET CLI:

```sh
dotnet add package starkinfra --version 0.1.0
```

1.3 To install by PackageReference:

```sh
<PackageReference Include="starkinfra" Version="0.1.0" />
```

1.4 To install with Paket CLI:

```sh
paket add starkinfra --version 0.1.0
```

## 2. Create your Private and Public Keys

We use ECDSA. That means you need to generate a secp256k1 private
key to sign your requests to our API, and register your public key
with us, so we can validate those requests.

You can use one of the following methods:

2.1. Check out the options in our [tutorial](https://starkbank.com/faq/how-to-create-ecdsa-keys). 

2.2. Use our SDK:

```c#
(string privateKey, string publicKey) = StarkInfra.Key.Create();

# or, to also save .pem files in a specific path
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

3.1.1. Log into [StarkInfra Sandbox](https://web.sandbox.starkbank.com)

3.1.2. Go to Menu > Integrations

3.1.3. Click on the "New Project" button

3.1.4. Create a Project: Give it a name and upload the public key you created in section 2

3.1.5. After creating the Project, get its Project ID

3.1.6. Use the Project ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
string privateKeyContent = "-----BEGIN EC PARAMETERS-----\nBgUrgQQACg==\n-----END EC PARAMETERS-----\n-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIMCwW74H6egQkTiz87WDvLNm7fK/cA+ctA2vg/bbHx3woAcGBSuBBAAK\noUQDQgAE0iaeEHEgr3oTbCfh8U2L+r7zoaeOX964xaAnND5jATGpD/tHec6Oe9U1\nIF16ZoTVt1FzZ8WkYQ3XomRD4HS13A==\n-----END EC PRIVATE KEY-----";

StarkInfra.Project project = new StarkInfra.Project(
    environment: "sandbox",
    id: "5656565656565656",
    privateKey: privateKeyContent
);
```

3.2. To create Organization credentials in Sandbox:

3.2.1. Log into [Starkinfra Sandbox](https://web.sandbox.starkbank.com)

3.2.2. Go to Menu > Integrations

3.2.3. Click on the "Organization public key" button

3.2.4. Upload the public key you created in section 2 (only a legal representative of the organization can upload the public key)

3.2.5. Click on your profile picture and then on the "Organization" menu to get the Organization ID

3.2.6. Use the Organization ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
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
StarkInfra.PixBalance balance = StarkInfra.PixBalance.Get(user: project); //or organization
```

4.2 Set it as a default user in the SDK:

```c#
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

Your initial balance is zero. For many operations in Stark Infra, you'll need funds in your account, which can be added to your balance by creating a StarkBank.Invoice or a StarkBank.Boleto.

In the Sandbox environment, most of the created Invoices and Boletos will be automatically paid, so there's nothing else you need to do to add funds to your account. Just create a few Invoices and wait around a bit.

In Production, you (or one of your clients) will need to actually pay this Invoice or Boleto for the value to be credited to your account.

# Usage

Here are a few examples on how to use the SDK. If you have any doubts, check out
the function or class docstring to get more info or go straight to our [API docs].

## Issuing

### Query IssuingBins

To take a look at the sub-issuer BINs available to you, just run the following:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingBin> bins = StarkInfra.IssuingBin.Query()

foreach (StarkInfra.IssuingBin bin in bins) {
    Console.Write(bin);
}
```

This will tell which card products and card number prefixes you have at your disposal.

### Create IssuingHolders

You can create card holders to which your cards will be bound.
They support spending rules that will apply to all underlying cards.

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.IssuingHolder> holders = StarkInfra.IssuingHolder.Create(
    new List<StarkInfra.IssuingHolder> {
        new StarkInfra.IssuingHolder(
            name: "Jamie Lanister",
            externalId : "external_id_12345",
            taxId : "012.345.678-90",
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

**Note**: Instead of using IssuingHolder and IssuingRule objects, you can also pass each element in dictionary format

### Query IssuingHolders

You can query multiple holders according to filters.

```c#
using System;
using System.Collections.Generic;

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

StarkInfra.IssuingHolder holder = StarkInfra.IssuingHolder.Cancel("5353197895942144");

Console.Write(holder);
```

### Get an IssuingHolder

To get a single Issuing Holder by its id, run:

```c#
using System;

StarkInfra.IssuingHolder holder = StarkInfra.IssuingHolder.Get("5353197895942144");

Console.Write(holder);
```

### Query IssuingHolder logs

You can query holder logs to better understand holder life cycles.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingHolder.Log> logs = StarkInfra.IssuingHolder.Log.Query(limit: 10);

foreach (StarkInfra.IssuingHolder.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingHolder log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.IssuingHolder.Log log = StarkInfra.IssuingHolder.Log.Get("6299741604282368");

Console.Write(log);
```

### Create IssuingCards

You can issue cards with specific spending rules.

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.IssuingCard> cards = StarkInfra.IssuingCard.Create(
    new List<StarkInfra.IssuingCard> {
        new StarkInfra.IssuingCard(
            holderName : "Developers",
            holderTaxId : "012.345.678-90",
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

foreach(StarkInfra.IssuingCard card in cards){
    Console.Write(card);
}
```

### Query IssuingCards

You can get a list of created cards given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingCard> cards = StarkInfra.IssuingCard.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach(StarkInfra.IssuingCard card in cards){
    Console.Write(card);
}
```

### Get an IssuingCard

After its creation, information on a card may be retrieved by its id.

```c#
using System;

StarkInfra.IssuingCard card = StarkInfra.IssuingCard.Get("5353197895942144");

Console.Write(card);
```

### Update an IssuingCard

You can update a specific card by its id.

```c#
using System;
using System.Collections.Generic;

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

StarkInfra.IssuingCard card = StarkInfra.IssuingCard.Cancel("5353197895942144");

Console.Write(card);
```

### Query IssuingCard logs

You can query card logs to better understand card life cycles.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingCard.Log> logs = StarkInfra.IssuingCard.Log.Query(limit: 10);

foreach (StarkInfra.IssuingCard.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingIssuingCardHolder log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.IssuingCard.Log log = StarkInfra.IssuingCard.Log.Get("6299741604282368");

Console.Write(log);
```


### Process Purchase authorizations

It's easy to process purchase authorizations delivered to your endpoint.
If you do not approve or decline the authorization within 2 seconds, the authorization will be denied.

```c#
using System;
using System.Collections.Generic;

Response response = listen();  // this is the method you made to get the events posted to your webhook endpoint

StarkInfra.IssuingAuthorization authorization = StarkInfra.IssuingAuthorization.ParseContent(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
);

sendResponse(  // you should also implement this method
    StarkInfra.IssuingAuthorization.Response(  // this optional method just helps you build the response JSON
        status: "accepted",
        amount: authorization.amount,
        tags= new List<string> { "my-purchase-id/123"}
    )
)

// or 

sendResponse(
    StarkInfra.IssuingAuthorization.Response(
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

IEnumerable<StarkInfra.IssuingPurchase> purchases = StarkInfra.IssuingPurchase.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingPurchase purchase in purchases){
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

Logs are pretty important to understand the life cycle of a purchase.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingPurchase.Log> logs = StarkInfra.IssuingPurchase.Log.Query(limit: 10);

foreach (StarkInfra.IssuingPurchase.Log log in logs){
    Console.Write(log);
}
```

### Get an IssuingPurchase log

You can get a single log by its id.

```c#
using System;

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

IEnumerable<StarkInfra.IssuingInvoice> invoices = StarkInfra.IssuingInvoice.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkInfra.IssuingInvoice invoice in invoices){
    Console.Write(invoice);
}
```

### Get an IssuingInvoice

After its creation, information on an invoice may be retrieved by its id. 
Its status indicates whether it's been paid.

```c#
using System;

StarkInfra.IssuingInvoice invoice = StarkInfra.IssuingInvoice.Get("5709933853016064");

Console.Write(invoice);
```

### Query IssuingInvoice logs

Logs are pretty important to understand the life cycle of an invoice.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.IssuingInvoice.Log> logs = StarkInfra.IssuingInvoice.Log.Query(limit: 10);

foreach (StarkInfra.IssuingInvoice.Log log in logs){
    Console.Write(log);
}
```

### Create IssuingWithdrawals

You can create withdrawals to send cash back from your Issuing balance to your Banking balance
by using the Withdrawal resource.

```c#
using System;

StarkInfra.IssuingWithdrawal withdrawal = StarkInfra.IssuingWithdrawal.Create(
    new StarkInfra.IssuingWithdrawal(
        amount: 10000,
        externalId: "3257",
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

StarkInfra.IssuingWithdrawal withdrawal = StarkInfra.IssuingWithdrawal.Get("5440727945314304");

Console.Write(withdrawal);
```

### Query IssuingWithdrawals

You can get a list of created invoices given some filters.

```c#
using System;
using System.Collections.Generic;

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

You can get a specific transaction by its id:

```c#
using System;

StarkInfra.IssuingTransaction issuingTransaction = StarkInfra.IssuingTransaction.Get("6539944898068480");

Console.Write(issuingTransaction);
```
## Pix

### Create PixRequests
You can create a PixRequest to charge a user:

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.PixRequest> requests = StarkInfra.PixRequest.Create(
    new List<StarkInfra.PixRequest> {
        new StarkIfnra.PixRequest(
            amount: 100,  // (R$ 1.00)
            external_id: "141234121",  // so we can block anything you send twice by mistake
            sender_branch_code: "0000",
            sender_account_number: "00000-0",
            sender_account_type: "checking",
            sender_name: "Tyrion Lannister",
            sender_tax_id: "012.345.678-90",
            receiver_bank_code: "00000001",
            receiver_branch_code: "0001",
            receiver_account_number: "00000-1",
            receiver_account_type: "checking",
            receiver_name: "Jamie Lannister",
            receiver_tax_id: "45.987.245/0001-92",
            end_to_end_id: EndToEndId.Create(bankCode: 20018183),
            description: "For saving my life"
        ),
        new StarkIfnra.PixRequest(
            amount: 200,  // (R$ 2.00)
            external_id: "2135613462",  // so we can block anything you send twice by mistake
            sender_account_number: "00000-0",
            sender_branch_code: "0000",
            sender_account_type: "checking",
            sender_name: "Arya Stark",
            sender_tax_id: "012.345.678-90",
            receiver_bank_code: "00000001",
            receiver_account_number: "00000-1",
            receiver_branch_code: "0001",
            receiver_account_type: "checking",
            receiver_name: "John Snow",
            receiver_tax_id: "012.345.678-90",
            end_to_end_id: EndToEndId.Create(bankCode: 20018183),
            tags: ["Needle", "sword"]
        )
    }
);

foreach(StarkInfra.PixRequest request in requests) {
    Console.WriteLine(request);
```

**Note**: Instead of using PixRequest objects, you can also pass each transaction element in dictionary format

### Query PixRequests

You can query multiple Pix requests according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixRequest> requests = StarkInfra.PixRequest.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixRequest request in requests) {
    Console.WriteLine(request);
}
```

### Get a PixRequest

After its creation, information on a Pix request may be retrieved by its id. Its status indicates whether it has been paid.

```c#
using System;

Starkinfra.PixRequest request = StarkInfra.PixRequest.Get("5155165527080960");

Console.WriteLine(request);
```

### Process PixRequest authorization requests

It's easy to process authorization requests that arrived in your handler. Remember to pass the
signature header so the SDK can make sure it's StarkInfra that sent you the event.

```c#
using System;

Response response = listen(); // this is your handler to listen for authorization requests

StarkInfra.PixRequest request= StarkInfra.PixRequest.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
)

Console.WriteLine(request);
```

### Query PixRequest logs

You can query Pix request logs to better understand PixRequest life cycles.

```c#
using System;
using System.Collections.Generic;

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

You can reverse a Pix request by whole or by a fraction of its amount using a PixReversal.

```c#
```c#
using System;
using System.Collections.Generic;

List<StarkInfra.PixReversal> reversal = StarkInfra.PixReversal.Create(
    new List<StarkInfra.PixReversal> {
        new StarkIfnra.PixReversal(
            amount: 100,
            end_to_end_id: "E20018183202201060100rzsJzG9PzMg",
            external_id: "17238435823958934",
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

IEnumerable<StarkInfra.PixReversal> reversals = StarkInfra.PixReversal.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkInfra.PixReversal reversal in reversals) {
    Console.WriteLine(reversal);
}
```

### Get a PixReversal

After its creation, information on a Pix reversal may be retrieved by its id. Its status indicates whether it has been paid.

```c#
using System;

Starkinfra.PixReversal reversal = StarkInfra.PixReversal.Get("5155165527080960");

Console.WriteLine(reversal);
```

### Process PixReversal authorization requests

It's easy to process authorization requests that arrived in your handler. Remember to pass the
signature header so the SDK can make sure it's StarkInfra that sent you the event.

```c#
using System;

Response response = listen(); // this is your handler to listen for authorization reversals

StarkInfra.PixReversal reversal= StarkInfra.PixReversal.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
)

Console.WriteLine(reversal);
```

### Query PixReversal logs

You can query Pix reversal logs to better understand PixReversal life cycles. 

```c#
using System;
using System.Collections.Generic;

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

StarkInfra.PixReversal.Log log = StarkInfra.PixReversal.Log.Get("4701727546671104");

Console.WriteLine(log);
```

### Get your PixBalance 

To know how much money you have in your workspace, run:

```c#
using System;

Starkinfra.PixBalance balance = StarkInfra.PixBalance.Get();

Console.WriteLine(reversal);
```

### Create a PixStatement

Statements are only available for direct participants. To create a statement of all the transactions that happened on your workspace during a specific day, run:

```c#
using System;
using System.Collections.Generic;

List<StarkInfra.PixStatement> statement = StarkInfra.PixStatement.Create(
    new List<StarkInfra.PixStatement> {
        new StarkIfnra.PixStatement(
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

You can query multiple Pix statements according to filters. 

```c#
using System;
using System.Collections.Generic;

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

Statements are only available for direct participants. To get a Pix statement by its id:

```c#
using System;

Starkinfra.PixStatement statement = StarkInfra.PixStatement.Get("5155165527080960");

Console.WriteLine(statement);
```

### Get a PixStatement .csv file

To get a .csv file of a Pix statement using its id, run:

```c#
using System;

byte[] csv = StarkInfra.PixStatement.Csv("5155165527080960");

System.IO.File.WriteAllBytes("statement.zip", csv);
```

### Create a PixKey

You can create a Pix key to link a bank account information to a key id:

```c#
using System;

StarkInfra.PixKey pixKey = StarkInfra.PixKey.Create(
    new StarkInfra.PixKey(
        accountCreated: new DateTime(2022, 02, 01),
        accountNumber: "00000",
        accountType: "savings",
        branchCode: "0000",
        name: "Jamie Lannister",
        taxId: "012.345.678-90",
        id: "+5511989898989"
    )
);

Console.Write(pixKey);
```

### Query PixKeys

You can query multiple Pix keys you own according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixKey> keys = StarkInfra.PixKey.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 01, 12),
    status: new List<string> { "created" },
    ids: new List<string> { "+5511989898989" },
    type: "phone"
);

foreach (StarkInfra.PixKey key in keys){
    Console.Write(key);
}
```

### Get a PixKey

Information on a Pix key may be retrieved by its id and the tax ID of the consulting agent.
An endToEndId must be informed so you can link any resulting purchases to this query,
avoiding sweep blocks by the Central Bank.

```c#
using System;
using StarkInfra.Utils;

StarkInfra.PixKey key = StarkInfra.PixKey.Get(
    id: "5155165527080960",
    payerId: "012.345.678-90",
    parameters: new Dictionary<string, object> {
        { "endToEndId", EndToEndId.Create("20018183") }
    }
);

Console.Write(key);
```

### Patch a PixKey

Update the account information linked to a Pix key.

```c#
using System;

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

StarkInfra.PixKey key = StarkInfra.PixKey.Cancel("+5511912345678");

Console.Write(key);
```

### Query PixKey logs

You can query PixKey logs to better understand a Pix key life cycle. 

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixKey.Log> logs = StarkInfra.PixKey.Log.Query(
    limit: 50,
    ids: new List<string> { "6126693430525952" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 05, 20),
    types: new List<string> { "created" },
    keyIds: new List<string> { "+5511912345678" }
);

foreach(StarkInfra.PixKey.Log log in logs){
    Console.Write(log);
}
```

### Get a PixKey log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixKey.Log log = StarkInfra.PixKey.Log.Get("5566693430525952");

Console.Write(log);
```

### Create a PixClaim

You can create a Pix claim to request the transfer of a Pix key from another bank to one of your accounts:

```c#
using System;

StarkInfra.PixClaim claim = StarkInfra.PixClaim.Create(
    new StarkInfra.PixClaim(
        accountCreated: new DateTime(2022, 02, 01),
        accountNumber: "5692908409716736",
        accountType: "checking",
        branchCode: "0001",
        name: "testKey",
        taxId: "012.345.678-90",
        keyId: "+5511989298469"
    )
);

Console.Write(claim);
```

### Query PixClaims

You can query multiple Pix claims according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixClaim> claims = StarkInfra.PixClaim.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 09, 12),
    status: new List<string> { "delivered" },
    ids: new List<string> { "6481646396112896" },
    type: "ownership",
    agent: "claimer",
    keyType: "phone",
    keyId: "+5511989298469"
);

foreach(StarkInfra.PixClaim claim in claims){
    Console.Write(claim);
}
```

### Get a PixClaim

After its creation, information on a Pix claim may be retrieved by its id.

```c#
using System;

StarkInfra.PixClaim claim = StarkInfra.PixClaim.Get("6481646396112896");

Console.Write(claim);
```

### Update a PixClaim

A Pix claim can be confirmed or canceled by patching its status.
A received Pix claim must be confirmed by the donor to be completed.
Ownership Pix claims can only be canceled by the donor if the reason is "fraud".
A sent Pix claim can also be canceled.

```c#
using System;

Dictionary<string, object> patchData = new Dictionary<string, object> {
    { "status", "canceled" }
};

StarkInfra.PixClaim claim = StarkInfra.PixClaim.Update(id: "4508444895739904", patchData: patchData);

Console.Write(claim);
```

### Query PixClaim logs

You can query Pix claim logs to better understand Pix claim life cycles.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixClaim.Log> logs = StarkInfra.PixClaim.Log.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "canceled" },
    claimIds: new List<string> { "4508444895739904" }
);

foreach(StarkInfra.PixClaim.Log log in logs){
    Console.Write(log);
}
```

### Get a PixClaim log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixClaim.Log log = StarkInfra.PixClaim.Log.Get("4598641893310464");

Console.Write(log);
```

### Create a PixDirector

To register the Pix director contact information at the Central Bank, run the following:

```c#
using System;

StarkInfra.PixDirector director = StarkInfra.PixDirector.Create(
    new StarkInfra.PixDirector(
        name: "Edward Stark",
        taxId: "012.345.678-90",
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

Pix infractions are used to report transactions that raise fraud suspicion, to request a refund or to 
reverse a refund. Pix infractions can be created by either participant of a transaction.

```c#
using System;

StarkInfra.PixInfraction infractions = StarkInfra.PixInfraction.Create(
    new List<StarkInfra.PixInfraction>{
        new StarkInfra.PixInfraction(
            referenceId: "E20018183202204951450u34sDGd19lz",
            type: "fraud"
        )
    }
);

foreach(StarkInfra.PixInfraction infraction in infractions){
    Console.Write(infraction);
}
```

### Query PixInfractions

You can query multiple Pix infractions according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixInfraction> infractions = StarkInfra.PixInfraction.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: "created",
    ids: new List<string> { "5724541800153088" }
);

foreach(StarkInfra.PixInfraction infraction in infractions){
    Console.Write(infraction);
}
```

### Get a PixInfraction

After its creation, information on a Pix infraction may be retrieved by its id.

```c#
using System;

StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Get("5724541800153088");

Console.Write(infraction);
```

### Patch a PixInfraction

A received Pix infraction can be confirmed or declined by patching its status.
After a Pix infraction is patched, its status changes to closed.

```c#
using System;

StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Update(id: "5586201146818560", result: "agreed");

Console.Write(infraction);
```

### Cancel a PixInfraction

Cancel a specific Pix infraction using its id.

```c#
using System;

StarkInfra.PixInfraction infraction = StarkInfra.PixInfraction.Cancel("5586201146818560");

Console.Write(infraction);
```
### Query PixInfraction logs

You can query Pix infraction logs to better understand their life cycles. 

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixInfraction.Log> logs = StarkInfra.PixInfraction.Log.Query(
    limit: 50,
    ids: new List<string> { "6307030096674816" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "closed" },
    infractionIds: new List<string> { "5586201146818560" }
);

foreach(StarkInfra.PixInfraction.Log log in logs){
    Console.Write(log);
}
```

### Get a PixInfraction log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixInfraction.Log log = StarkInfra.PixInfraction.Log.Get("6307030096674816");

Console.Write(log);
```

### Create PixChargebacks

Pix chargebacks can be created when fraud is detected on a transaction or a system malfunction 
results in an erroneous transaction.

```c#
using System;

StarkInfra.PixChargeback chargebacks = StarkInfra.PixChargeback.Create(
    new List<StarkInfra.PixChargeback>{
        new StarkInfra.PixChargeback(
            amount: 100,
            referenceId: "E20018183202201201450u34sDGd19lz",
            reason: "fraud"
        )
    }
);

foreach(StarkInfra.PixChargeback chargeback in chargebacks){
    Console.Write(chargeback);
}
```

### Query PixChargebacks

You can query multiple Pix chargebacks according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixChargeback> chargebacks = StarkInfra.PixChargeback.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: "delivered",
    ids: new List<string> { "6689875965247488" }
);

foreach(StarkInfra.PixChargeback chargeback in chargebacks){
    Console.Write(chargeback);
}
```
### Get a PixChargeback

After its creation, information on a Pix chargebacks may be retrieved by its.

```c#
using System;

StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Get("6689875965247488");

Console.Write(chargeback);
```

### Patch a PixChargeback

A received Pix chargeback can be accepted or rejected by patching its status.
After a Pix chargeback is patched, its status changes to closed.

```c#
using System;
using StarkInfra.Utils;

StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Update(
    id: "6689875965247488",
    result: "accepted",
    patchData: new Dictionary<string, object> {
        { "reversalReferenceId", ReturnId.Create("20018183") }
    }
);

Console.Write(chargeback);
```

### Cancel a PixChargeback

Cancel a specific Pix reversal using its id.

```c#
using System;

StarkInfra.PixChargeback chargeback = StarkInfra.PixChargeback.Cancel("6689875965247488");

Console.Write(chargeback);
```

### Query PixChargeback logs

You can query Pix request logs to better understand Pix chargeback life cycles. 

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixChargeback.Log> logs = StarkInfra.PixChargeback.Log.Query(
    limit: 50,
    ids: new List<string> { "5036445332930560" },
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    types: new List<string> { "closed" },
    chargebackIds: new List<string> { "4806677870149632" }
);

foreach (StarkInfra.PixChargeback.Log log in logs){
    Console.Write(log);
}
```
### Get a PixChargeback log

You can also get a specific log by its id.

```c#
using System;

StarkInfra.PixChargeback.Log chargeback = StarkInfra.PixChargeback.Log.Get("5036445332930560");

Console.Write(chargeback);
```

### Query PixDomains

You can query for certificates of registered SPI participants able to issue dynamic QR Codes.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.PixDomain> domains = StarkInfra.PixDomain.Query();

foreach (StarkInfra.PixDomain domain in domains){
    Console.Write(domain);
}
```

## Credit Note

### Create CreditNotes
You can create Credit notes to generate a CCB contracts:

```c#
using System;

List<StarkInfra.CreditNote> notes = StarkInfra.CreditNote.Create(
    new List<StarkInfra.CreditNote>() { 
        new StarkInfra.CreditNote(
            templateId: "5707012469948416",
            name: "Jamie Lannister",
            taxId: "012.345.678-90",
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
                taxId: "012.345.678-90",
            ),
            paymentType: "transfer",
            signers: new List<StarkInfra.CreditNote.Signer>{
                new StarkInfra.CreditNote.Signer(
                    name: "Jamie Lannister",
                    contact: "jamie.lannister@gmail.com",
                    method: "link"
                )
            },
            externalId: "my_external_id_10",
            streetLine1: "Av. Paulista, 200",
            streetLine2: "10 andar",
            district: "Bela Vista",
            city: "São Paulo",
            stateCode: "SP",
            zipCode: "01310-000"
        )    
    }
);

foreach(StarkInfra.CreditNote note in notes){
    Console.Write(note);
}
```

**Note**: Instead of using CreditNote objects, you can also pass each element in dictionary format

### Query CreditNotes

You can query multiple credit notes according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.CreditNote> notes = StarkInfra.CreditNote.Query(
    limit: 10,
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
    status: new List<string>{ "signed" },
    tags: new List<string>{ "iron", "suit" },
);

foreach(StarkInfra.CreditNote note in notes){
    Console.Write(note);
}
```

### Get a CreditNote

After its creation, information on a credit note may be retrieved by its id.

```c#
using System;

StarkInfra.CreditNote note = StarkInfra.CreditNote.Get("5155165527080960");

Console.Write(note);
```

### Cancel a CreditNote

You can cancel a credit note if it has not been signed yet.

```c#
using System;

StarkInfra.CreditNote note = StarkInfra.CreditNote.Cancel(id: creditNote.ID);

Console.Write(note);
```

### Query CreditNote logs

You can query credit note logs to better understand credit note life cycles. 

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.CreditNote.Log> logs = StarkInfra.CreditNote.Log.Query(
    limit: 50, 
    after: new DateTime(2022, 01, 01),
    before: new DateTime(2022, 12, 01),
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

StarkInfra.CreditNote.Log log = StarkInfra.CreditNote.Log.Get("5155165527080960");

Console.Write(log);
```

## Webhook

### Create a webhook subscription

To create a webhook subscription and be notified whenever an event occurs, run:

```c#
using System;

StarkInfra.Webhook webhook = StarkInfra.Webhook.Create(
    url: "https://webhook.site/6cd3c98d-60ca-4f4a-97c6-a7d0365b15B7",
    subscription: new List<string> {
        "contract",
        "credit-note",
        "signer",
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
        "pix-infraction"        
    }
);

Console.Write(webhook);
```

### Query webhooks

To search for registered webhooks, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.Webhook> webhooks = StarkInfra.Webhook.Query();

foreach (StarkInfra.Webhook webhook in webhooks){
    Console.Write(webhook);
}
```

### Get a webhook

You can get a specific webhook by its id.

```c#
using System;

StarkInfra.Webhook webhook = StarkInfra.Webhook.Get("1082736198236817");

Console.Write(webhook);
```

### Delete a webhook

You can also delete a specific webhook by its id.

```c#
using System;

StarkInfra.Webhook webhook = StarkInfra.Webhook.Delete("1082736198236817");

Console.Write(webhook);
```

## Process webhook events

It's easy to process events delivered to your Webhook endpoint. Remember to pass the
signature header so the SDK can make sure it was StarkInfra that sent you
the event.

```c#
using System;

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
```

### Query webhook events

To search for webhook events, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkInfra.Event> events = StarkInfra.Event.Query(
    after: DateTime.Today,
    isDelivered: false
);

foreach(StarkInfra.Event eventObject in events) {
    Console.WriteLine(eventObject);
}
```

### Get a webhook event

You can get a specific webhook event by its id.

```c#
using System;

StarkInfra.Event eventObject = StarkInfra.Event.Get("1082736198236817");

Console.WriteLine(eventObject);
```

### Delete a webhook event

You can also delete a specific webhook event by its id.

```c#
using System;

StarkInfra.Event eventObject = StarkInfra.Event.Delete("1082736198236817");

Console.WriteLine(eventObject);
```

### Set webhook events as delivered

This can be used in case you"ve lost events.
With this function, you can manually set events retrieved from the API as
"delivered" to help future event queries with `isDelivered: false`.

```c#
using System;

StarkInfra.Event eventObject = StarkInfra.Event.Update("1298371982371921", isDelivered: true);

Console.WriteLine(eventObject);
```

### Query failed webhook event delivery attempts information

You can also get information on failed webhook event delivery attempts.

```c#
using System;

List<StarkInfra.Event.Attempt> attempts = StarkInfra.Event.Attempt.Query(after: "2020-03-20").ToList();

foreach(StarkInfra.Event.Attempt attempt in attempts) {
    Console.WriteLine(attempt);
}
```

### Get a failed webhook event delivery attempt information

To retrieve information on a single attempt, use the following function:

```c#
using System;

StarkInfra.Event.Attempt attempt = StarkInfra.Event.Attempt.Get("1616161616161616");

Console.WriteLine(attempt);
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

try {
    List<StarkInfra.PixReversal> reversal = StarkInfra.PixReversal.Create(
        new List<StarkInfra.PixReversal> {
            new StarkIfnra.PixReversal(
                amount: 100,
                end_to_end_id: "E00000000202201060100rzsJzG9PzMg",
                external_id: "1723843582395893",
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

__InvalidSignatureError__ will be raised specifically by starkinfra.event.parse()
when the provided content and signature do not check out with the Stark Infra public
key.

# Help and Feedback

If you have any questions about our SDK, just send us an email.
We will respond you quickly, pinky promise. We are here to help you integrate with us ASAP.
We also love feedback, so don't be shy about sharing your thoughts with us.

Email: developers@starkbank.com
