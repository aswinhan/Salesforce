namespace SalesforceAPI.Dtos
{
    public class HospitalAffiliationDto
    {
        public string Hospital_Affiliation_Address__c { get; set; }
        public string Hospital_Affiliation_Name__c { get; set; }
        public string Category_of_Membership__c { get; set; }
        public DateTime? Start_Date_of_Affiliation__c { get; set; }
        public DateTime? End_Date_of_Affiliation__c { get; set; }
    }
}
