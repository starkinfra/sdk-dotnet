using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BrcodePreview object
    /// <br/>
    /// The BrcodePreview object is used to preview information from a BR Code before paying it.
    /// <br/>
    /// When you initialize a BrcodePreview, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: BR Code string for the Pix payment. This is also the information directly encoded in a QR Code. ex: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
    ///     <item>PayerID [string]: tax id (CPF/CNPJ) of the individual or business requesting the PixKey information. This id is used by the Central Bank to limit request rates. ex: "20.018.183/0001-80"
    ///     <item>EndToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"
    ///     <item>AccountNumber [string]: Payment receiver account number. ex: "1234567"</item>
    ///     <item>AccountType [string]: Payment receiver account type. ex: "checking"</item>
    ///     <item>Amount [integer]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
    ///     <item>AmountType [string]: Amount type of the BR Code. If the amount type is "custom" the BR Code's amount can be changed by the sender at the moment of payment. Options: "fixed" or "custom"</item>
    ///     <item>BankCode [string]: Payment receiver bank code. ex: "20018183"</item>
    ///     <item>BranchCode [string]: Payment receiver branch code. ex: "0001"</item>
    ///     <item>CashAmount [integer]: Amount to be withdrawn from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
    ///     <item>CashierBankCode [string]: Cashier's bank code. ex: "20018183"</item>
    ///     <item>CashierType [string]: Cashier's type. Options: "merchant", "participant" and "other"</item>
    ///     <item>DiscountAmount [integer]: Discount value calculated over nominalAmount. ex: 3000</item>
    ///     <item>FineAmount [integer]: Fine value calculated over nominalAmount. ex: 20000</item>
    ///     <item>InterestAmount [integer]: Interest value calculated over nominalAmount. ex: 10000</item>
    ///     <item>KeyID [string]: Receiver's PixKey id. ex: "+5511989898989"</item>
    ///     <item>Name [string]: Payment receiver name. ex: "Tony Stark"</item>
    ///     <item>NominalAmount [integer]: BR Code emission amount, without fines, fees and discounts. ex: 1234 (= R$ 12.34)</item>
    ///     <item>ReconciliationID [string]: Reconciliation ID linked to this payment. If the BR Code is dynamic, the reconciliationID will have from 26 to 35 alphanumeric characters, ex: "cd65c78aeb6543eaaa0170f68bd741ee". If the brcode is static, the reconciliationID will have up to 25 alphanumeric characters "ah27s53agj6493hjds6836v49"</item>
    ///     <item>ReductionAmount [integer]: Reduction value to discount from nominalAmount. ex: 1000</item>
    ///     <item>Scheduled [DateTime]: date of payment execution. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Status [string]: Payment status. Options: "active", "paid", "canceled" or "unknown"</item>
    ///     <item>TaxID [string]: Payment receiver tax ID. ex: "012.345.678-90"</item>
    /// </list>
    /// </summary>
    public partial class BrcodePreview : Resource
    {
        public string PayerID { get; }
        public string EndToEndID { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public int? Amount { get; }
        public string AmountType { get; }
        public string BankCode { get; }
        public string BranchCode { get; }
        public int? CashAmount { get; }
        public string CashierBankCode { get; }
        public string CashierType { get; }
        public int? DiscountAmount { get; }
        public int? FineAmount { get; }
        public int? InterestAmount { get; }
        public string KeyID { get; }
        public string Name { get; }
        public int? NominalAmount { get; }
        public string ReconciliationID { get; }
        public int? ReductionAmount { get; }
        public DateTime? Scheduled { get; }
        public string Status { get; }
        public string TaxID { get; }

        /// <summary>
        /// BrcodePreview object
        /// <br/>
        /// The BrcodePreview object is used to preview information from a BR Code before paying it.
        /// <br/>
        /// When you initialize a BrcodePreview, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: BR Code string for the Pix payment. This is also the information directly encoded in a QR Code. ex: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
        ///     <item>payerID [string]: tax id (CPF/CNPJ) of the individual or business requesting the PixKey information. This id is used by the Central Bank to limit request rates. ex: "20.018.183/0001-80"
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>endToEndID [string, default null]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>accountNumber [string]: Payment receiver account number. ex: "1234567"</item>
        ///     <item>accountType [string]: Payment receiver account type. ex: "checking"</item>
        ///     <item>amount [integer]: Value in cents that this payment is expecting to receive. If 0, any value is accepted. ex: 123 (= R$1,23)</item>
        ///     <item>amountType [string]: Amount type of the BR Code. If the amount type is "custom" the BR Code's amount can be changed by the sender at the moment of payment. Options: "fixed" or "custom"</item>
        ///     <item>bankCode [string]: Payment receiver bank code. ex: "20018183"</item>
        ///     <item>branchCode [string]: Payment receiver branch code. ex: "0001"</item>
        ///     <item>cashAmount [integer]: Amount to be withdrawn from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
        ///     <item>cashierBankCode [string]: Cashier's bank code. ex: "20018183"</item>
        ///     <item>cashierType [string]: Cashier's type. Options: "merchant", "participant" and "other"</item>
        ///     <item>discountAmount [integer]: Discount value calculated over nominalAmount. ex: 3000</item>
        ///     <item>fineAmount [integer]: Fine value calculated over nominalAmount. ex: 20000</item>
        ///     <item>interestAmount [integer]: Interest value calculated over nominalAmount. ex: 10000</item>
        ///     <item>keyID [string]: Receiver's PixKey id. ex: "+5511989898989"</item>
        ///     <item>name [string]: Payment receiver name. ex: "Tony Stark"</item>
        ///     <item>nominalAmount [integer]: BR Code emission amount, without fines, fees and discounts. ex: 1234 (= R$ 12.34)</item>
        ///     <item>reconciliationID [string]: Reconciliation ID linked to this payment. If the BR Code is dynamic, the reconciliationID will have from 26 to 35 alphanumeric characters, ex: "cd65c78aeb6543eaaa0170f68bd741ee". If the brcode is static, the reconciliationID will have up to 25 alphanumeric characters "ah27s53agj6493hjds6836v49"</item>
        ///     <item>reductionAmount [integer]: Reduction value to discount from nominalAmount. ex: 1000</item>
        ///     <item>scheduled [DateTime]: date of payment execution. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string]: Payment status. Options: "active", "paid", "canceled" or "unknown"</item>
        ///     <item>taxID [string]: Payment receiver tax ID. ex: "012.345.678-90"</item>
        /// </list>
        /// </summary>
        public BrcodePreview( 
            string id, string payerID, string endToEndID = null, string accountNumber = null, 
            string accountType = null, int? amount = null, string amountType = null, string bankCode = null, 
            string branchCode = null, int? cashAmount = null, string cashierBankCode = null, 
            string cashierType = null, int? discountAmount = null, int? fineAmount = null, 
            int? interestAmount = null, string keyID = null, string name = null, 
            int? nominalAmount = null, string reconciliationID = null, int? reductionAmount = null, 
            DateTime? scheduled = null, string status = null, string taxID = null
        ) : base(id)
        {
            PayerID = payerID;
            EndToEndID = endToEndID;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Amount = amount;
            AmountType = amountType;
            BankCode = bankCode;
            BranchCode = branchCode;
            CashAmount = cashAmount;
            CashierBankCode = cashierBankCode;
            CashierType = cashierType;
            DiscountAmount = discountAmount;
            FineAmount = fineAmount;
            InterestAmount = interestAmount;
            KeyID = keyID;
            Name = name;
            NominalAmount = nominalAmount;
            ReconciliationID = reconciliationID;
            ReductionAmount = reductionAmount;
            Scheduled = scheduled;
            Status = status;
            TaxID = taxID;
        }
        
        /// <summary>
        /// Create BrcodePreview objects
        /// <br/>
        /// Process BR Codes before paying them.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of BrcodePreview objects]: List of BrcodePreview objects to preview. ex: { new StarkInfra.BrcodePreview("00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A") }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>list of BrcodePreview objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BrcodePreview> Create(List<BrcodePreview> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (BrcodePreview)o);
        }

        /// <summary>
        /// Create BrcodePreview objects
        /// <br/>
        /// Process BR Codes before paying them.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of Dictionaries]: list of Dictionaries representing the BrcodePreview objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BrcodePreview objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BrcodePreview> Create(List<Dictionary<string, object>> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (BrcodePreview)o);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BrcodePreview", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string payerID = json.payerID;
            string endToEndID = json.endToEndID;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            int? amount = json.amount;
            string amountType = json.amountType;
            string bankCode = json.bankCode;
            string branchCode = json.branchCode;
            int? cashAmount = json.cashAmount;
            string cashierBankCode = json.cashierBankCode;
            string cashierType = json.cashierType;
            int? discountAmount = json.discountAmount;
            int? fineAmount = json.fineAmount;
            int? interestAmount = json.interestAmount;
            string keyID = json.keyId;
            string name = json.name;
            int? nominalAmount = json.nominalAmount;
            string reconciliationID = json.reconciliationId;
            int? reductionAmount = json.reductionAmount;
            DateTime? scheduled = json.scheduled;
            string status = json.status;
            string taxID = json.taxId;

            return new BrcodePreview(
                id: id, payerID: payerID, endToEndID: endToEndID, accountNumber: accountNumber, accountType: accountType, 
                amount: amount, amountType: amountType, bankCode: bankCode, 
                branchCode: branchCode, cashAmount: cashAmount, cashierBankCode: cashierBankCode, 
                cashierType: cashierType, discountAmount: discountAmount, fineAmount: fineAmount, 
                interestAmount: interestAmount, keyID: keyID, name: name, 
                nominalAmount: nominalAmount, reconciliationID: reconciliationID, reductionAmount: reductionAmount, 
                scheduled: scheduled, status: status, taxID: taxID
            );
        }
    }
}
