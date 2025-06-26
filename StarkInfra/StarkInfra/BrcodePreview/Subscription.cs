using System;


namespace StarkInfra
{
    /// <summary>
    /// Subscription object
    /// <br/>
    /// Subscription is a recurring payment that can be used to charge a user periodically.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: amount to be charged in cents. ex: 1000 = R$ 10.00</item>
    ///     <item>AmountMinLimit [long]: minimum amount limit for the subscription. ex: 500 = R$ 5.00</item>
    ///     <item>BacenId [string]: BACEN (Brazilian Central Bank) identifier.</item>
    ///     <item>Created [DateTime?]: creation datetime for the subscription. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Description [string]: description of the subscription.</item>
    ///     <item>InstallmentEnd [DateTime?]: end datetime for the installments. ex: DateTime(2020, 3, 10)</item>
    ///     <item>InstallmentStart [DateTime?]: start datetime for the installments. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Interval [string]: interval for the recurring charge. ex: "monthly"</item>
    ///     <item>PullRetryLimit [int?]: maximum number of retries for pulling the payment.</item>
    ///     <item>ReceiverBankCode [string]: bank code of the receiver.</item>
    ///     <item>ReceiverName [string]: name of the receiver.</item>
    ///     <item>ReceiverTaxId [string]: tax ID of the receiver.</item>
    ///     <item>ReferenceCode [string]: reference code for the subscription.</item>
    ///     <item>SenderFinalName [string]: final sender name.</item>
    ///     <item>SenderFinalTaxId [string]: final sender tax ID.</item>
    ///     <item>Status [string]: current status of the subscription.</item>
    ///     <item>Type [string]: type of the subscription.</item>
    ///     <item>Updated [DateTime?]: last update datetime for the subscription. ex: DateTime(2020, 3, 10)</item>
    /// </list>
    /// </summary>
    public partial class Subscription : StarkCore.Utils.SubResource
    {
        public long? Amount { get; }
        public long? AmountMinLimit { get; }
        public string BacenId { get; }
        public DateTime? Created { get; }
        public string Description { get; }
        public DateTime? InstallmentEnd { get; }
        public DateTime? InstallmentStart { get; }
        public string Interval { get; }
        public int? PullRetryLimit { get; }
        public string ReceiverBankCode { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxId { get; }
        public string ReferenceCode { get; }
        public string SenderFinalName { get; }
        public string SenderFinalTaxId { get; }
        public string Status { get; }
        public string Type { get; }
        public DateTime? Updated { get; }

        public Subscription(
            long? amount = null, long? amountMinLimit = null, string bacenId = null, DateTime? created = null,
            string description = null, DateTime? installmentEnd = null, DateTime? installmentStart = null, string interval = null,
            int? pullRetryLimit = null, string receiverBankCode = null, string receiverName = null, string receiverTaxId = null,
            string referenceCode = null, string senderFinalName = null, string senderFinalTaxId = null, string status = null,
            string type = null, DateTime? updated = null
        )
        {
            Amount = amount;
            AmountMinLimit = amountMinLimit;
            BacenId = bacenId;
            Created = created;
            Description = description;
            InstallmentEnd = installmentEnd;
            InstallmentStart = installmentStart;
            Interval = interval;
            PullRetryLimit = pullRetryLimit;
            ReceiverBankCode = receiverBankCode;
            ReceiverName = receiverName;
            ReceiverTaxId = receiverTaxId;
            ReferenceCode = referenceCode;
            SenderFinalName = senderFinalName;
            SenderFinalTaxId = senderFinalTaxId;
            Status = status;
            Type = type;
            Updated = updated;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Subscription", resourceMaker: ResourceMaker);
        }

        internal static Subscription ResourceMaker(dynamic json)
        {
            long? amount = json.amount;
            long? amountMinLimit = json.amountMinLimit;
            string bacenId = json.bacenId;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.created);
            string description = json.description;
            DateTime? installmentEnd = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.installmentEnd);
            DateTime? installmentStart = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.installmentStart);
            string interval = json.interval;
            int? pullRetryLimit = json.pullRetryLimit;
            string receiverBankCode = json.receiverBankCode;
            string receiverName = json.receiverName;
            string receiverTaxId = json.receiverTaxId;
            string referenceCode = json.referenceCode;
            string senderFinalName = json.senderFinalName;
            string senderFinalTaxId = json.senderFinalTaxId;
            string status = json.status;
            string type = json.type;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime((string)json.updated);

            return new Subscription(
                amount: amount, amountMinLimit: amountMinLimit, bacenId: bacenId, created: created,
                description: description, installmentEnd: installmentEnd, installmentStart: installmentStart,
                interval: interval, pullRetryLimit: pullRetryLimit, receiverBankCode: receiverBankCode,
                receiverName: receiverName, receiverTaxId: receiverTaxId, referenceCode: referenceCode,
                senderFinalName: senderFinalName, senderFinalTaxId: senderFinalTaxId, status: status,
                type: type, updated: updated
            );
        }
    }
}