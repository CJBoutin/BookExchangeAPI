using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TextbookTradingServiceLayer
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDistribute
    {

        // Asks the WCF service if it's alive
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "IsAlive")]
        string IsAlive();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Authenticate")]
        string Authenticate(LoginDetails details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NewUser")]
        string CreateNewUser(NewUser details);


        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "NewListing")]
        string NewListing(ListingDetails details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetProfile?Uid={x}")]
        string GetProfile(int x);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "AcceptPurchase")]
        string AcceptPurchase(AcceptPurchase details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SearchTransactions")]
        string SearchTransactions(ProductDetails details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TransactionDetails?TranId={transactionId}")]
        string GetTransactionDetails(int transactionId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UpdateProfile")]
        string UpdateProfile(UpdateUser details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MakeBid")]
        string MakeBid(BidData details);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetBids?TranId={transactionId}")]
        string GetBid(int transactionId);

    }

    [DataContract]
    public class LoginDetails
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
    }

    [DataContract]
    public class NewUser
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }

    [DataContract]
    public class UpdateUser
    {
        [DataMember]
        public int UIdToChange { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string NewPwHash { get; set; }
    }


    [DataContract]
    public class BasicUserData
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
    [DataContract]
    public class ListingDetails
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ISBN { get; set; }
        [DataMember]
        public List<string> Images { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Publisher { get; set; }
        [DataMember]
        public string Edition { get; set; }
        [DataMember]
        public int ListPrice { get; set; }
        [DataMember]
        public int Negotiable { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Condition { get; set; }
    }

    [DataContract]
    public class AcceptPurchase
    {
        [DataMember]
        public int SellerId { get; set; }

        [DataMember]
        public int BidId { get; set; }
    }

    [DataContract]
    public class ProductDetails
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string ISBN { get; set; }
        [DataMember]
        public string Author { get; set; }
    }

    [DataContract]
    public class BidData
    {
        [DataMember]

        public int PurchaserId { get; set; }
        [DataMember]

        public int TransactionId { get; set; }
        [DataMember]
        public int ProposedPrice { get; set; }
    }

}
