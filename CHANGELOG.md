# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.


## [Unreleased]

## [0.10.0] - 2025-07-23
### Added
- fraudType parameter to PixInfraction resource
- bacenId attribute to PixChargeback resource

## [0.9.0] - 2025-07-08
### Added
- endToEndId attribute to BrcodePreview resource

## [0.8.0] - 2025-06-27
### Added
- subcription and due attribute to BrcodePreview resource

## [0.7.0] - 2025-05-13
### Added
- payerId parameter to BrcodePreview resource

## [0.6.0] - 2025-04-15
### Fix
- .csproj file
### Added
- method, operatorPhone and operatorEmail parameters to PixInfraction resource

## [0.5.2] - 2024-10-18
### Fixed
- IssuingCard expand expiration

## [0.5.1] - 2024-09-11
### Added
- PixUser resource 

## [0.5.0] - 2024-07-02
### Added
- request methods
### Changed
- core version

## [0.4.1] - 2024-06-13
### Fixed
- EndToEndId date format

## [0.4.0] - 2024-03-19
### Added
- IndividualIdentity resource
- IndividualDocument resource
- IssuingEmbossingKit resource
- CreditHolmes resource
### Changed
- cardDesingID and envelopID to kitID attribute

## [0.3.0] - 2024-01-03
### Changed
- senderTaxID and receiverTaxID parameters to DynamicBrcode resource
- internal structure to use starkcore as a dependency
- starkcore version
- sdk version
### Added
- metadata attribute to IssuingPurchase resource

## [0.2.0] - 2022-12-12
### Added
- IssuingDesign resource
- IssuingEmbossingRequest resource
- IssuingRestock resource 
- IssuingStock resource
- BrcodePreview resource
- CreditPreview sub-resource
- CreditNotePreview sub-resource
- code attribute to IssuingProduct resource
- default to fee, externalId and tags in parse method of PixRequest and PixReversal resources
- tags parameter to PixClaim, PixInfraction, Pix Chargeback, DynamicBrcode and StaticBrcode resources
- tags parameter to query and page methods in PixChargeback, PixClaim and PixInfraction resources
- flow parameter to PixClaim resource
- flow parameter to query and page methods of PixClaim
- zipCode, purchase, isPartialAllowed, cardTags and holderTags attributes to IssuingPurchase resource
- brcode, link and due attributes to IssuingInvoice resource
- StaticBrcode resource
- DynamicBrcode resource
- IssuingRule.Method sub-resource
- IssuingRule.Country sub-resource
- IssuingRule.Category sub-resource
- parse method to IssuingPurchase resource
- response method to PixRequest, PixReversal and IssuingPurchase resources
- nominalInterest attribute to CreditNote resource
### Changed
- PixDirector from resource to sub-resource
- IssuingBin resource to IssuingProduct
- settlement parameter to fundingType in IssuingProduct resource
- client parameter to holderType in IssuingProduct resource
- agent parameter to flow in PixInfraction and PixChargeback resources
- agent parameter to flow in query and page methods of PixInfraction and PixChargeback resources
- bankCode parameter to claimerBankCode in PixClaim resource
- fine and interest attributes to return only in CreditNote.Invoice sub-resource
- expiration from returned-only to optional parameter in the CreditNote resource 
- CreditNote.Signer sub-resource to CreditSigner resource
### Removed
- category parameter from IssuingProduct resource
- bacenId parameter from PixChargeback and PixInfraction resources
- agent parameter from PixClaim.Log resource
- IssuingAuthorization resource
- bankCode attribute from PixReversal resource


## [0.1.0] - 2022-06-03
### Added
- credit receiver's billing address on CreditNote

## [0.0.2] - 2022-05-26
### Added
- PixDirector resource for Direct Participants
- PixKey resource for Indirect and Direct Participants
- PixClaim resource for Indirect and Direct Participants
- PixDomain resource for Indirect and Direct Participants
- PixInfraction resource for Indirect and Direct Participants
- PixChargeback resource for Indirect and Direct Participants
- CreditNote resource for money lending with Stark's Infra endorsement.
- IssuingAuthorization resource for Sub Issuers
- IssuingBalance resource for Sub Issuers
- IssuingBin resource for Sub Issuers
- IssuingCard resource for Sub Issuers
- IssuingHolder resource for Sub Issuers
- IssuingPurchase resource for Sub Issuers
- IssuingTransaction resource for Sub Issuers
- IssuingInvoice resource for Sub Issuers
- IssuingWithdrawal resource for Sub Issuers
- Webhook resource to receive Events
- Event.Attempt sub-resource to allow retrieval of information on failed webhook event delivery attempts


## [0.0.1] - 2022-03-15
### Added
- PixRequest resource for Indirect and Direct Participants
- PixReversal resource for Indirect and Direct Participants
- PixBalance resource for Indirect and Direct Participants
- PixStatement resource for Direct Participants
- Event resource for webhook receptions
