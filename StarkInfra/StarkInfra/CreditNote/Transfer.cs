using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote.Transfer object
    /// <br/>
    /// Transfer sent to the credit receiver after the contract is signed.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
    ///     <item>TaxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>BankCode [string]: code of the receiver bank institution in Brazil. ex: "20018183"</item>
    ///     <item>BranchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///     <item>AccountNumber [string]: Receiver bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///     <item>AccountType [string, default "checking"]: Receiver bank account type. This parameter only has effect on Pix Transfers. ex: "checking", "savings", "salary" or "payment"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for transfers. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>ID [string]: unique id returned when the transfer is created. ex: "5656565656565656"</item>
    ///     <item>Amount [integer]: amount in cents to be transferred. ex: 1234 (= R$ 12.34)</item>
    ///     <item>ExternalID [string]: url safe string that must be unique among all your transfers. Duplicated externalIds will cause failures. By default, this parameter will block any transfer that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///     <item>Scheduled [DateTime]: DateTime when the transfer will be processed. May be pushed to next business day if necessary. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Description [string]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
    ///     <item>Fee [integer]: fee charged when the Transfer is processed. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string]: current transfer status. ex: "success" or "failed"</item>
    ///     <item>TransactionIds [list of strings]: ledger Transaction IDs linked to this Transfer (if there are two, the second is the chargeback). ex: List<string>{ "19827356981273" }</item>
    ///     <item>Created [DateTime]: creation DateTime for the transfer. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the transfer. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Transfer : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public List<string> Tags { get; }
        public long? Amount { get; }
        public string ExternalID { get; }
        public DateTime? Scheduled { get; }
        public string Description { get; }
        public int? Fee { get; }
        public string Status { get; }
        public List<string> TransactionIds { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// CreditNote.Transfer object
        /// <br/>
        /// Transfer sent to the credit receiver after the contract is signed.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: receiver full name. ex: "Anthony Edward Stark"</item>
        ///     <item>taxID [string]: receiver tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>bankCode [string]: code of the receiver bank institution in Brazil. ex: "20018183" or "341"</item>
        ///     <item>branchCode [string]: receiver bank account branch. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///     <item>accountNumber [string]: Receiver bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>accountType [string, default "checking"]: Receiver bank account type. This parameter only has effect on Pix Transfers. ex: "checking", "savings", "salary" or "payment"</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for transfers. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the transfer is created. ex: "5656565656565656"</item>
        ///     <item>amount [integer]: amount in cents to be transferred. ex: 1234 (= R$ 12.34)</item>
        ///     <item>externalID [string]: url safe string that must be unique among all your transfers. Duplicated externalIds will cause failures. By default, this parameter will block any transfer that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///     <item>scheduled [DateTime]: DateTime when the transfer will be processed. May be pushed to next business day if necessary. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>description [string]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
        ///     <item>fee [integer]: fee charged when the Transfer is processed. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string]: current transfer status. ex: "success" or "failed"</item>
        ///     <item>transactionIds [list of strings]: ledger Transaction IDs linked to this Transfer (if there are two, the second is the chargeback). ex: List<string>{ "19827356981273" }</item>
        ///     <item>created [DateTime]: creation DateTime for the transfer. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the transfer. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        ///
        public Transfer(string name, string taxID, string bankCode, string branchCode, string accountNumber, string accountType = "checking",
            List<string> tags = null, long? amount = null, string externalID = null, DateTime? scheduled = null, string description = null,
            string id = null, int? fee = null, string status = null, List<string> transactionIds = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            TaxID = taxID;
            BankCode = bankCode;
            BranchCode = branchCode;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Tags = tags;
            Amount = amount;
            ExternalID = externalID;
            Scheduled = scheduled;
            Description = description;
            Fee = fee;
            Status = status;
            TransactionIds = transactionIds;
            Created = created;
            Updated = updated;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Transfer", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            string taxID = json.taxId;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            List<string> tags = json.tags?.ToObject<List<string>>();
            long? amount = json.amount;
            string externalID = json.externalId;
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckDateTime(scheduledString);
            string description = json.description;
            int? fee = json.fee;
            string status = json.status;
            List<string> transactionIds = json.transactionIds?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);


            return new Transfer(
                id: id, name: name, taxID: taxID, bankCode: bankCode, branchCode: branchCode,
                accountNumber: accountNumber, accountType: accountType, tags: tags, amount: amount,
                externalID: externalID, scheduled: scheduled, description: description, fee: fee,
                status: status, transactionIds: transactionIds, created: created, updated: updated
            );
        }
    }
}
