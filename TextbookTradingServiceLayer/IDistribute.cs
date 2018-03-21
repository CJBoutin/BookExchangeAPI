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
            UriTemplate = "NewListing")]
        string NewListing(NewListingDetails details);
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
    }

    [DataContract]
    public class NewListingDetails
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Isbn { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Publisher { get; set; }
        [DataMember]
        public string Edition { get; set; }
        [DataMember]
        public string ListPrice { get; set; }
        [DataMember]
        public string Negotiable { get; set; }
        [DataMember]
        public string Condition { get; set; }
    }
}
