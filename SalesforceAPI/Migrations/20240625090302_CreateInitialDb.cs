using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesforceAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CredentialingContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPersonRole = table.Column<int>(type: "int", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContact = table.Column<bool>(type: "bit", nullable: true),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialingContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCertification = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalCredentialingProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    AccreditationEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccreditationStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccreditingBody = table.Column<int>(type: "int", nullable: true),
                    ProofofAccreditationStatus = table.Column<bool>(type: "bit", nullable: true),
                    Whenwasthelastaccreditationsurvey = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxID = table.Column<long>(type: "bigint", nullable: true),
                    MedicareProvider = table.Column<bool>(type: "bit", nullable: true),
                    MedicareNumber = table.Column<long>(type: "bigint", nullable: true),
                    NPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddressStreet1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddressZipcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddressCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddressStreet1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddressZipcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddressCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialties = table.Column<int>(type: "int", nullable: true),
                    OtherSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificateofLiability = table.Column<bool>(type: "bit", nullable: true),
                    CertificateofLiabilityExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguagesSpoken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CulturalCompetences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X6monthgapinemploymentsinceprofess = table.Column<int>(type: "int", nullable: true),
                    X6MonthGapStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    X6MonthGapEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    X6MonthGapActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X6MonthGapReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PleaseacknowledgeIfdeniedcredential = table.Column<bool>(type: "bit", nullable: true),
                    Canyouperformtheessentialdutiesof = table.Column<int>(type: "int", nullable: true),
                    Lackofpresentillegaldruguse = table.Column<int>(type: "int", nullable: true),
                    Historyoflossoflicense = table.Column<int>(type: "int", nullable: true),
                    Historyoffelonyconvictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Historyoflossorlimitationsofprivil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExplanationHistoryoffelonyconvictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentMalpracticeInsuranceCoverage = table.Column<int>(type: "int", nullable: true),
                    Attestationbytheapplicantofthecorr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CulturalCompetenciesPicklist = table.Column<int>(type: "int", nullable: true),
                    W9Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    W9Status = table.Column<int>(type: "int", nullable: true),
                    DisclosureFormExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfessionalLiabilityExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommercialLiabilityExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkersCompensationExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fiveyearhistoryorgliability = table.Column<int>(type: "int", nullable: true),
                    Explanationfiveyearhistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actionpendingorglicense = table.Column<int>(type: "int", nullable: true),
                    Explanationorglicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orgaccreditationlimited = table.Column<int>(type: "int", nullable: true),
                    Explanationorgaccreditationlimited = table.Column<int>(type: "int", nullable: true),
                    Actionpendingcurrentaccreditation = table.Column<int>(type: "int", nullable: true),
                    Explanationcurrentaccreditation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orgmedicaidsanctions = table.Column<int>(type: "int", nullable: true),
                    Orgmedicaresanctions = table.Column<int>(type: "int", nullable: true),
                    Explanationorgmedicaidsanctions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanationorgmedicaresanctions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orginsuranceinitiallyrefused = table.Column<int>(type: "int", nullable: true),
                    Explanationorginsuranceinitiallyrefused = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgdefendantSUDpayment = table.Column<int>(type: "int", nullable: true),
                    ExplanationorgdefendantSUDpayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgmalpracticeclaimsSUD = table.Column<int>(type: "int", nullable: true),
                    ExplanationorgmalpracticeclaimsSUD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pleaseindicateifyouhaveaspeciality = table.Column<bool>(type: "bit", nullable: true),
                    ProofofAccreditation = table.Column<bool>(type: "bit", nullable: true),
                    CyberLiabilityifapplicable = table.Column<bool>(type: "bit", nullable: true),
                    CommercialLiability = table.Column<bool>(type: "bit", nullable: true),
                    ProofofAccreditationExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CyberLiabilityExpirationifapplicable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CyberLiabilityStatusifapplicable = table.Column<int>(type: "int", nullable: true),
                    DisclosureFormStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalCredentialingProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalPrimarySourceVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CredentialingProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimarySourceVerifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVO = table.Column<bool>(type: "bit", nullable: true),
                    OtherAccred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiersCredentialingOrganization = table.Column<int>(type: "int", nullable: true),
                    LARALicense = table.Column<int>(type: "int", nullable: true),
                    LARAUploaded = table.Column<bool>(type: "bit", nullable: true),
                    ProofofAccreditation = table.Column<bool>(type: "bit", nullable: true),
                    Disciplinarystatuswithregulatoryboar = table.Column<bool>(type: "bit", nullable: true),
                    MDHHSSanctionedProviderCheck = table.Column<bool>(type: "bit", nullable: true),
                    Atleastfiveyearhistoryoforganizati = table.Column<bool>(type: "bit", nullable: true),
                    OnSiteQualityAssessmentRecredential = table.Column<bool>(type: "bit", nullable: true),
                    OfficeofInspectorGeneralCheck = table.Column<bool>(type: "bit", nullable: true),
                    SAMgovCheck = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalPrimarySourceVerifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    PractitionerGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EncompassID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLocationLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ServiceLocations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLicenseExpirationifapplicabl = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceLicenseNumberifapplicable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLicenseStatusifapplicable = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLocationLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    AccountSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityLicenseExpirationifapplicab = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityLicenseifapplicable = table.Column<bool>(type: "bit", nullable: true),
                    FacilityLicenseNumberifapplicable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityLicenseStatusifapplicable = table.Column<int>(type: "int", nullable: true),
                    HoursofOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccomodationsAccessibility = table.Column<int>(type: "int", nullable: true),
                    AccomodationsAccessibilityOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicensedFacility = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLocations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialingContacts");

            migrationBuilder.DropTable(
                name: "DirectServices");

            migrationBuilder.DropTable(
                name: "OrganizationalCredentialingProfiles");

            migrationBuilder.DropTable(
                name: "OrganizationalPrimarySourceVerifications");

            migrationBuilder.DropTable(
                name: "ProviderKeys");

            migrationBuilder.DropTable(
                name: "ServiceLocationLicenses");

            migrationBuilder.DropTable(
                name: "ServiceLocations");
        }
    }
}
