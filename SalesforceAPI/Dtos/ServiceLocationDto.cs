using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SalesforceAPI.Dtos
{
    public class ServiceLocationDto
    {
        public string? Credentialing_Profile__c { get; set; }
        public string? Account_Site__c { get; set; }
        public string? Account__c { get; set; }
        public bool? Facility_License_if_applicable__c { get; set; }
        public DateTime? Facility_License_Expiration_if_applicab__c { get; set; }
        public string? Facility_License_Number_if_applicable__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FacilityLicenseStatusIfApplicableCEnum? Facility_License_Status_if_applicable__c { get; set; }
        public string? Hours_of_Operation__c { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccomodationsAccessibilityCEnum? Accomodations_Accessibility__c { get; set; }
        public string? Accomodations_Accessibility_Other__c { get; set; }
        public bool? Licensed_Facility__c { get; set; }
        public enum FacilityLicenseStatusIfApplicableCEnum
        {
            [EnumMember(Value = "Active")]
            ActiveEnum = 0,
            [EnumMember(Value = "Expired")]
            ExpiredEnum = 1
        }
        public enum AccomodationsAccessibilityCEnum
        {
            [EnumMember(Value = "Accessible Alarms")]
            AccessibleAlarmsEnum = 0,
            [EnumMember(Value = "Accessible Drinking Fountains")]
            AccessibleDrinkingFountainsEnum = 1,
            [EnumMember(Value = "Accessible Entrance")]
            AccessibleEntranceEnum = 2,
            [EnumMember(Value = "Accessible Parking")]
            AccessibleParkingEnum = 3,
            [EnumMember(Value = "Accessible Route")]
            AccessibleRouteEnum = 4,
            [EnumMember(Value = "Accessible Telephones")]
            AccessibleTelephonesEnum = 5,
            [EnumMember(Value = "Accessible treatment rooms")]
            AccessibleTreatmentRoomsEnum = 6,
            [EnumMember(Value = "Accessible restrooms")]
            AccessibleRestroomsEnum = 7,
            [EnumMember(Value = "Automatic doors/doorbell")]
            AutomaticDoorsdoorbellEnum = 8,
            [EnumMember(Value = "Elevator or no steps")]
            ElevatorOrNoStepsEnum = 9,
            [EnumMember(Value = "Grab bars in restrooms")]
            GrabBarsInRestroomsEnum = 10,
            [EnumMember(Value = "Handicap accessible office building")]
            HandicapAccessibleOfficeBuildingEnum = 11,
            [EnumMember(Value = "Hearing-Impaired")]
            HearingImpairedEnum = 12,
            [EnumMember(Value = "Intercoms in residential bathrooms")]
            IntercomsInResidentialBathroomsEnum = 13,
            [EnumMember(Value = "Interpreters available")]
            InterpretersAvailableEnum = 14,
            [EnumMember(Value = "Reasonable accommodations")]
            ReasonableAccommodationsEnum = 15,
            [EnumMember(Value = "Speech Impaired")]
            SpeechImpairedEnum = 16,
            [EnumMember(Value = "Visually Impaired")]
            VisuallyImpairedEnum = 17,
            [EnumMember(Value = "Wheelchair")]
            WheelchairEnum = 18,
            [EnumMember(Value = "None")]
            NoneEnum = 19,
            [EnumMember(Value = "Other")]
            OtherEnum = 20
        }

    }
}
