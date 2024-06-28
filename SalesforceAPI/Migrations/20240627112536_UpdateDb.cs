using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesforceAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CollegeUniversityProgramAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeUniversityProgramName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialingProfileID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalAffiliations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalAffiliationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalAffiliationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryofMembership = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateofAffiliation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateofAffiliation = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalAffiliations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostGraduateMedicalTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalTrainingHospitalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalTrainingHospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalTrainingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrainingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGraduateMedicalTrainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PractitionerCredentialingProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxID = table.Column<long>(type: "bigint", nullable: true),
                    MedicareProvider = table.Column<bool>(type: "bit", nullable: true),
                    MedicareNumber = table.Column<long>(type: "bigint", nullable: true),
                    PractitionerNPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    CurrentMalpracticeInsuranceCoverage = table.Column<int>(type: "int", nullable: true),
                    MalpracticeInsuranceCoverageStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExplanationCurrentMalpractice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalpracticeInsuranceCoverageExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguagesSpoken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CulturalCompetences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fiveyearworkhistory = table.Column<bool>(type: "bit", nullable: true),
                    X6monthgapinemploymentsinceprofess = table.Column<int>(type: "int", nullable: true),
                    X6MonthGapStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    X6MonthGapEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    X6MonthGapActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X6MonthGapReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PleaseacknowledgeIfdeniedcredential = table.Column<bool>(type: "bit", nullable: true),
                    Canyouperformtheessentialdutiesof = table.Column<int>(type: "int", nullable: true),
                    Reasonforinabilitytoperformessentia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lackofpresentillegaldruguse = table.Column<int>(type: "int", nullable: true),
                    ExplanationLackofpresentillegaldrug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Historyoflossoflicense = table.Column<int>(type: "int", nullable: true),
                    ExplanationHistoryoflossoflicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Historyoffelonyconvictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExplanationHistoryoffelonyconvictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Historyoflossorlimitationsofprivil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExplanationHistoryofloss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attestationbytheapplicantofthecorr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CulturalCompetenciesPicklist = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractitionerCredentialingProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PractitionerLicensesCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    RecordTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialingProfileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileUploaded = table.Column<bool>(type: "bit", nullable: true),
                    LicenseCertificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseCertificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseTypesLARA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractitionerLicensesCertifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PractitionerPrimarySourceVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CredentialingProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimarySourceVerifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVO = table.Column<bool>(type: "bit", nullable: true),
                    OtherAccred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiersredentialingOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IchatBackgroundheck = table.Column<bool>(type: "bit", nullable: true),
                    WorkforceBackgroundheck = table.Column<bool>(type: "bit", nullable: true),
                    MedicareBaseEnrollment = table.Column<bool>(type: "bit", nullable: true),
                    MedicareOptOut = table.Column<bool>(type: "bit", nullable: true),
                    LARALicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LARAUploaded = table.Column<bool>(type: "bit", nullable: true),
                    OfficialTranscriptfromAccreditedScho = table.Column<bool>(type: "bit", nullable: true),
                    DegreeVerification = table.Column<bool>(type: "bit", nullable: true),
                    AMAVerification = table.Column<bool>(type: "bit", nullable: true),
                    AOAVerification = table.Column<bool>(type: "bit", nullable: true),
                    MDHHSSanctionedProviderheck = table.Column<bool>(type: "bit", nullable: true),
                    MCBAPVerification = table.Column<bool>(type: "bit", nullable: true),
                    OfficeofInspectorGeneralheck = table.Column<bool>(type: "bit", nullable: true),
                    SAMgovheck = table.Column<bool>(type: "bit", nullable: true),
                    NationalSexOffenderRegistryheck = table.Column<bool>(type: "bit", nullable: true),
                    MIPublicSexOffenderRegistryheck = table.Column<bool>(type: "bit", nullable: true),
                    ECFMG = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractitionerPrimarySourceVerifications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "HospitalAffiliations");

            migrationBuilder.DropTable(
                name: "PostGraduateMedicalTrainings");

            migrationBuilder.DropTable(
                name: "PractitionerCredentialingProfiles");

            migrationBuilder.DropTable(
                name: "PractitionerLicensesCertifications");

            migrationBuilder.DropTable(
                name: "PractitionerPrimarySourceVerifications");
        }
    }
}
