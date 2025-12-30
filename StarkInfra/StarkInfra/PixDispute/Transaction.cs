using System;


namespace StarkInfra
{
    public partial class PixDispute
    {
        /// <summary>
        /// PixDispute.Transaction object
        /// <br/>
        /// Transaction object related to the PixDispute.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>EndToEndID [string]: Central Bank's unique transaction id. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>Amount [long]: refundable amount. ex: 11234 (= R$ 112.34)</item>
        ///     <item>NominalAmount [long]: transaction amount. ex: 11234 (= R$ 112.34)</item>
        ///     <item>ReceiverType [string]: receiver person type. Options: "individual", "business"</item>
        ///     <item>ReceiverTaxIDCreated [string]: receiver's taxId creation date. For business type only.</item>
        ///     <item>ReceiverAccountCreated [string]: receiver's account creation date.</item>
        ///     <item>ReceiverBankCode [string]: receiver's bank code. ex: "20018183"</item>
        ///     <item>ReceiverID [string]: identifier of accountholder in the graph.</item>
        ///     <item>SenderType [string]: sender person type. Options: "individual", "business"</item>
        ///     <item>SenderTaxIDCreated [string]: sender's taxId creation date. For business type only.</item>
        ///     <item>SenderAccountCreated [string]: sender's account creation date.</item>
        ///     <item>SenderBankCode [string]: sender's bank code. ex: "20018183"</item>
        ///     <item>SenderID [string]: identifier of accountholder in the graph.</item>
        ///     <item>Settled [DateTime]: settled datetime of the transaction in ISO format. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Transaction
        {
            public string EndToEndID { get; }
            public long? Amount { get; }
            public long? NominalAmount { get; }
            public string ReceiverType { get; }
            public string ReceiverTaxIDCreated { get; }
            public string ReceiverAccountCreated { get; }
            public string ReceiverBankCode { get; }
            public string ReceiverID { get; }
            public string SenderType { get; }
            public string SenderTaxIDCreated { get; }
            public string SenderAccountCreated { get; }
            public string SenderBankCode { get; }
            public string SenderID { get; }
            public DateTime? Settled { get; }

            /// <summary>
            /// PixDispute.Transaction object
            /// <br/>
            /// Transaction object related to the PixDispute.
            /// <br/>
            /// Parameters:
            /// <list>
            ///     <item>endToEndID [string]: Central Bank's unique transaction id. ex: "E79457883202101262140HHX553UPqeq"</item>
            ///     <item>amount [long]: refundable amount. ex: 11234 (= R$ 112.34)</item>
            ///     <item>nominalAmount [long]: transaction amount. ex: 11234 (= R$ 112.34)</item>
            ///     <item>receiverType [string]: receiver person type. Options: "individual", "business"</item>
            ///     <item>receiverTaxIDCreated [string]: receiver's taxId creation date. For business type only.</item>
            ///     <item>receiverAccountCreated [string]: receiver's account creation date.</item>
            ///     <item>receiverBankCode [string]: receiver's bank code. ex: "20018183"</item>
            ///     <item>receiverID [string]: identifier of accountholder in the graph.</item>
            ///     <item>senderType [string]: sender person type. Options: "individual", "business"</item>
            ///     <item>senderTaxIDCreated [string]: sender's taxId creation date. For business type only.</item>
            ///     <item>senderAccountCreated [string]: sender's account creation date.</item>
            ///     <item>senderBankCode [string]: sender's bank code. ex: "20018183"</item>
            ///     <item>senderID [string]: identifier of accountholder in the graph.</item>
            ///     <item>settled [DateTime]: settled datetime of the transaction in ISO format. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Transaction(string endToEndID = null, long? amount = null, long? nominalAmount = null,
                string receiverType = null, string receiverTaxIDCreated = null, string receiverAccountCreated = null,
                string receiverBankCode = null, string receiverID = null, string senderType = null,
                string senderTaxIDCreated = null, string senderAccountCreated = null, string senderBankCode = null,
                string senderID = null, DateTime? settled = null)
            {
                EndToEndID = endToEndID;
                Amount = amount;
                NominalAmount = nominalAmount;
                ReceiverType = receiverType;
                ReceiverTaxIDCreated = receiverTaxIDCreated;
                ReceiverAccountCreated = receiverAccountCreated;
                ReceiverBankCode = receiverBankCode;
                ReceiverID = receiverID;
                SenderType = senderType;
                SenderTaxIDCreated = senderTaxIDCreated;
                SenderAccountCreated = senderAccountCreated;
                SenderBankCode = senderBankCode;
                SenderID = senderID;
                Settled = settled;
            }

            internal static Transaction ResourceMaker(dynamic json)
            {
                string endToEndID = json.endToEndId;
                long? amount = json.amount;
                long? nominalAmount = json.nominalAmount;
                string receiverType = json.receiverType;
                string receiverTaxIDCreated = json.receiverTaxIdCreated;
                string receiverAccountCreated = json.receiverAccountCreated;
                string receiverBankCode = json.receiverBankCode;
                string receiverID = json.receiverId;
                string senderType = json.senderType;
                string senderTaxIDCreated = json.senderTaxIdCreated;
                string senderAccountCreated = json.senderAccountCreated;
                string senderBankCode = json.senderBankCode;
                string senderID = json.senderId;
                string settledString = json.settled;
                DateTime? settled = StarkCore.Utils.Checks.CheckDateTime(settledString);

                return new Transaction(
                    endToEndID: endToEndID, amount: amount, nominalAmount: nominalAmount,
                    receiverType: receiverType, receiverTaxIDCreated: receiverTaxIDCreated,
                    receiverAccountCreated: receiverAccountCreated, receiverBankCode: receiverBankCode,
                    receiverID: receiverID, senderType: senderType, senderTaxIDCreated: senderTaxIDCreated,
                    senderAccountCreated: senderAccountCreated, senderBankCode: senderBankCode,
                    senderID: senderID, settled: settled
                );
            }
        }
    }
}
